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
    public class UserTests
    {
        private DbContextOptions<OnlineStoreDbContext> dbOptions;
        private OnlineStoreDbContext dbContext;

        private IUserService userService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase("OnlineStoreInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new OnlineStoreDbContext(this.dbOptions, false);


            this.dbContext.Database.EnsureCreated();

            DataSeeder.Seed(this.dbContext);

            this.userService = new UserService(this.dbContext);
        }

        [Test]
        public async Task GetUserById()
        {
            var user = await this.userService.GetUserById(DataSeeder.ApplicationUser.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual(DataSeeder.ApplicationUser.Id, user.Id);
        }

        [Test]
        public async Task GetUserByIdInvalidId()
        {
            var user = await this.userService.GetUserById(Guid.NewGuid());

            Assert.IsNull(user);
        }

        [Test]
        public async Task GetUsers()
        {
            var users = await this.userService.GetUsers();

            Assert.AreEqual(2, users.Count());
            Assert.IsTrue(users.Any(u => u.Id == DataSeeder.ApplicationUser.Id));
            Assert.IsTrue(users.Any(u => u.Id == DataSeeder.AdminUser.Id));
        }
    }
}
