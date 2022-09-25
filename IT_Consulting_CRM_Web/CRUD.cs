using System;
using System.Net.Http;
using System.Text;

namespace IT_Consulting_CRM_Web
{
    /// <summary>
    /// Класс методов обращения к серверу.
    /// </summary>
    public static class CRUD
    {
        public static HttpClient httpClient = new HttpClient();
        public static string Host = "https://localhost:5001/api/";

        public static string Token { get; set; }


        /// Добавить данные на сервер (строки) (Create)
        public static void Create(string url, string json)
        {
            Answer(httpClient.PostAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }

        /// Получить данные с сервера (Read)
        public static string Read(string url)
        {
            return httpClient.GetStringAsync($"{Host}{url}").Result;
        }
        public static void Update(string url, string json)
        {
            Answer(httpClient.PutAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }
        public static void Delete(string url)
        {
            Answer(httpClient.DeleteAsync($"{Host}{url}").Result.ToString());
        }
        /// Сообщение сервера в случае неудачи.
        private static void Answer(string answer)
        {
            if (!answer.Contains("200")) Console.WriteLine(answer);
        }
    }
}
