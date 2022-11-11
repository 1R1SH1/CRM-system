using Desktop_App.Core;
using Desktop_App.Views;
using System.ComponentModel;

namespace Desktop_App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginWindow logWin { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand ShowLoginWindow { get; set; }
        private RelayCommand _send;

        private object _currentView;
        private LoginWindow _loginWindow;

        public LoginWindow LogWin
        {
            get => _loginWindow;
            set
            {
                _loginWindow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogWin)));
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

        public LoginViewModel(LoginWindow loginWindow)
        {
            LogWin = loginWindow;

            ShowLoginWindow = new(o => { CurrentView = LogWin; });

        }
        public RelayCommand Send => _send ?? (_send = new RelayCommand(obj => LogWin.EnterDesktop()));
    }
}
