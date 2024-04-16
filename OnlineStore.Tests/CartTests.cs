using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Services;
using OnlineStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests
{
    public class CartTests
    {
        private DbContextOptions<OnlineStoreDbContext> dbOptions;
        private OnlineStoreDbContext dbContext;

        private ICartService cartService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase("OnlineStoreInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new OnlineStoreDbContext(this.dbOptions, false);


            this.dbContext.Database.EnsureCreated();

            DataSeeder.Seed(this.dbContext);

            this.cartService = new CartService(this.dbContext);
        }


        [Test]
        public async Task Add()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.ApplicationUser.Email);

            var comicId = 1;
            var comic = this.dbContext.Comics.Find(comicId);

            await this.cartService.Add(cart, comicId);

            Assert.IsTrue(cart.Comics.Count == 1);
            Assert.IsTrue(cart.Comics.Contains(comic));
        }

        [Test]
        public async Task GetCartByUserId()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.ApplicationUser.Email);

            Assert.IsNotNull(cart);
            Assert.IsTrue(cart.Comics.Count == 1);
        }

        [Test]
        public async Task Remove()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.ApplicationUser.Email);

            var comicId = 1;
            var comic = this.dbContext.Comics.Find(comicId);

            await this.cartService.Remove(cart, comicId);

            Assert.IsTrue(cart.Comics.Count == 0);
            Assert.IsFalse(cart.Comics.Contains(comic));
        }

        [Test]
        public async Task ComicExistsInCart()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.ApplicationUser.Email);

            var comicId = 1;

            var result = await this.cartService.ComicExistsInCart(cart, comicId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ComicExistInCartIsFalse()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.ApplicationUser.Email);

            var comicId = 2;

            var result = await this.cartService.ComicExistsInCart(cart, comicId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ComicExistInCartInvalidId()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.ApplicationUser.Email);

            var comicId = 44;

            var result = await this.cartService.ComicExistsInCart(cart, comicId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EmptyCartHasItems()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.AdminUser.Email);
            cart.Comics.Add(this.dbContext.Comics.Find(2));

            await this.cartService.EmptyCart(cart.Id);

            Assert.IsTrue(cart.Comics.Count == 0);
        }

        [Test]
        public async Task EmptyCartNoItems()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.AdminUser.Email);

            await this.cartService.EmptyCart(cart.Id);

            Assert.IsTrue(cart.Comics.Count == 0);
        }

        [Test]
        public async Task CartExists()
        {
            var cart = await this.cartService.GetCartByUserId(DataSeeder.AdminUser.Email);

            var result = await this.cartService.CartExists(cart.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CartDoesNotExist()
        {
            var result = await this.cartService.CartExists(44);

            Assert.IsFalse(result);
        }
    }
}
