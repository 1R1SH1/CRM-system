using Desktop_App.Views;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Desktop_App.Core
{
    public class CRUD
    {
        public static MainWindow Main { get; set; }
        public static void Init(MainWindow main)
        {
            Main = main;
        }

        public static void Create(string url, string json)
        {
            Answer(Main.httpClient.PostAsync($"{Main.Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }

        public static string Read(string url)
        {
            return Main.httpClient.GetAsync($"{Main.Host}{url}").Result.Content.ReadAsStringAsync().Result;
        }
        public static void Update(string url, string json)
        {
            Answer(Main.httpClient.PutAsync($"{Main.Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }
        public static void Update(string url)
        {
            Answer(Main.httpClient.PutAsync($"{Main.Host}{url}", new StringContent("")).Result.ToString());
        }
        public static void Delete(string url)
        {
            Answer(Main.httpClient.DeleteAsync($"{Main.Host}{url}").Result.Content.ReadAsStringAsync().Result);
        }
        private static void Answer(string answer)
        {
            if (!answer.Contains("200")) Console.WriteLine(answer);
        }
    }
}
