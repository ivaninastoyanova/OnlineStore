using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IComicService comicService;

        public CartController(ICartService cartService,
            IComicService comicService)
        {
            this.cartService = cartService;
            this.comicService = comicService;
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

        public async Task<IActionResult> Purchase(int id)
        {
            var email = GetEmail(this.User);

            if (email == null)
            {
                TempData["Error"] = "No user found!";

                return RedirectToAction("Index", "Home");
            }

            var comic = await comicService.ComicExistsAsync(id);

            if(!comic)
            {
                TempData["Error"] = "Comic not found!";

                return RedirectToAction("All", "Comic");
            }

            Cart cart = await cartService.GetCartByUserId(email);

            await cartService.Add(cart, id);

            TempData["Success"] = "Comic added to cart!";

            return RedirectToAction("All", "Comic");
        }

    }
}
