using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    public class CategoryController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
