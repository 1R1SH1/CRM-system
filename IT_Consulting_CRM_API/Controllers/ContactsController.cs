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
    /// Контроллер контактов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public static DataContext Context { get; set; }
        public ContactsController(DataContext dataContext)
        {
            Context = dataContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Contacts contacts)
        {
            Context.Contact.Add(contacts);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(Contacts contacts)
        {
            Context.Contact.Update(contacts);
            await Context.SaveChangesAsync();
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Contacts>> Get()
        {
            return await Context.Contact.ToListAsync();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Contacts contact = Context.Contact.ToList().Find(u => u.Id == id);
            Context.Contact.Remove(contact);
            await Context.SaveChangesAsync();
            return Ok();
        }
    }
}
