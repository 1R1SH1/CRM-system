using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Index(string host)
        {
            host = "https://localhost:44390/api/Users";
            string json = httpClient.GetStringAsync(host).Result;
            Console.WriteLine(json);
            var r = JsonConvert.DeserializeObject(json);
            Console.WriteLine(r);

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
            string url = "https://localhost:44390/api/Users";

            var post = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;

            return View(model);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            string url = "https://localhost:44390/api/Users";
            var post = httpClient.DeleteAsync(url).Result.Content.ReadAsStringAsync().Result;

            return RedirectToAction(nameof(Index));
        }
    }
}
