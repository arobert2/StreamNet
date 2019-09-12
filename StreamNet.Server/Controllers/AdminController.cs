using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamNet.DomainEntities.Data;
using StreamNet.DomainEntities.Entities;
using StreamNet.ExtensionsMethods;
using StreamNet.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public AdminController(
            UserManager<AppIdentityUser> userManager,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets the Admin Dashboard
        /// </summary>
        /// <returns>The admin Dashboard View</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Creates a new user GET
        /// </summary>
        /// <returns>New user view</returns>
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
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromForm] CreateUserViewModel createUserViewModel)
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
        /// <summary>
        /// Set user permissions
        /// </summary>
        /// <param name="setpermvm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetPermissions([FromForm] SetPermissionsViewModel setpermvm)
        {
            if (!ModelState.IsValid)
                return View(setpermvm);

            var user = _userManager.Users.FirstOrDefault(u => u.Id == setpermvm.Id);
            var roles = await _userManager.GetRolesAsync(user);
            IdentityResult resPerm = new IdentityResult();
            if (setpermvm.Administrator)
            {
                if (!roles.Contains("administrator"))
                    resPerm = await _userManager.AddToRoleAsync(user, "administrator");
            }
            else
            {
                if (roles.Contains("administrator"))
                    resPerm = await _userManager.RemoveFromRoleAsync(user, "administrator");
            }

            if (!resPerm.Succeeded)
                foreach (var e in resPerm.Errors)
                    ModelState.AddModelError(e.Code, e.Description);

            IdentityResult resLock = new IdentityResult();
            var locked = await _userManager.IsLockedOutAsync(user);
            if(setpermvm.Locked)
            {
                if (locked)
                    resLock = await _userManager.SetLockoutEnabledAsync(user, true);
            }
            else
            {
                if (!locked)
                    resLock = await _userManager.SetLockoutEnabledAsync(user, false);
            }

            if (!resLock.Succeeded)
                foreach (var e in resLock.Errors)
                    ModelState.AddModelError(e.Code, e.Description);

            if (!resLock.Succeeded || !resPerm.Succeeded)
                return View(setpermvm);

            return Redirect(nameof(UserList));

        }
        [HttpGet]
        public IActionResult Messages(int index = 0, int total = 10)
        {
            var messages = _dbContext.AdminMessages.Skip(index).Take(total);
            return View(messages);
        }
    }
}