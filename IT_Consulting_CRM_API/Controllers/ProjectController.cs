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
    /// <summary>
    /// Контроллер проектов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public static DataContext Context { get; set; }
        public ProjectController(DataContext dataContext)
        {
            Context = dataContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Projects data)
        {
            await Context.Project.AddAsync(data);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Projects data)
        {
            Context.Project.Update(data);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Projects>> Get()
        {
            return await Context.Project.ToListAsync();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Projects project = Context.Project.ToList().Find(u => u.Id == id);
            Context.Project.Remove(project);
            await Context.SaveChangesAsync();
            return Ok();
        }
    }
}
