using IT_Consulting_CRM_API.Models;
using IT_Consulting_CRM_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AnswerUserViewModel>> Get()
        {
            List<AnswerUserViewModel> UsersList = new List<AnswerUserViewModel>();
            foreach (var users in _userManager.Users.ToList())
            {
                UsersList.Add(new AnswerUserViewModel(users.Id, users.Email));
            }
            return UsersList;
        }
        [HttpPost]
        public async Task Post([FromBody] LoginViewModel model)
        {
            User user = new User { Email = model.Name, UserName = model.Name, EmailConfirmed = true };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }
        }
        [HttpDelete]
        public async Task Delete(string id)
        {
            if (id != "1")
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                }
            }
        }
    }
}
