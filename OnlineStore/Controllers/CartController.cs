using OnlineStore.Core.Contracts;

namespace OnlineStore.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

    }
}
