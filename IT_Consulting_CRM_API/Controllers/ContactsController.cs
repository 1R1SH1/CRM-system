using IT_Consulting_CRM_API.Methods;
using IT_Consulting_CRM_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ContactsController : ControllerBase
    {
        public static ContactCore Core { get; set; }
        public ContactsController(ContactCore core)
        {
            Core = core;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Contacts contacts)
        {
            await Core.PostContact(contacts);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(Contacts contacts)
        {
            await Core.PutContact(contacts);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Contacts>> Get()
        {
            return await Core.GetContact();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Core.DeleteContact(id);
            return Ok();
        }
    }
}
