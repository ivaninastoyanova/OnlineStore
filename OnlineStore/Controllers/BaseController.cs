﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Constants;
using System.Security.Claims;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public static string? GetId(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string? GetEmail(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }

        public static bool IsAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole(AdministratorConstants.AdminRoleName);
        }
    }
}
