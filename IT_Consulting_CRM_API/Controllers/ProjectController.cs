using IT_Consulting_CRM_API.Methods;
using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProjectController : ControllerBase
    {
        public static ProjectCore Core { get; set; }
        public ProjectController(ProjectCore core)
        {
            Core = core;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Projects data)
        {
            await Core.PostProject(data);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Projects data)
        {
            await Core.PutProject(data);
            return Ok();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Projects>> Get()
        {
            return await Core.GetProject();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Core.DeleteProject(id);
            return Ok();
        }
    }
}
