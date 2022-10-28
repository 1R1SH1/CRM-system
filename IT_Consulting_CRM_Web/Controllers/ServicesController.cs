using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ServicesController : Controller
    {
        private static List<Services> _services = new List<Services>();

        private static Services Template { get; set; }

        public IActionResult Service()
        {
            _services = JsonConvert.DeserializeObject<List<Services>>(CRUD.Read("Services"));
            return View(_services);
        }

        public IActionResult AddService(string serviceInformation)
        {
            Template.ServicesInformation = serviceInformation;
            if (Template.Id == 0)
            {
                CRUD.Create("Services", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("ServiceEdit", "Services");
        }

        public IActionResult ServiceEdit()
        {
            return View();
        }

        public IActionResult DeleteService(string id)
        {
            CRUD.Delete($"Services/{id}");
            return RedirectToAction("ShowServices", "Services");
        }

        [HttpPost]
        public IActionResult UpdateService()
        {
            if (Template.Id == 0)
            {
                CRUD.Create("Services", JsonConvert.SerializeObject(Template));
            }
            else
            {
                CRUD.Update("Services", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("ShowServices", "Services");
        }
    }
}