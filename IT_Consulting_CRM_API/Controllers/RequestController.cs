using IT_Consulting_CRM_API.Data;
using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_API.Controllers
{
    /// <summary>
    /// Контроллер заявок
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        public static DataContext Context { get; set; }

        public static List<Requests> Requests = new List<Requests>();

        private static HttpClient httpClient { get; set; }

        private static string BotUrl { get; set; }

        private static string PrevDatas { get; set; }

        private static int Update_id { get; set; }

        public RequestController(DataContext dataContext)
        {
            Context = dataContext;
            httpClient = new HttpClient();
            BotUrl = @"https://api.telegram.org/bot5125573757:/";
            Update_id = 0;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] Requests request)
        {
            request.Status = 0;
            await Context.Request.AddAsync(request);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Requests req)
        {
            Requests request = Context.Request.ToList().Find(u => u.Id == req.Id);
            Context.Request.Update(request);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<Requests>> Get(Requests request)
        {
            ReadDataFromBot(request);
            return await Context.Request.ToListAsync();
        }
        /// Получение заявок от бота.
        private void ReadDataFromBot(Requests request)
        {
            string url = $"{BotUrl}getUpdates";
            string r = httpClient.GetStringAsync(url).Result;
            if (PrevDatas != r)
            {
                PrevDatas = r;
                var msgs = JObject.Parse(r)["result"].ToArray();

                foreach (dynamic msg in msgs)
                {
                    string userMessage = msg.message.text;
                    string userId = msg.message.from.id;
                    string useFirstrName = msg.message.from.first_name;

                    DateTime date = new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(msg.message.date));
                    request.Date = date;

                    if (Context.Request.ToList().Find(u => u.Date == request.Date && u.Name == request.Name) == null)
                    {
                        request.Status = 0;
                        Context.Request.AddAsync(request);
                        Context.SaveChangesAsync();

                        url = $"{BotUrl}sendMessage?chat_id={userId}&text=Заявка принята к рассмотрению";
                        r = httpClient.GetStringAsync(url).Result;
                    }
                }
            }
        }
    }
}
