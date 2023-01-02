using IT_Consulting_CRM_API.Methods;
using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ServicesController : ControllerBase
    {
        public static ServiceCore Core { get; set; }
        public ServicesController(ServiceCore core)
        {
            Core = core;
        }
        [HttpPost]
        public async Task Post([FromBody] Services service)
        {
            await Core.PostService(service);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Services service)
        {
            await Core.PutService(service);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Services>> Get()
        {
            return await Core.GetService();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Core.DeleteService(id);
            return Ok();
        }
    }
}
