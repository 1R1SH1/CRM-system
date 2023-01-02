using IT_Consulting_CRM_API.Methods;
using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RequestController : ControllerBase
    {
        public static RequestCore Core { get; set; }

        public static List<Requests> Requests = new List<Requests>();

        //private static HttpClient? httpClient { get; set; }

        //private static string? BotUrl { get; set; }

        //private static string? PrevDatas { get; set; }

        //private static int Update_id { get; set; }
        public RequestController(RequestCore core)
        {
            Core = core;
            //httpClient = new HttpClient();
            //BotUrl = @"https://api.telegram.org/bot5207281250:AAHsxrjSznifTZZ5jwMXFlDNjZl4s-FjhUk";
            //Update_id = 0;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] Requests request)
        {
            request.Status = 0;
            await Core.PostRequest(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Requests req)
        {
            await Core.PutRequest(req);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Requests>> Get()
        {
            //ReadDataFromBot();
            return await Core.GetRequest();
        }

        //private static void ReadDataFromBot()
        //{
        //    string url = $"{BotUrl}";
        //    string r = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        //    if (PrevDatas != r)
        //    {
        //        PrevDatas = r;
        //        var msgs = JObject.Parse(r)["result"].ToArray();

        //        foreach (dynamic msg in msgs)
        //        {
        //            string userMessage = msg.message.text;
        //            string userId = msg.message.from.id;
        //            string useFirstrName = msg.message.from.first_name;

        //            Requests request = new();
        //            DateTime date = new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(msg.message.date));
        //            request.Date = date;

        //            request.Status = 0;
        //                Context.Request.AddAsync(request);
        //                Context.SaveChangesAsync();

        //                url = $"{BotUrl}sendMessage?chat_id={userId}&text=Заявка принята к рассмотрению";
        //                r = httpClient.GetStringAsync(url).Result;
        //        }
        //    }
        //}
    }
}
