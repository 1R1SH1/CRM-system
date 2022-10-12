using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Site;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        public IActionResult Login(LoginViewModel model)
        {
            string url = @"https://localhost:44390/api/Account/authentication";

            if (model.Username == null) { model.Username = ""; }
            if (model.Password == null) { model.Password = ""; }
            string c = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            if (c != "Unauthorize")
            {
                string urll = @"https://localhost:44390/api/Values";
                string json = httpClient.GetStringAsync(urll).Result;
                Console.WriteLine(json);
                var r = JsonConvert.DeserializeObject(json);
                Username = r.ToString();
                Role = r.ToString();
                Console.WriteLine(r);
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel model)
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