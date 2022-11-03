using Desktop_App.Core;
using Desktop_App.ViewModels;
using System.Net.Http;
using System.Windows;

namespace Desktop_App.Views
{
    public partial class MainWindow : Window
    {
        public HttpClient httpClient { get; set; }
        public string Host { get; set; }
        public string apiType { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CRUD.Init(this);
            Host = "https://localhost:44390/api/";
            httpClient = new HttpClient();
            DataContext = new MainViewModel();
        }
    }
}
