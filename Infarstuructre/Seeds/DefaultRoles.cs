using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using static Domain.Entity.BooksHelper;

namespace Infarstuructre.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync( RoleManager<IdentityRole> roleManager)
        {
          
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(BooksHelper.Roles.SuperAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(BooksHelper.Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(BooksHelper.Roles.Basic.ToString()));

            }

        }
    }
}
