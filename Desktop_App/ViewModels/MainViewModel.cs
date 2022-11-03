using Desktop_App.Core;
using Desktop_App.Views;
using System.ComponentModel;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowRequestWindow { get; set; }
        public RelayCommand ShowServiceWindow { get; set; }
        public RelayCommand ShowProjectWindow { get; set; }
        public RelayCommand ShowBlogsWindow { get; set; }
        public RelayCommand ShowContactsWindow { get; set; }
        public MainWindow MainWin { get; set; }
        private object _currentView;
        
        public event PropertyChangedEventHandler PropertyChanged;
        private RequestsViewModel _requestviewmodel;
        private ServicesViewModel _serviceviewmodel;
        private ProjectViewModel _projectsviewmodel;
        private BlogsViewModel _blogsviewmodel;
        private ContactsViewModel _contactsviewmodel;

        public RequestsViewModel RequestViewModel
        {
            get => _requestviewmodel;
            set
            {
                _requestviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestViewModel)));
            }
        }
        public ServicesViewModel ServiceViewModel
        {
            get => _serviceviewmodel;
            set
            {
                _serviceviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ServiceViewModel)));
            }
        }
        public ProjectViewModel ProjectsViewModel
        {
            get => _projectsviewmodel;
            set
            {
                _projectsviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectsViewModel)));
            }
        }
        public BlogsViewModel BlogViewModel
        {
            get => _blogsviewmodel;
            set
            {
                _blogsviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BlogViewModel)));
            }
        }
        public ContactsViewModel ContactViewModel
        {
            get => _contactsviewmodel;
            set
            {
                _contactsviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContactViewModel)));
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

        public MainViewModel()
        {
            RequestViewModel = new RequestsViewModel("Request");
            ServiceViewModel = new ServicesViewModel("Services");
            ProjectsViewModel = new ProjectViewModel("Project");
            BlogViewModel = new BlogsViewModel("Blog");
            ContactViewModel = new ContactsViewModel("Contacts");

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
            ShowRequestWindow = new(o => { CurrentView = RequestViewModel; });
            ShowServiceWindow = new(o => { CurrentView = ServiceViewModel; });
            ShowProjectWindow = new(o => { CurrentView = ProjectsViewModel; });
            ShowBlogsWindow = new(o => { CurrentView = BlogViewModel; });
            ShowContactsWindow = new(o => { CurrentView = ContactViewModel; });
        }
    }
}
