using IT_Consulting_CRM_API.Models;
using IT_Consulting_CRM_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> PostAsync([FromBody] LoginViewModel authRequest,
            [FromServices] IJwtSigningEncodingKey signingEncodingKey,
            [FromServices] IJwtEncryptingEncodingKey encryptingEncodingKey)
        {
            if ((await _signInManager.PasswordSignInAsync(authRequest.Username, authRequest.Password, false, false)).Succeeded)
            {
                User user = await _userManager.FindByNameAsync(authRequest.Username);
                string role;
                if (await _userManager.IsInRoleAsync(user, "admin")) { role = "admin"; }
                else if (await _userManager.IsInRoleAsync(user, "user")) { role = "user"; }
                else { role = "uncknown"; }

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, authRequest.Username),
                    new Claim(ClaimTypes.Role, role)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(
                    issuer: "DiplomApi",
                    audience: "DiplomClient",
                    subject: new ClaimsIdentity(claims),
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(5),
                    issuedAt: DateTime.Now,
                    signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm),
                    encryptingCredentials: new EncryptingCredentials(
                        encryptingEncodingKey.GetKey(),
                        encryptingEncodingKey.SigningAlgorithm,
                        encryptingEncodingKey.EncryptingAlgorithm));

                var jwtToken = tokenHandler.WriteToken(token);
                return jwtToken;
            }
            else
            {
                return "Unauthorize";
            }
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            string defaultRole = "user";

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };

                // add user
                var createResult = await _userManager.CreateAsync(user, model.Password);

                // set role to user
                var addToRole = await _userManager.AddToRoleAsync(user, defaultRole);

                if (createResult.Succeeded && addToRole.Succeeded)
                {
                    // set cookies
                    await _signInManager.SignInAsync(user, false);
                }

                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError(String.Empty, identityError.Description);
                    }
                }
            }

            return Ok(model);
        }
    }
}
