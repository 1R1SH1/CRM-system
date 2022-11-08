using Desktop_App.Core;
using Desktop_App.Models;
using Desktop_App.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public MainWindow Main { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowLoginWindow { get; set; }

        private object _currentView;
        private LoginViewModel _logviewmodel;

        public LoginViewModel LogViewModel
        {
            get => _logviewmodel;
            set
            {
                _logviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogViewModel)));
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

        public LoginViewModel()
        {
            LogViewModel = new LoginViewModel("Authentication");

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
            ShowLoginWindow = new(o => { CurrentView = LogViewModel; });
        }

        public  string ApiType { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public static string Role { get; set; }
        public LoginViewModel(string apitype)
        {
            ApiType = apitype;
            Enter();
        }

        public string Auth()
        {
            SendLogin model = new SendLogin(Login, Password);
            string url = Main.Host + "Authentication";
            return Main.httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                mediaType: "application/json")).Result.Content.ReadAsStringAsync().Result;
        }
        private void Enter()
        {
            if (Login != "" && Password != "" && Login != null && Password != null)
            {
                if (Auth() != "Unauthorize")
                {
                    string host = "https://localhost:44390/api/Values";
                    string json = Main.httpClient.GetStringAsync(host).Result;
                    List<string> r = JsonConvert.DeserializeObject<List<string>>(json);
                    Role = r[1];

                }
            }
        }
    }
}
