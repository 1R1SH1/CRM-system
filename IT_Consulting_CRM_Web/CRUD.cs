using System.Net.Http.Headers;
using System.Text;

namespace IT_Consulting_CRM_Web
{
    public static class CRUD
    {
        public static HttpClient httpClient = new HttpClient();
        public static string Host = "https://localhost:5001/api/";

        public static string? Token { get; set; }

        public static void Create(string url, string json)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Answer(httpClient.PostAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }

        public static string GetUsers(string url)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return httpClient.GetStringAsync($"{Host}{url}").Result;
        }

        public static string Read(string url)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return httpClient.GetAsync($"{Host}{url}").Result.Content.ReadAsStringAsync().Result;
        }
        public static void Update(string url, string json)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Answer(httpClient.PutAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }
        public static void Delete(string url)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Answer(httpClient.DeleteAsync($"{Host}{url}").Result.Content.ReadAsStringAsync().Result);
        }
        private static void Answer(string answer)
        {
            if (!answer.Contains("200")) Console.WriteLine(answer);
        }
    }
}
