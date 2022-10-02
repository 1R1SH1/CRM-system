using IT_Consulting_CRM_Web.Models;
using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient httpClient = new HttpClient();
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public static string Name { get; set; }
        public static string Role { get; set; }

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            string url = @"https://localhost:5001/api/Account/authentication";

            if (model.Username == null) { model.Username = ""; }
            if (model.Password == null) { model.Password = ""; }
            CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            if (CRUD.Token != "Unauthorize")
            {
                string urll = url + "values";
                string json = httpClient.GetAsync(urll).Result.ToString();
                List<User>? r = JsonConvert.DeserializeObject<List<User>>(json);
                Name = r[0].ToString();
                Role = r[1].ToString();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            string url = @"https://localhost:5001/api/Account/registration";
            //CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
            //    Encoding.UTF8, "application/json")).Result.ToString();

            //string url = host + "registration/";
            CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).ToString();

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            string url = @"https://localhost:5001/api/Accaunt/logout";
            Name = "";
            Role = "";
            await httpClient.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
