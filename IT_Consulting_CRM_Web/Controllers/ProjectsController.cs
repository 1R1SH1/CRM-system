using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ProjectsController : Controller
    {
        private static List<Projects> _project = new List<Projects>();

        public static Projects Template { get; set; }

        [HttpGet]
        public IActionResult Project()
        {
            return View(ShowProjects());
        }

        [HttpPost]
        public IActionResult ShowProjects()
        {
            _project = JsonConvert.DeserializeObject<List<Projects>>(CRUD.Read("Projects"));
            return View();
        }

        [HttpPost]
        public IActionResult AddProject()
        {
            if (Template.Id == 0)
            {
                CRUD.Create("Projects", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("Project");
        }

        [HttpGet]
        public IActionResult ProjectEdit()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteProject(string id)
        {
            CRUD.Delete($"Projects/{id}");
            return RedirectToAction("Project");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateProject()
        {
            if (Template.Id == 0)
            {
                CRUD.Create("Projects", JsonConvert.SerializeObject(Template));
            }
            else
            {
                CRUD.Update("Projects", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("Project");
        }
    }
}
