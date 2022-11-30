using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class UsersController : Controller
    {
        private static List<LoginViewModel> _log = new();

        [HttpGet]
        public IActionResult Index()
        {
            _log = JsonConvert.DeserializeObject<List<LoginViewModel>>(CRUD.Read("Users"));
            return View(_log);
        }
    }
}
