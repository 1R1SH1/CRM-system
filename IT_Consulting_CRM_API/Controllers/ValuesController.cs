using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var nameIdentifier1 = this.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var nameIdentifier2 = this.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Role);

            return new string[] { nameIdentifier1?.Value, nameIdentifier2?.Value };
        }
    }
}
