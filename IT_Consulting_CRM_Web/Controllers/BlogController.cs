using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static List<Blogs> Blog = new List<Blogs>();
        public static Blogs Selected { get; set; }
        public static Blogs Template { get; set; }

        public BlogController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ShowBlog()
        {
            //Blog = JsonConvert.DeserializeObject<List<Blogs>>(CRUD.Read("Blog").ToString());
            return View();
        }

        public IActionResult AddBlog(string Image, string blogInformation, DateTime dateTime)
        {
            Template.Image = Image;
            Template.BlogInformation = blogInformation;
            Template.dateTime = dateTime;
            if (Template.Id == 0)
            {
                //CRUD.Create("Blog", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("Index", "Blog");
        }

        public IActionResult BlogEdit(int id)
        {
            if (id != 0)
            {
                Selected = Blog.Find(u => u.Id == id);
            }
            else
            {
                Selected = new Blogs();
            }
            return View();
        }

        public IActionResult DeleteBlog(string id)
        {
            //CRUD.Delete($"Blog/{id}");
            return RedirectToAction("ShowBlog", "Blog");
        }

        [HttpPost]
        public IActionResult UpdateBlog(string Image, string blogInformation, DateTime dateTime)
        {
            Template.Image = Image;
            Template.BlogInformation = blogInformation;
            Template.dateTime = dateTime;
            if (Template.Id == 0)
            {
                //CRUD.Create("Blog", JsonConvert.SerializeObject(Template));
            }
            else
            {
                //CRUD.Update("Blog", JsonConvert.SerializeObject(Template));
            }
            return RedirectToAction("ShowBlog", "Blog");
        }
    }
}
