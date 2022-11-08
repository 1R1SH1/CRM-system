using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class UsersController : Controller
    {
        public static List<AnswerUser> users = new List<AnswerUser>();

        public IActionResult Index()
        {
            users = JsonConvert.DeserializeObject<List<AnswerUser>>(CRUD.Read("Users"));
            return View(users);
        }
    }
}
