using Desktop_App.Core;
using Desktop_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class SendRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowSendRequestWindow { get; set; }

        private object _currentView;
        private SendRequestViewModel _requestviewmodel;

        public SendRequestViewModel RequestViewModel
        {
            get => _requestviewmodel;
            set
            {
                _requestviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestViewModel)));
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

        public SendRequestViewModel()
        {
            RequestViewModel = new SendRequestViewModel("Request");

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
            ShowSendRequestWindow = new(o => { CurrentView = RequestViewModel; });
        }

        private RelayCommand _send;
        private Requests _selected;
        private List<Requests> _rawrequests = new();
        private List<Requests> _requests = new();
        private string _description;
        private string _name;
        private string _surName;
        private string _eMail;
        private DateTime _dateTime;

        public RelayCommand Send => _send ?? (_send = new RelayCommand(obj => Create()));

        public Requests Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public List<Requests>? RawRequests
        {
            get => _rawrequests;
            set
            {
                _rawrequests = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RawRequests)));
            }
        }
        public List<Requests>? Requests
        {
            get => _requests;
            set
            {
                _requests = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Requests)));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string Information
        {
            get => _description;
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Information)));
            }
        }
        public string SurName
        {
            get => _surName;
            set
            {
                _surName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SurName)));
            }
        }
        public DateTime DateTimes
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTimes)));
            }
        }
        public string EMail
        {
            get => _eMail;
            set
            {
                _eMail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EMail)));
            }
        }

        private string ApiType { get; set; }
        private Dictionary<int, int> Ids { get; set; }
        public SendRequestViewModel(string apitype)
        {
            ApiType = apitype;
        }

        private void Create()
        {
            CRUD.Create(ApiType, JsonConvert.SerializeObject(new Requests(0, Name, SurName, EMail, Information)));
        }
    }
}
