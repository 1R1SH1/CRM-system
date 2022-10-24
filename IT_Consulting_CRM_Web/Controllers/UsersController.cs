using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class UsersController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        public static List<CreateUserViewModel> users = new List<CreateUserViewModel>();

        public static CreateUserViewModel Template { get; set; }

        public IActionResult Index()
        {
            users = JsonConvert.DeserializeObject<List<CreateUserViewModel>>(CRUD.Read("Users"));
            return View(users);
        }
    }
}
