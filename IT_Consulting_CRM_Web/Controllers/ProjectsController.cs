using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ProjectsController : Controller
    {
        private static List<Projects> _services = new();

        public IActionResult Project()
        {
            _services = JsonConvert.DeserializeObject<List<Projects>>(CRUD.Read("Project"));
            return View(_services);
        }

        public IActionResult ProjectDetails(Projects projects)
        {
            _services = JsonConvert.DeserializeObject<List<Projects>>(CRUD.Read("Project"));
            var p = _services.FirstOrDefault(x => x.Id == projects.Id);
            return View(p);
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(Projects projects)
        {
            if (projects.Id == 0)
            {
                CRUD.Create("Project", JsonConvert.SerializeObject(projects));
            }
            return RedirectToAction("Project", "Projects");
        }

        [HttpGet]
        public IActionResult UpdateProject()
        {
            return View();
        }

        public IActionResult DeleteProject(string id)
        {
            CRUD.Delete($"Project/{id}");
            return RedirectToAction("Project", "Projects");
        }

        public IActionResult UpdateProject(Projects projects, string Image, string Header, string ProjectInformation)
        {
            projects.Image = Image;
            projects.Header = Header;
            projects.ProjectInformation = ProjectInformation;
            if (projects.Id == 0)
            {
                CRUD.Create("Project", JsonConvert.SerializeObject(projects));
            }
            else
            {
                CRUD.Update("Project", JsonConvert.SerializeObject(projects));
            }
            return RedirectToAction("Project");
        }
    }
}
