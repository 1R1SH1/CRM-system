using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            var user = JsonConvert.DeserializeObject<List<LoginViewModel>>(CRUD.Read("Users"));
            return View(user);
        }
    }
}
