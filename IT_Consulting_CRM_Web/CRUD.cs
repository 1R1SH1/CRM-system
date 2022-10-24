using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IT_Consulting_CRM_Web
{
    public static class CRUD
    {
        public static HttpClient httpClient = new HttpClient();
        public static string Host = "https://localhost:44390/api/";

        public static string Token { get; set; }

        public static void Create(string url, string json)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Answer(httpClient.PostAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }

        public static void CreateContact(string url, string description)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "*/*");
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(description), "url");
            Answer(httpClient.PostAsync($"{Host}{url}", multipartContent).Result.ToString());
        }

        public static void UpdateContact(string url)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "*/*");
            Answer(httpClient.PutAsync($"{Host}{url}", new StringContent("")).Result.ToString());
        }

        public static string Read(string url)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return httpClient.GetStringAsync($"{Host}{url}").Result;
        }
        public static void Update(string url, string json)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Answer(httpClient.PutAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }
        public static void Delete(string url)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Answer(httpClient.DeleteAsync($"{Host}{url}").Result.ToString());
        }
        private static void Answer(string answer)
        {
            if (!answer.Contains("200")) Console.WriteLine(answer);
        }
    }
}
