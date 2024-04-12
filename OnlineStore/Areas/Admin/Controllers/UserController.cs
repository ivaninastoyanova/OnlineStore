using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Core.Contracts;
using OnlineStore.Infrastructure.Constants;
using OnlineStore.Infrastructure.Data.Models;
using static OnlineStore.Core.Constants.MessageConstants;

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

        [Route("Admin/User/Add/{id}")]
        public async Task<IActionResult> Add(Guid id)
        {
            var user = await userService.GetUserById(id);

            if (user == null)
            {
                TempData[UserMessageError] = "No such user!";
                return RedirectToAction("All", "User");
            }

            if(await userManager.IsInRoleAsync(user, AdministratorConstants.AdminRoleName))
            {
                TempData[UserMessageError] = "User is already Admin!";
                return RedirectToAction("All", "User");
            }

            await userManager.AddToRoleAsync(user, AdministratorConstants.AdminRoleName);
            TempData[UserMessageSuccess] = "User added to Admin role!";

            return RedirectToAction("All", "User");
        }

        [Route("Admin/User/Remove/{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await userService.GetUserById(id);

            if (user == null)
            {
                TempData[UserMessageError] = "No such user!";
                return RedirectToAction("All", "User");
            }

            if(!await userManager.IsInRoleAsync(user, AdministratorConstants.AdminRoleName))
            {
                TempData[UserMessageError] = "User is not Admin!";
                return RedirectToAction("All", "User");
            }

            await userManager.RemoveFromRoleAsync(user, AdministratorConstants.AdminRoleName);
            TempData[UserMessageSuccess] = "User removed from Admin role!";

            return RedirectToAction("All", "User");
        }
    }
}
