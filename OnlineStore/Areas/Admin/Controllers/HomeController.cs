using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
