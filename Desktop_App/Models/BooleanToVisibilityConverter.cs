using Desktop_App.ViewModels;
using Desktop_App.Views;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Desktop_App.Models
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static MainViewModel _mainViewModel { get; set; }
        public LoginWindow LoginWin = new(_mainViewModel);
        private object GetVisibility(LoginWindow value)
        {
            if (LoginWindow.Role == "user")
                return Visibility.Collapsed;
            
            if (LoginWindow.Role == "admin")
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetVisibility(LoginWin);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetVisibility(LoginWin);
        }
    }
}
