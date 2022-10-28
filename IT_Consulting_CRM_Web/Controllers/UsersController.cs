using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class UsersController : Controller
    {
        public static List<CreateUserViewModel> users = new List<CreateUserViewModel>();

        public IActionResult Index()
        {
            users = JsonConvert.DeserializeObject<List<CreateUserViewModel>>(CRUD.Read("Users"));
            return View(users);
        }
    }
}
