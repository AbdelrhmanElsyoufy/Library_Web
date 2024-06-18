using Domain.Constants;
using Domain.Entity;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedSuperAdminAsync
            (UserManager<ApplicationUser> userManager ,
            RoleManager<IdentityRole> roleManager)
        {

            var defaultUser = new ApplicationUser
            {
                Email = BooksHelper.Email,
                UserName = BooksHelper.UserName,
                Name = BooksHelper.Name,
                ImageUser = BooksHelper.Image,
                ActiveUser = true,
                EmailConfirmed = true
            };

            var user = userManager.FindByEmailAsync(defaultUser.Email);
            if(user.Result == null)
            {
               await userManager.CreateAsync(defaultUser, BooksHelper.Password);
               await userManager.AddToRoleAsync(defaultUser, BooksHelper.Roles.SuperAdmin.ToString());

            }

            await roleManager.SeedClaimsAsync();
        }


        public static async Task SeedAdminAsync
         (UserManager<ApplicationUser> userManager,
         RoleManager<IdentityRole> roleManager)
        {

            var defaultUser = new ApplicationUser
            {
                Email = BooksHelper.AdminEmail,
                UserName = BooksHelper.AdminUserName,
                Name = BooksHelper.AdminName,
                ImageUser = BooksHelper.Image,
                ActiveUser = true,
                EmailConfirmed = true
            };

            var user = userManager.FindByEmailAsync(defaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(defaultUser, BooksHelper.AdminPassword);
                await userManager.AddToRoleAsync(defaultUser, BooksHelper.Roles.Admin.ToString());

            }


        }


        public static async Task SeedBasicAsync
         (UserManager<ApplicationUser> userManager,
         RoleManager<IdentityRole> roleManager)
        {

            var defaultUser = new ApplicationUser
            {
                Email = BooksHelper.BasicEmail,
                UserName = BooksHelper.BasicUserName,
                Name = BooksHelper.BasicName,
                ImageUser = BooksHelper.Image,
                ActiveUser = true,
                EmailConfirmed = true
            };

            var user = userManager.FindByEmailAsync(defaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(defaultUser, BooksHelper.BasicPassword);
                await userManager.AddToRoleAsync(defaultUser, BooksHelper.Roles.Basic.ToString());

            }


        }

        public static async Task SeedClaimsAsync(this RoleManager<IdentityRole> roleManager)
        {
           var superadminRole = await roleManager.FindByNameAsync(BooksHelper.Roles.SuperAdmin.ToString());
            var modules = Enum.GetValues(typeof(BooksHelper.PermissionModuleName));
            foreach(var module in modules)
            {
               await  roleManager.AddPermissionClaimsAsync(superadminRole, module.ToString());
            }
        }

        public static async Task AddPermissionClaimsAsync(this RoleManager<IdentityRole> roleManager, IdentityRole role , string module)
        {
            var allClamis = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsFromModule(module);
            foreach(var permission in allPermissions)
            {
                if(!allClamis.Any(claim=> claim.Type == BooksHelper.Permission && claim.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(BooksHelper.Permission, permission));
                }
            }
        }
    }
}
