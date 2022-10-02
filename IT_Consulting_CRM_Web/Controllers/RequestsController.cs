using IT_Consulting_CRM_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<Requests> _requests { get; set; }
        private static List<Requests> _rawRequests { get; set; }

        // Диапазон выводимых дат
        private static int? Diapazon { get; set; }

        // Начальная дата
        public static DateTime Start { get; set; }

        // Конечная дата
        public static DateTime End { get; set; }
        public RequestsController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _rawRequests = new List<Requests>();
            _requests = new List<Requests>();
        }

        public IActionResult Worktable()
        {
            if (Diapazon == null) Diapazon = 0;
            Show();
            Console.WriteLine(_rawRequests.Count);
            return View();
        }

        public IActionResult Today()
        {
            Diapazon = 0;
            Show();
            return RedirectToAction("Worktable", "Requests");
        }
        public IActionResult Tomorrow()
        {
            Diapazon = 1;
            Show();
            return RedirectToAction("Worktable", "Requests");
        }
        public IActionResult Week()
        {
            Diapazon = 2;
            Show();
            return RedirectToAction("Worktable", "Requests");
        }
        public IActionResult Month()
        {
            Diapazon = 3;
            Show();
            return RedirectToAction("Worktable", "Requests");
        }
        public IActionResult StartDate(DateTime start)
        {
            Start = start;
            Diapazon = 4;
            Show();
            return RedirectToAction("Worktable", "Requests");
        }
        public IActionResult EndDate(DateTime end)
        {
            End = end;
            Diapazon = 4;
            Show();
            return RedirectToAction("Worktable", "Requests");
        }

        // Формирование коллекции на основании диапазона дат
        private void Show()
        {
            _rawRequests = JsonConvert.DeserializeObject<List<Requests>>(CRUD.Read("Request").ToString());
            _requests = new List<Requests>();
            SetDiapazon();
            for (int i = 0; i < _rawRequests.Count; i++)
            {
                if (_rawRequests[i].Date >= Start && _rawRequests[i].Date < End.AddDays(1))
                {
                    _requests.Add(_rawRequests[i]);
                }
            }
        }

        // Установка диапазона дат
        private void SetDiapazon()
        {
            if (Diapazon == 0) { Start = DateTime.Today; End = DateTime.Today; }
            if (Diapazon == 1) { Start = DateTime.Today.AddDays(-1); End = DateTime.Today.AddDays(-1); }
            if (Diapazon == 2) { Start = GetFirstDayOfWeek(DateTime.Today); End = DateTime.Today; }
            if (Diapazon == 3) { Start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); ; End = DateTime.Today; }
            if (Diapazon == 4)
            {
                if (End < Start) End = Start;
            }
        }

        [HttpPost]
        public IActionResult SetStatus(string Status)
        {
            _rawRequests = JsonConvert.DeserializeObject<List<Requests>>(CRUD.Read("Request").ToString());
            string[] output = ParseStatus(Status);
            Requests request = _rawRequests.Find(u => u.Id == int.Parse(output[0]));
            request.Status = int.Parse(output[1]);
            CRUD.Update("Request", JsonConvert.SerializeObject(request));
            return RedirectToAction("Worktable", "Requests");
        }

        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
        }

        private static string[] ParseStatus(string status)
        {
            string[] patt = new string[1];
            patt[0] = ",";
            return status.Split(patt, StringSplitOptions.None);
        }
    }
}
