using Desktop_App.Core;
using Desktop_App.Models;
using Desktop_App.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Controls;

namespace Desktop_App.Views
{
    public partial class LoginWindow : UserControl
    {
        public MainViewModel ViewModel { get; set; }
        public LoginWindow(MainViewModel model)
        {
            InitializeComponent();
            ViewModel = model;
            CRUD.Login(this);
            DataContext = new LoginViewModel(this);
        }
        public string Host;
        public HttpClient httpClient = new HttpClient();

        public static string Role { get; set; }

        public void EnterDesktop()
        {
            Enter();
        }
        public string Auth()
        {
            Host = "https://localhost:5001/api/";
            SendLogin model = new(Login.Text, Password.Password);
            string url = Host + "Authentication/";
            return CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                       Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
        }
        public void Enter()
        {
            if (Login.Text != "" && Password.Password != "" && Login.Text != null && Password.Password != null)
            {
                if (Auth() != "Unauthorize")
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CRUD.Token);
                    Host = "https://localhost:5001/api/";
                    string url = Host + "Values";
                    string json = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                    List<string> r = JsonConvert.DeserializeObject<List<string>>(json);
                    Role = r[1];
                    if (Role != null)
                    {
                        ViewModel.Access();
                    }
                    else
                    {
                        ViewModel.Access();
                    }
                }
            }
        }
    }
}
