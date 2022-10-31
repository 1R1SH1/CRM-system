using IT_Consulting_CRM_Web.Models;
using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowRequest()
        {
            return View();
        }

        public IActionResult SendRequest(string name, string surName, string email, string information)
        {
            Requests request = new(0, name, surName, email, information);
            string url = @"https://localhost:44390/api/Request";
            CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json")).Result.ToString();
            Console.WriteLine(CRUD.Token);
            if (CRUD.Token.Contains("200"))
            {
                return RedirectToAction(nameof(RequestDone));
            }
            else
            {
                return RedirectToAction(nameof(RequestError));
            }
        }

        public IActionResult RequestDone()
        {
            return View();
        }
        public IActionResult RequestError()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}