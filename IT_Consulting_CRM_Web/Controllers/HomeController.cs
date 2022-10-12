using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Sendrequest(Requests request)
        //{
        //    string url = @"https://localhost:5001/api/Request";
        //    //CRUD.Token = httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request),
        //    //    Encoding.UTF8, "application/json")).Result.ToString();
        //    Console.WriteLine(/*CRUD.Token*/);
        //    //if (/*CRUD.Token.Contains("200")*/)
        //    //{
        //    //    return RedirectToAction(nameof(RequestDone));
        //    //}
        //    //else
        //    //{
        //    //    return RedirectToAction(nameof(RequestError));
        //    //}
        //}
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