using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ServicesController : Controller
    {
        private static List<Services>? _services = new List<Services>();

        public IActionResult Service()
        {
            _services = JsonConvert.DeserializeObject<List<Services>>(CRUD.Read("Services"));
            return View(_services);
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddService(Services services)
        {
            if (services.Id == 0)
            {
                CRUD.Create("Services", JsonConvert.SerializeObject(services));
            }
            return RedirectToAction("Service", "Services");
        }

        [HttpGet]
        public IActionResult UpdateService()
        {
            return View();
        }

        public IActionResult DeleteService(string id)
        {
            CRUD.Delete($"Services/{id}");
            return RedirectToAction("Service", "Services");
        }

        public IActionResult UpdateService(Services services, string Header, string ServicesInformation)
        {
            services.Header = Header;
            services.ServicesInformation = ServicesInformation;
            if (services.Id == 0)
            {
                CRUD.Create("Services", JsonConvert.SerializeObject(services));
            }
            else
            {
                CRUD.Update("Services", JsonConvert.SerializeObject(services));
            }
            return RedirectToAction("Service", "Services");
        }
    }
}