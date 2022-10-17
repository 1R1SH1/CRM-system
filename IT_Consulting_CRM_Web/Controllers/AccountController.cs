using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class AccountController : Controller
    {

        private HttpClient httpClient = new HttpClient();
        public static string Name { get; set; }
        public static string Role { get; set; }
        public static string Token { get; set; }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Name == null) { model.Name = ""; }
            if (model.Password == null) { model.Password = ""; }
            string url = "https://localhost:44390/api/Authentication";
            Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            if (Token != "Unauthorize")
            {
                string host = "https://localhost:44390/api/Values";
                string json = httpClient.GetStringAsync(host).Result;
                List<string> r = JsonConvert.DeserializeObject<List<string>>(json);
                Name = r[0];
                Role = r[1];
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationViewModel model)
        {
            string url = @"https://localhost:44390/api/Authentication/registration";
            var post = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Logout()
        {
            Name = "";
            Role = "";
            string url = "https://localhost:44390/api/Authentication";
            httpClient.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
            return RedirectToAction("Index", "Home");
        }
    }
}