using Desktop_App.Core;
using Desktop_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class RequestsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowRequestWindow { get; set; }

        private object _currentView;
        private RequestsViewModel _requestviewmodel;

        public RequestsViewModel RequestViewModel
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

        public RequestsViewModel()
        {
            RequestViewModel = new RequestsViewModel("Request");

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
        }

        private RelayCommand _forall;
        private string _info;
        private string _cnt;
        private string _findtext;
        private DateTime _end;
        private DateTime _start;
        private RelayCommand _month;
        private RelayCommand _week;
        private RelayCommand _tomorrow;
        private RelayCommand _today;
        private RelayCommand _changedselection;
        private int _selectedstatus;
        private RelayCommand _getdatascomm;
        private Requests _selected;
        private List<Requests> _rawrequests = new();
        private List<Requests> _requests = new();

        public RelayCommand ForAll => _forall ?? (_forall = new RelayCommand(obj => ForAllTimes()));
        public RelayCommand Month => _month ?? (_month = new RelayCommand(obj => ShowMonth()));
        public RelayCommand Week => _week ?? (_week = new RelayCommand(obj => ShowWeek()));
        public RelayCommand Tomorrow => _tomorrow ?? (_tomorrow = new RelayCommand(obj => ShowTomorrow()));
        public RelayCommand Today => _today ?? (_today = new RelayCommand(obj => ShowToday()));
        public RelayCommand ChangedSelection => _changedselection ?? (_changedselection = new RelayCommand(obj => UpdateStatus()));
        public RelayCommand GetDatasComm => _getdatascomm ?? (_getdatascomm = new RelayCommand(obj => GetDatas()));

        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Info)));
            }
        }
        public string Count
        {
            get => _cnt;
            set
            {
                _cnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }
        public string Findtext
        {
            get => _findtext;
            set
            {
                _findtext = value;
                if (RawRequests != null && Findtext != null) Show();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Findtext)));
            }
        }
        public DateTime End
        {
            get => _end;
            set
            {
                _end = value;
                if (End < Start) End = Start;
                if (Requests != null) Show();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(End)));
            }
        }
        public DateTime Start
        {
            get => _start;
            set
            {
                _start = value;
                if (End < Start) End = Start;
                if (Requests != null) Show();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Start)));
            }
        }
        public int SelectedStatus
        {
            get => _selectedstatus;
            set
            {
                _selectedstatus = value;
                MessageBox.Show($"{SelectedStatus}");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStatus)));
            }
        }
        public Requests Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public List<Requests> RawRequests
        {
            get => _rawrequests;
            set
            {
                _rawrequests = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RawRequests)));
            }
        }
        public List<Requests> Requests
        {
            get => _requests;
            set
            {
                _requests = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Requests)));
            }
        }

        private string ApiType { get; set; }
        private DateTime FirstDate { get; set; }
        public RequestsViewModel(string apitype)
        {
            ApiType = apitype;
            Findtext = "";
            FirstDate = DateTime.Today;
            Start = DateTime.Today;
            End = DateTime.Today;
            GetDatas();
        }

        public void GetDatas()
        {
            CRUD.Read(ApiType);
            RawRequests = JsonConvert.DeserializeObject<List<Requests>>(CRUD.Read(ApiType));
            if (RawRequests.Count > 0)
            {
                FirstDate = RawRequests[0].Date;
            }
            Show();
        }
        private void Show()
        {
            Requests = new List<Requests>();
            for (int i = 0; i < RawRequests.Count; i++)
            {
                if (RawRequests[i].Date >= Start && RawRequests[i].Date < End.AddDays(1))
                {
                    if (CheckAll(RawRequests[i])) Requests.Add(RawRequests[i]);
                }
            }
            Info = $"За всё время: {RawRequests.Count}";
            Count = $"Отсортировано: {Requests.Count}";
        }
        private void UpdateStatus()
        {
            CRUD.Update(ApiType, JsonConvert.SerializeObject(Selected));
            GetDatas();
        }
        private void ShowToday()
        {
            Start = DateTime.Today;
            End = DateTime.Today;
            Show();
        }
        private void ShowTomorrow()
        {
            Start = DateTime.Today.AddDays(-1);
            End = DateTime.Today.AddDays(-1);
            Show();
        }
        private void ShowWeek()
        {
            Start = GetFirstDayOfWeek(DateTime.Today);
            End = DateTime.Today;
            Show();
        }
        private void ShowMonth()
        {
            Start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            End = DateTime.Today;
            Show();
        }
        private bool CheckAll(Requests request)
        {
            bool Check = false;
            Check = Check || request.Name.ToLower().Contains(Findtext.ToLower());
            Check = Check || request.EMail.ToLower().Contains(Findtext.ToLower());
            Check = Check || request.Information.ToLower().Contains(Findtext.ToLower());
            return Check;
        }
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
        }
        private void ForAllTimes()
        {
            Start = FirstDate;
            End = DateTime.Today;
        }
    }
}
