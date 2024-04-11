using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Contracts
{
    public interface ICartService
    {
        public Task<Cart> GetCartByUserId(string email);

        public Task Add(Cart cart, int id);

        public Task Remove(Cart cart, int id);

        public Task<bool> ComicExistsInCart(Cart cart, int id);

        public Task EmptyCart(int id);

        public Task<bool> CartExists(int id);

    }
}
