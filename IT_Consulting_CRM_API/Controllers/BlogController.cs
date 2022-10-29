using IT_Consulting_CRM_API.Data;
using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public static DataContext Context { get; set; }
        public BlogController(DataContext dataContext)
        {
            Context = dataContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Blogs data)
        {
            await Context.Blog.AddAsync(data);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Blogs data)
        {
            Context.Blog.Update(data);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Blogs>> Get()
        {
            return await Context.Blog.ToListAsync();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Blogs blog = Context.Blog.ToList().Find(u => u.Id == id);
            Context.Blog.Remove(blog);
            await Context.SaveChangesAsync();
            return Ok();
        }
    }
}
