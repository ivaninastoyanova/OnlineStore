using Microsoft.AspNetCore.Identity;
using OnlineStore.Infrastructure.Constants;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedAdmin(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (userManager != null
                && roleManager != null
                && await roleManager.RoleExistsAsync(AdministratorConstants.AdminRoleName) == false)
            {
                var role = new IdentityRole<Guid>(AdministratorConstants.AdminRoleName);
                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByEmailAsync("admin@mail.com");

                if (admin != null)
                {
                    await userManager.AddToRoleAsync(admin, role.Name);
                }
        }

    }
    }
}
