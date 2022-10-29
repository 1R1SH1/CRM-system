using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class BlogController : Controller
    {

        private static List<Blogs> _blog = new();

        public IActionResult Blogs()
        {
            _blog = JsonConvert.DeserializeObject<List<Blogs>>(CRUD.Read("Blog"));
            return View(_blog);
        }

        public IActionResult BlogDetails(Blogs blogs)
        {
            _blog = JsonConvert.DeserializeObject<List<Blogs>>(CRUD.Read("Blog"));
            var p = _blog.FirstOrDefault(x => x.Id == blogs.Id);
            return View(p);
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBlog(Blogs blogs)
        {
            if (blogs.Id == 0)
            {
                CRUD.Create("Blog", JsonConvert.SerializeObject(blogs));
            }
            return RedirectToAction("Blogs", "Blog");
        }

        [HttpGet]
        public IActionResult UpdateBlog()
        {
            return View();
        }

        public IActionResult DeleteBlog(string id)
        {
            CRUD.Delete($"Blog/{id}");
            return RedirectToAction("Blogs", "Blog");
        }

        public IActionResult UpdateBlog(Blogs blogs, string Header, string Image, string blogInformation, DateTime DateTime)
        {
            blogs.Header = Header;
            blogs.Image = Image;
            blogs.BlogInformation = blogInformation;
            blogs.DateTime = DateTime;
            if (blogs.Id == 0)
            {
                CRUD.Create("Blog", JsonConvert.SerializeObject(blogs));
            }
            else
            {
                CRUD.Update("Blog", JsonConvert.SerializeObject(blogs));
            }
            return RedirectToAction("Blogs", "Blog");
        }
    }
}
