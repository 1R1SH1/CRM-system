using IT_Consulting_CRM_Web.Models;
using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ProjectsController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        private static List<Projects> _services = new();

        public static Projects Template { get; set; }

        public IActionResult Project()
        {
            _services = JsonConvert.DeserializeObject<List<Projects>>(CRUD.Read("Project"));
            return View(_services);
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(Projects projects)
        {
            string url = @"https://localhost:44390/api/Project";
            string p = httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(projects),
                Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Project", "Projects");
        }

        public IActionResult ProjectEdit()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteProject(string id)
        {
            //CRUD.Delete($"Projects/{id}");
            return RedirectToAction("Project");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateProject()
        {
            if (Template.Id == 0)
            {
                //CRUD.Create("Projects", JsonConvert.SerializeObject(Template));
            }
            else
            {
                //CRUD.Update("Projects", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("Project");
        }
    }
}
