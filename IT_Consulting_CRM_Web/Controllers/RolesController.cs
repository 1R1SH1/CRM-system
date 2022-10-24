using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace IT_Consulting_CRM_API.Controllers
{
    public class RolesController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), "Users");
        }

        [HttpGet]
        public IActionResult Edit(ChangeRoleViewModel model)
        {
            string url = @"https://localhost:44390/api/Roles";

            string json = httpClient.GetAsync(url).Result.ToString();
            List<string> r = JsonConvert.DeserializeObject<List<string>>(json.ToString());

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        public IActionResult Edit(string userId, List<string> roles)
        {
            string url = @"https://localhost:44390/api/Roles";

            string json = httpClient.GetAsync(url).Result.ToString();
            List<string> r = JsonConvert.DeserializeObject<List<string>>(json.ToString());

            return RedirectToAction(nameof(Index), "Home");
        }


    }
}
