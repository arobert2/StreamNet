using Microsoft.AspNetCore.Identity;
using StreamNet.Server.DomainEntities.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNet.Server
{
    public class SeedData
    {
        private const string DEFAULT_ADMIN_ROLE_NAME = "administrator";
        private const string DEFAULT_USER_ROLE_NAME = "user";
        private const string DEFAULT_USER_NAME = "administrator";
        private const string DEFAULT_PASSWORD = "ChangeMe2019!";

        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public SeedData(
            UserManager<AppIdentityUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDatabase()
        {
            if (!AdminRoleExists())
            {
                var createRole = await CreateAdminRole();
                if (!createRole.Succeeded)
                    throw new Exception("Failed to create default admin role.");
            }

            if(!UserRoleExists())
            {
                var creatRole = await CreateUserRole();
                if (!creatRole.Succeeded)
                    throw new Exception("Failed to create default user role.");
            }

            if (!UserExists())
            {
                var createUser = await CreateUser();
                if (!createUser.Succeeded)
                    throw new Exception("Failed to create default user.");
            }

            if (!await UserRoleApplied())
            {
                var addRole = await AddRoleToUser();
                if (!addRole.Succeeded)
                    throw new Exception("Failed to add role to user.");
            }               
        }

        private bool AdminRoleExists()
        {
            var roles = _roleManager.Roles.FirstOrDefault(r => r.Name == DEFAULT_ADMIN_ROLE_NAME);
            return roles != null;
        }

        private bool UserRoleExists()
        {
            var roles = _roleManager.Roles.FirstOrDefault(r => r.Name == DEFAULT_USER_ROLE_NAME);
            return roles != null;
        }

        private bool UserExists()
        {
            var users = _userManager.Users.FirstOrDefault(u => u.UserName == DEFAULT_USER_NAME);
            return users != null;
        }

        private async Task<bool> UserRoleApplied()
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == DEFAULT_USER_NAME);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains(DEFAULT_ADMIN_ROLE_NAME);
        }

        private async Task<IdentityResult> CreateUser()
        {
            var user = new AppIdentityUser()
            {
                UserName = DEFAULT_USER_NAME,
                Email = DEFAULT_USER_NAME + "@StreemNet.com"
            };

            var res = await _userManager.CreateAsync(user, DEFAULT_PASSWORD);
            return res;
        }

        private async Task<IdentityResult> CreateAdminRole()
        {
            var role = new IdentityRole<Guid>();
            role.Name = DEFAULT_ADMIN_ROLE_NAME;
            var res = await _roleManager.CreateAsync(role);
            return res;
        }

        private async Task<IdentityResult> CreateUserRole()
        {
            var role = new IdentityRole<Guid>();
            role.Name = DEFAULT_USER_ROLE_NAME;
            var res = await _roleManager.CreateAsync(role);
            return res;
        }

        private async Task<IdentityResult> AddRoleToUser()
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == DEFAULT_USER_NAME);
            var res = await _userManager.AddToRoleAsync(user, DEFAULT_ADMIN_ROLE_NAME);
            return res;
        }
    }
}
