using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Reviews;
using OnlineStore.Core.Services;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests
{
    public class ReviewTests
    {
        private DbContextOptions<OnlineStoreDbContext> dbOptions;
        private OnlineStoreDbContext dbContext;

        private IReviewService reviewService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase("OnlineStoreInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new OnlineStoreDbContext(this.dbOptions, false);


            this.dbContext.Database.EnsureCreated();

            DataSeeder.Seed(this.dbContext);

            this.reviewService = new ReviewService(this.dbContext);
        }

        [Test]
        public async Task Add()
        {
            ReviewAddFormModel model = new ReviewAddFormModel
            {
                Id = Guid.NewGuid(),
                ReviewerId = DataSeeder.ApplicationUser.Id.ToString(),
                ComicId = 1,
                ReviewText = "Test",
                StarRating = 5,
                ReviewerName = DataSeeder.ApplicationUser.UserName
            };

            await this.reviewService.Add(model);

            Assert.AreEqual(2, this.dbContext.Reviews.Count());
            Assert.AreEqual(model.Id, this.dbContext.Reviews.Last().Id);
            Assert.AreEqual(model.ReviewerId, this.dbContext.Reviews.Last().ReviewerId.ToString());
        }

        [Test]
        public async Task FindReviewAsync()
        {
            Guid id = DataSeeder.Review.Id;

            var review = await this.reviewService.FindReviewAsync(id);

            Assert.AreEqual(DataSeeder.Review.Id, review.Id);
        }

        [Test]
        public async Task FindReviewAsync_InvalidId()
        {
            Guid id = Guid.NewGuid();

            var review = await this.reviewService.FindReviewAsync(id);

            Assert.IsNull(review);
        }

        [Test]
        public async Task FindReviewAsync_NullId()
        {
            Guid id = Guid.Empty;

            var review = await this.reviewService.FindReviewAsync(id);

            Assert.IsNull(review);
        }

        [Test]
        public async Task RemoveAsync()
        {
            Guid id = DataSeeder.Review.Id;

            await this.reviewService.RemoveAsync(id);

            Assert.AreEqual(1, this.dbContext.Reviews.Count());
        }

        [Test]
        public async Task RemoveAsync_InvalidId()
        {
            Guid id = Guid.NewGuid();

            await this.reviewService.RemoveAsync(id);

            Assert.AreEqual(1, this.dbContext.Reviews.Count());
        }

        [Test]
        public async Task RemoveAsync_NullId()
        {
            Guid id = Guid.Empty;

            await this.reviewService.RemoveAsync(id);

            Assert.AreEqual(1, this.dbContext.Reviews.Count());
        }
        
    }
}
