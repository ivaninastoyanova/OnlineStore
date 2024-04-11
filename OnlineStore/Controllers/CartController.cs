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

            var comicInCart = await cartService.ComicExistsInCart(cart, id);

            if (comicInCart)
            {
                TempData["Error"] = "Comic already in cart!";

                return RedirectToAction("All", "Comic");
            }
            await cartService.Add(cart, id);

            TempData["Success"] = "Comic added to cart!";

            return RedirectToAction("All", "Comic");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var email = GetEmail(this.User);

            if (email == null)
            {
                TempData["Error"] = "No user found!";

                return RedirectToAction("Index", "Home");
            }

            Cart cart = await cartService.GetCartByUserId(email);

            var comicInCart = await cartService.ComicExistsInCart(cart, id);

            if (!comicInCart)
            {
                TempData["Error"] = "No such comic in the cart!";

                return RedirectToAction("All", "Comic");
            }

            await cartService.Remove(cart, id);

            return RedirectToAction("Details");
        }

        public async Task<IActionResult> Order(int id)
        {
            var email = GetEmail(this.User);

            if (email == null)
            {
                TempData["Error"] = "No user found!";

                return RedirectToAction("Index", "Home");
            }

            var cartExist = await cartService.CartExists(id);

            if (!cartExist)
            {
                TempData["Error"] = "No cart found!";

                return RedirectToAction("All", "Comic");
            }

            Cart cart = await cartService.GetCartByUserId(email);

            if(cart == null || cart.Id != id)
            {
                TempData["Error"] = "No cart found!";

                return RedirectToAction("All", "Comic");
            }


            if (cart.Comics.Count == 0)
            {
                TempData["Error"] = "No items in cart!";

                return RedirectToAction("All", "Comic");
            }

            await cartService.EmptyCart(cart.Id);

            return View();
        }

    }
}
