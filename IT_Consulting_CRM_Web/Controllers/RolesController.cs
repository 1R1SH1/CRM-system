using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_API.Controllers
{
    public class RolesController : Controller
    {
        private HttpClient httpClient = new HttpClient();
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), "Users");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(ChangeRoleViewModel model)
        {
            string url = @"https://localhost:44390/api/Roles";

            string json = httpClient.GetAsync(url).Result.ToString();
            List<string> r = JsonConvert.DeserializeObject<List<string>>(json.ToString());

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            string url = @"https://localhost:44390/api/Roles";

            string json = httpClient.GetAsync(url).Result.ToString();
            List<string> r = JsonConvert.DeserializeObject<List<string>>(json.ToString());

            return RedirectToAction(nameof(Index), "Home");
        }


    }
}
