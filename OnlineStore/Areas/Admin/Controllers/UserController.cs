using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Core.Contracts;
using OnlineStore.Infrastructure.Constants;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(
            IUserService _userService,
            UserManager<ApplicationUser> userManager)
        {
            userService = _userService;
            this.userManager = userManager;
        }

        [Route("Admin/User/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<ApplicationUser> users = await userService.GetUsers();

            foreach (ApplicationUser user in users)
            {
                if (await userManager.IsInRoleAsync(user, AdministratorConstants.AdminRoleName))
                {
                    user.IsAdmin = true;
                }
            }

            return this.View(users);
        }
    }
}
