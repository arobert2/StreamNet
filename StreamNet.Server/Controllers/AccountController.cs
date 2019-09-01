using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamNet.Server.DomainEntities.Entities;
using StreamNet.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Controllers
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
        [Authorize(Roles = "user, administrator")]
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var profileViewModel = Mapper.Map<UserProfileViewModel>(user);
            return View(profileViewModel);
        }
        [Authorize(Roles = "user, administrator")]
        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }
        [Authorize(Roles = "user, administrator")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        [Authorize(Roles = "user, administrator")]
        [HttpGet]
        public async Task<IActionResult> UpdateMyProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            if (user == null)
                return NotFound();
            var userprofviewmodel = Mapper.Map<UserProfileViewModel>(user);
            userprofviewmodel = Mapper.Map(roles, typeof(List<string>), typeof(UserProfileViewModel)) as UserProfileViewModel;
            return View(userprofviewmodel);
        }
        [Authorize(Roles = "user, administrator")]
        [HttpPost]
        public async Task<IActionResult> UpdateMyProfile([FromForm] UserProfileViewModel userprofvm)
        {
            if (!ModelState.IsValid)
                return View(userprofvm);

            var user = _userManager.Users.FirstOrDefault(u => u.Id == userprofvm.Id);
            user = Mapper.Map(userprofvm, typeof(UserProfileViewModel), typeof(AppIdentityUser)) as AppIdentityUser;
            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
                return RedirectToAction(nameof(MyProfile));

            foreach (var e in res.Errors)
                ModelState.AddModelError(e.Code, e.Description);

            return View(userprofvm);
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction(nameof(Login));
        }
    }
}