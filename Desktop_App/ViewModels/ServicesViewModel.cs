using Desktop_App.Core;
using Desktop_App.Models;
using Desktop_App.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class ServicesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowSrvicestWindow { get; set; }

        private object _currentView;
        private ServicesViewModel _servicesviewmodel;

        public ServicesViewModel ServiceViewModel
        {
            get => _servicesviewmodel;
            set
            {
                _servicesviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ServiceViewModel)));
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentView)));
            }
        }

        public ServicesViewModel()
        {
            ServiceViewModel = new ServicesViewModel("Services");

            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindowCommand = new(o => { Application.Current.MainWindow.DragMove(); });
            ShutDownWindowCommand = new(o => { Application.Current.Shutdown(); });
            MaximizeWindowCommand = new(o =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });

            MinimizeWindowCommand = new(o =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            ShowSrvicestWindow = new(o => { CurrentView = ServiceViewModel; });
        }

        private RelayCommand _changedselection;
        private RelayCommand _deleteselection;
        private RelayCommand _getdatascomm;
        private RelayCommand _send;
        private Services _selected;
        private List<Services> _rawservices = new();
        private List<Services> _services = new();
        private string _description;
        private string _header;

        public RelayCommand ChangedSelection => _changedselection ?? (_changedselection = new RelayCommand(obj => Update()));
        public RelayCommand GetDatasComm => _getdatascomm ?? (_getdatascomm = new RelayCommand(obj => GetDatas()));
        public RelayCommand DeleteSelection => _deleteselection ?? (_deleteselection = new RelayCommand(obj => Delete()));
        public RelayCommand Send => _send ?? (_send = new RelayCommand(obj => Create()));

        public Services Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public List<Services> RawServices
        {
            get => _rawservices;
            set
            {
                _rawservices = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RawServices)));
            }
        }
        public List<Services> Service
        {
            get => _services;
            set
            {
                _services = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Service)));
            }
        }
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Header)));
            }
        }
        public string ServiceInformation
        {
            get => _description;
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ServiceInformation)));
            }
        }

        private string ApiType { get; set; }
        private Dictionary<int, int> Ids { get; set; }
        public ServicesViewModel(string apitype)
        {
            ApiType = apitype;
            GetDatas();
        }

        public void GetDatas()
        {
            CRUD.Read(ApiType);
            RawServices = JsonConvert.DeserializeObject<List<Services>>(CRUD.Read(ApiType));
            Show();
        }
        private void Create()
        {
            if(LoginWindow.Role == "admin")
            {
                CRUD.Create(ApiType, JsonConvert.SerializeObject(new Services(0, Header, ServiceInformation)));
                GetDatas();
            }            
        }
        private void Update()
        {
            if (LoginWindow.Role == "admin")
            {
                CRUD.Update(ApiType, JsonConvert.SerializeObject(new Services(Ids[Selected.Id], Selected.Header, Selected.ServicesInformation)));
                GetDatas();
            }                
        }
        private void Delete()
        {
            if (LoginWindow.Role == "admin")
            {
                CRUD.Delete($"{ApiType}/{Ids[Selected.Id]}");
                GetDatas();
            }
        }
        private void Show()
        {
            Service.Clear();
            Ids = new Dictionary<int, int>();
            for (int i = 0; i < RawServices.Count; i++)
            {
                Ids.Add(i + 1, RawServices[i].Id);
                Service.Add(RawServices[i]);
                Service[Service.Count - 1].Id = i + 1;
            }
        }
    }
}
