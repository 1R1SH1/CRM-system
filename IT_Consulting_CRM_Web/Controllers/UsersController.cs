using IT_Consulting_CRM_Web.Models;
using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_API.Controllers
{
    public class UsersController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            string host = @"https://localhost:5001/api/Users/get";
            string json = httpClient.GetStringAsync(host).Result;
            Console.WriteLine(json);
            var r = JsonConvert.DeserializeObject(json);
            //Console.WriteLine(r);
            //string url = @"https://localhost:5001/api/Users/get";

            //var json = httpClient.GetAsync(url).Result;
            //List<User> r = JsonConvert.DeserializeObject<List<User>>(json.ToString());

            return View(r);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            string url = @"https://localhost:5001/api/Users/create";

            var post = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;

            return View(model);
        }

        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);

        //    if (user != null)
        //    {
        //        IdentityResult result = await _userManager.DeleteAsync(user);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
