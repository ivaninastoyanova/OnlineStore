using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Details()
        {
            Cart cart = new Cart();

            var email = GetEmail(this.User);

            if (email == null)
            {
                TempData["Error"] = "No user found!";

                return RedirectToAction("Index", "Home");
            }

            cart = await cartService.GetCartByUserId(email);

            return View(cart);
        }
    }
}
