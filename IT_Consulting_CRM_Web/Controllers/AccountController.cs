using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Site;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient httpClient = new HttpClient();
        public static string Username { get; set; }
        public static string Role { get; set; }
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            string url = @"https://localhost:44390/api/Account/authentication";

            if (model.Username == null) { model.Username = ""; }
            if (model.Password == null) { model.Password = ""; }
            string c = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", c);
            if (c != "Unauthorize")
            {
                string host = @"https://localhost:44390/api/Values";
                string json = httpClient.GetStringAsync(host).Result;
                Console.WriteLine(json);
                List<string> r = JsonConvert.DeserializeObject<List<string>>(json);
                //var r = JsonConvert.DeserializeObject(json);
                Username = r[0];
                Role = r[1];
                Console.WriteLine(r);
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationViewModel model)
        {
            string url = @"https://localhost:5001/api/Account/registration";
            var post = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            string url = @"https://localhost:5001/api/Accaunt/logout";
            await httpClient.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}