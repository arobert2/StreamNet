using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamNetServer.Entities;
using StreamNetServer.ExtensionMethod;
using StreamNetServer.Models;

namespace StreamNetServer.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;

        public AdminController(UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// Gets the Admin Dashboard
        /// </summary>
        /// <returns>The admin Dashboard View</returns>
        [Authorize(Roles = "administrator")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Creates a new user GET
        /// </summary>
        /// <returns>New user view</returns>
        [Authorize(Roles = "administrator")]
        [HttpGet]
        public IActionResult CreateNewUser()
        {
            return View();
        }
        /// <summary>
        /// Create a new user POST
        /// </summary>
        /// <param name="createUserViewModel">New user form</param>
        /// <returns>Redirects to user list</returns>
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserViewModel createUserViewModel)
        {
            //Check default validation
            if (!ModelState.IsValid)
                return View(ModelState);
            //Check if passwords match
            if (createUserViewModel.Password != createUserViewModel.PasswordCheck)
            {
                ModelState.AddModelError("PasswordMatch", "Passwords do not match.");
                return View(ModelState);
            }
            //Convert to AppIdentityUser
            var user = Mapper.Map<AppIdentityUser>(createUserViewModel);
            //Attempt to create user
            var res = await _userManager.CreateAsync(user, createUserViewModel.Password);
            //If succeded go to Admin page
            if (res.Succeeded)
                return RedirectToAction(nameof(Index));
            //Add all errors to ModelState
            foreach (var e in res.Errors)
                ModelState.AddModelError(e.Code, e.Description);
            //Return view with model state.
            return View(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.OrderBy(u => u.UserName);
            var userprofiles = new List<UserProfileViewModel>();
            foreach(var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                var newuserprof = Mapper.Map<UserProfileViewModel>(u);
                newuserprof = Mapper.Map(roles, typeof(List<string>), typeof(UserProfileViewModel)) as UserProfileViewModel;
                userprofiles.Add(newuserprof);
            }
            return View(userprofiles);
        }

        [HttpGet]
        public IActionResult SetPermissions(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            var userprofile = Mapper.Map<SetPermissionsViewModel>(user);
            return View(userprofile);
        }

        [HttpPost]
        public async Task<IActionResult> SetUserAttributes([FromForm] SetPermissionsViewModel setpermvm)
        {
            if (!ModelState.IsValid)
                return View(setpermvm);

            var user = _userManager.Users.FirstOrDefault(u => u.Id == setpermvm.Id);

        }
    }
}