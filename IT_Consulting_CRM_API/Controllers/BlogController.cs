using IT_Consulting_CRM_API.Methods;
using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class BlogController : ControllerBase
    {
        public static BlogCore Core { get; set; }
        public BlogController(BlogCore core)
        {
            Core = core;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Blogs data)
        {
            await Core.PostBlog(data);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Blogs data)
        {
            await Core.PutBlog(data);
            return Ok();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Blogs>> Get()
        {
            return await Core.GetBlog();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Core.DeleteBlog(id);
            return Ok();
        }
    }
}
