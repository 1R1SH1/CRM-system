﻿using IT_Consulting_CRM_API.Models;
using IT_Consulting_CRM_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IT_Consulting_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        public UserManager<User> _userManager;
        public SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl) && !string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                }
            }

            ModelState.AddModelError(String.Empty, "Wrong user or password");
            return View(model);
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

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            // remove cookies
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
