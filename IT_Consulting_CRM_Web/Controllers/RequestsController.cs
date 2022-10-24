using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IT_Consulting_CRM_Web.Controllers
{
    public class RequestsController : Controller
    {
        public static List<RequestMessage> Requests { get; set; }
        public static List<RequestMessage> RawRequests { get; set; }
        private static int? Diapazon { get; set; }
        public static DateTime Start { get; set; }
        public static DateTime End { get; set; }
        public RequestsController()
        {
            RawRequests = new List<RequestMessage>();
            Requests = new List<RequestMessage>();
        }

        public IActionResult Worktable()
        {
            if (Diapazon == null) Diapazon = 0;
            Show();
            Console.WriteLine(RawRequests.Count);
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
        /// <summary>
        /// Формирование коллекции на основании диапазона дат.
        /// </summary>
        private void Show()
        {
            RawRequests = JsonConvert.DeserializeObject<List<RequestMessage>>(CRUD.Read("Request"));
            Requests = new List<RequestMessage>();
            SetDiapazon();
            for (int i = 0; i < RawRequests.Count; i++)
            {
                if (RawRequests[i].Date >= Start && RawRequests[i].Date < End.AddDays(1))
                {
                    Requests.Add(RawRequests[i]);
                }
            }
        }
        /// <summary>
        /// Установка диапазона дат.
        /// </summary>
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
            RawRequests = JsonConvert.DeserializeObject<List<RequestMessage>>(CRUD.Read("Request"));
            string[] output = ParseStatus(Status);
            RequestMessage request = RawRequests.Find(u => u.Id == int.Parse(output[0]));
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
