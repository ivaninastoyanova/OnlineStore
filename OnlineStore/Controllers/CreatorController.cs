using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    public class CreatorController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
