using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static OnlineStore.Infrastructure.Constants.AdministratorConstants;

namespace OnlineStore.Areas.Admin.Controllers
{

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class BaseAdminController : Controller
    {

    }
}
