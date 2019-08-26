using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamNetServer.Data;
using StreamNetServer.Entities;
using StreamNetServer.Models;

namespace StreamNetServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly UserManager<AppIdentityUser> _userManager;

        public AccountController(
            SignInManager<AppIdentityUser> signInManager,
            UserManager<AppIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login)
        {
            //Return if ModelState is not valid.
            if (!ModelState.IsValid)
                return View(login);

            //Attempt to find user
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == login.UserName);
            //Return BadLogin if username not found.
            if(user == null)
            {
                ModelState.AddModelError("BadLogin", "Username or Password is incorrect");
                return View(login);
            }
            //Attempt to login
            var res = await _signInManager.PasswordSignInAsync(user, login.Password, login.StayLoggedIn, false);
            //Return to Home if logged in.
            if (res.Succeeded)
                return RedirectToAction("Index", "Home");
            //Check for all errors. Add Errors to model state.
            if (res.IsLockedOut)
                ModelState.AddModelError("Locked", "Account is locked. Please contact an administrator.");
            if (res.IsNotAllowed)
                ModelState.AddModelError("Unauthorized", "This action is not allowed. Please contact an administrator.");
            if (res.RequiresTwoFactor)
                ModelState.AddModelError("TwoFactorRequired", "Two factor authentication is required.");
            if (!res.IsLockedOut && !res.IsNotAllowed && !res.RequiresTwoFactor)
                ModelState.AddModelError("BadLogin", "Username or Password is incorrect");
            //Return ModelState in view.
            return View(login);

        }
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var profileViewModel = Mapper.Map<UserProfileViewModel>(user);
            return View(profileViewModel);
        }
        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}