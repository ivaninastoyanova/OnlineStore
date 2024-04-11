﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services
{
    public class CartService : ICartService
    {
        private readonly OnlineStoreDbContext db;

        public CartService(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<Cart> GetCartByUserId(string email)
        {
            Cart? result = new Cart();

            result = await db.Carts
                .Include(x => x.Comics)
                .Where(c => c.User.Email == email)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                result = new Cart();

                var user = db.Users.FirstOrDefault(u => u.Email == email);

                user.Cart = result;

                db.Carts.Add(result);

                await db.SaveChangesAsync();

                return result;
            }

            return result;
        }

        public async Task Add(Cart cart, int id)
        {
            var comic = await db.Comics.FindAsync(id);

            cart.Comics.Add(comic);

            await db.SaveChangesAsync();
        }

    }
}
