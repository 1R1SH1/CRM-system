using IT_Consulting_CRM_Web.Models;
using IT_Consulting_CRM_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private HttpClient httpClient = new HttpClient();
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Users");
        }

        [HttpGet("roles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var streamTask = httpClient.GetStreamAsync("https://localhost:5001/api/Roles/edithGet");
            var repositories = JsonSerializer.Deserialize<List<User>>(await streamTask);
            User user = await _userManager.FindByIdAsync(repositories.ToString());

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();

                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserLogin = user.UserName,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost("changeRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            var streamTask = httpClient.GetStreamAsync("https://localhost:5001/api/Roles/edithPost");
            var repositories = JsonSerializer.Deserialize<List<User>>(await streamTask);
            User user = await _userManager.FindByIdAsync(repositories.ToString());

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index", "Users");
            }

            return NotFound();
        }


    }
}
