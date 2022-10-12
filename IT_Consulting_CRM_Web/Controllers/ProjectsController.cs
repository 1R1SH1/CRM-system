using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class ProjectsController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        const string host = "https://localhost:5001/api/Project";

        private static List<Projects> _services = new();

        public static Projects Template { get; set; }

        public IActionResult Project()
        {
            //string p = httpClient.GetStringAsync($"{host}").Result;
            //_services = JsonConvert.DeserializeObject<List<Projects>>(p);
            return View();
        }

        //public IActionResult ShowProject()
        //{
        //    string p = httpClient.GetStringAsync($"{host}").Result;
        //    _services = JsonConvert.DeserializeObject<List<Projects>>(p);
        //    return View(_services);
        //}

        public IActionResult AddProject(Projects projects)
        {
            string url = @"https://localhost:5001/api/Project";
            string p = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(projects),
                Encoding.UTF8, "application/json")).Result.ToString();
            return View();
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
