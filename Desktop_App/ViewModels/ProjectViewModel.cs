using Desktop_App.Core;
using Desktop_App.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowProjectstWindow { get; set; }

        private object _currentView;
        private ProjectViewModel _projectsviewmodel;

        public ProjectViewModel ProjectsViewModel
        {
            get => _projectsviewmodel;
            set
            {
                _projectsviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectsViewModel)));
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

        public ProjectViewModel()
        {
            ProjectsViewModel = new ProjectViewModel("Project");

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
            ShowProjectstWindow = new(o => { CurrentView = ProjectsViewModel; });
        }

        private RelayCommand _changedselection;
        private RelayCommand _deleteselection;
        private RelayCommand _getdatascomm;
        private RelayCommand _send;
        private Projects _selected;
        private List<Projects> _rawProjects = new();
        private List<Projects> _projects = new();
        private string _description;
        private string _header;
        private string _image;

        public RelayCommand ChangedSelection => _changedselection ?? (_changedselection = new RelayCommand(obj => Update()));
        public RelayCommand GetDatasComm => _getdatascomm ?? (_getdatascomm = new RelayCommand(obj => GetDatas()));
        public RelayCommand DeleteSelection => _deleteselection ?? (_deleteselection = new RelayCommand(obj => Delete()));
        public RelayCommand Send => _send ?? (_send = new RelayCommand(obj => Create()));

        public Projects Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public List<Projects> RawProjects
        {
            get => _rawProjects;
            set
            {
                _rawProjects = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RawProjects)));
            }
        }
        public List<Projects> Project
        {
            get => _projects;
            set
            {
                _projects = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Project)));
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
        public string ProjectInformation
        {
            get => _description;
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectInformation)));
            }
        }
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }

        private string ApiType { get; set; }
        private Dictionary<int, int> Ids { get; set; }
        public ProjectViewModel(string apitype)
        {
            ApiType = apitype;
            GetDatas();
        }

        public void GetDatas()
        {
            CRUD.Read(ApiType);
            RawProjects = JsonConvert.DeserializeObject<List<Projects>>(CRUD.Read(ApiType));
            Show();
        }
        private void Create()
        {
            CRUD.Create(ApiType, JsonConvert.SerializeObject(new Projects(0, Header, Image, ProjectInformation)));
            GetDatas();
        }
        private void Update()
        {
            CRUD.Update(ApiType, JsonConvert.SerializeObject(new Projects(Ids[Selected.Id], Selected.Header, Selected.Image, Selected.ProjectInformation)));
            GetDatas();
        }
        private void Delete()
        {
            CRUD.Delete($"{ApiType}/{Ids[Selected.Id]}");
            GetDatas();
        }
        private void Show()
        {
            Project.Clear();
            Ids = new Dictionary<int, int>();
            for (int i = 0; i < RawProjects.Count; i++)
            {
                Ids.Add(i + 1, RawProjects[i].Id);
                Project.Add(RawProjects[i]);
                Project[Project.Count - 1].Id = i + 1;
            }
        }
    }
}
