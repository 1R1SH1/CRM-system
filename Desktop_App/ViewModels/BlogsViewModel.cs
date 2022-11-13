using Desktop_App.Core;
using Desktop_App.Models;
using Desktop_App.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class BlogsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowBlogsWindow { get; set; }

        private object _currentView;
        private BlogsViewModel _blogsviewmodel;

        public BlogsViewModel BlogViewModel
        {
            get => _blogsviewmodel;
            set
            {
                _blogsviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BlogViewModel)));
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

        public BlogsViewModel()
        {
            BlogViewModel = new BlogsViewModel("Blog");

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
            ShowBlogsWindow = new(o => { CurrentView = BlogViewModel; });
        }

        private RelayCommand _changedselection;
        private RelayCommand _deleteselection;
        private RelayCommand _getdatascomm;
        private RelayCommand _send;
        private Blogs _selected;
        private List<Blogs> _rawBlogs = new();
        private List<Blogs> _blogs = new();
        private string _description;
        private string _header;
        private string _image;
        private DateTime _date;

        public RelayCommand GetDatasComm => _getdatascomm ?? (_getdatascomm = new RelayCommand(obj => GetDatas()));
        public RelayCommand ChangedSelection => _changedselection ?? (_changedselection = new RelayCommand(obj => Update()));
        public RelayCommand DeleteSelection => _deleteselection ?? (_deleteselection = new RelayCommand(obj => Delete()));
        public RelayCommand Send => _send ?? (_send = new RelayCommand(obj => Create()));

        public Blogs Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public List<Blogs> RawBlogs
        {
            get => _rawBlogs;
            set
            {
                _rawBlogs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RawBlogs)));
            }
        }
        public List<Blogs> Blog
        {
            get => _blogs;
            set
            {
                _blogs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Blog)));
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
        public string BlogInformation
        {
            get => _description;
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BlogInformation)));
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
        public DateTime DateTimes
        {
            get => _date;
            set
            {
                _date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTimes)));
            }
        }

        private string ApiType { get; set; }
        private Dictionary<int, int> Ids { get; set; }
        public BlogsViewModel(string apitype)
        {
            ApiType = apitype;
            GetDatas();
        }

        public void GetDatas()
        {
            CRUD.Read(ApiType);
            RawBlogs = JsonConvert.DeserializeObject<List<Blogs>>(CRUD.Read(ApiType));
            Show();
        }
        private void Create()
        {
            if(LoginWindow.Role == "admin")
            {
                CRUD.Create(ApiType, JsonConvert.SerializeObject(new Blogs(0, Header, Image, BlogInformation, DateTimes)));
                GetDatas();
            }            
        }
        private void Update()
        {
            if (LoginWindow.Role == "admin")
            {
                CRUD.Update(ApiType, JsonConvert.SerializeObject(new Blogs(Ids[Selected.Id], Selected.Header, Selected.Image, Selected.BlogInformation, Selected.DateTime)));
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
            Blog.Clear();
            Ids = new Dictionary<int, int>();
            for (int i = 0; i < RawBlogs.Count; i++)
            {
                Ids.Add(i + 1, RawBlogs[i].Id);
                Blog.Add(RawBlogs[i]);
                Blog[Blog.Count - 1].Id = i + 1;
            }
        }
    }
}
