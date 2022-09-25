using IT_Consulting_CRM_Web.Models;
using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class AccountController : Controller
    {
        const string host = "https://localhost:5001/api/Accaunt/";
        private HttpClient httpClient = new HttpClient();

        public static string Name { get; set; }
        public static string Role { get; set; }

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //string url = @"https://localhost:5001/api/Accaunt/authentication";
            //CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;

            string url = host + "authentication/";
            CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.ToString();

            //ModelState.AddModelError(String.Empty, "Неправильный пароль");
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            //string url = @"https://localhost:5001/api/Accaunt/registration";
            //CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
            //    Encoding.UTF8, "application/json")).Result.ToString();

            string url = host + "registration/";
            CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.ToString();

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //string url = @"https://localhost:5001/api/Accaunt/logout";
            Name = "";
            Role = "";
            string url = host + "logout/";
            await httpClient.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
