using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Category;
using OnlineStore.Core.Services;
using OnlineStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests
{
    public class CategoryTests
    {
        private DbContextOptions<OnlineStoreDbContext> dbOptions;
        private OnlineStoreDbContext dbContext;

        private ICategoryService categoryService;

        [SetUp]
        public void SetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase("OnlineStoreInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new OnlineStoreDbContext(this.dbOptions, false);

            this.dbContext.Database.EnsureCreated();

            DataSeeder.Seed(this.dbContext);

            this.categoryService = new CategoryService(this.dbContext);
        }

        [Test]
        public async Task GetCategoriesAsync()
        {
            var categories = await this.categoryService.GetCategoriesAsync();

            Assert.AreEqual(2, categories.Count());
        }

        [Test]
        public async Task AllCategoryNames()
        {
            var categoryNames = await this.categoryService.AllCategoryNames();

            Assert.AreEqual(2, categoryNames.Count());
        }

        [Test]
        public void CheckIfAnyComicWithGivenCategory()
        {
            int categoryId = 1;
            var result = this.categoryService.CheckIfAnyComicWithGivenCategory(categoryId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckIfAnyComicWithGivenCategory_ShouldReturnFalse()
        {
            int categoryId = 3;
            var result = this.categoryService.CheckIfAnyComicWithGivenCategory(categoryId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ValidateCategory()
        {
            string categoryName = "Superhero";
            var result = await this.categoryService.ValidateCategory(categoryName);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ValidateCategorySmallLetters()
        {
            string categoryName = "fantasy";
            var result = await this.categoryService.ValidateCategory(categoryName);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ValidateCategoryInvalidName()
        {
            string categoryName = "Invalid Name";
            var result = await this.categoryService.ValidateCategory(categoryName);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddCategoryAsync()
        {
            var model = new CategoryViewModel()
            {
                Name = "New Category"
            };

            await this.categoryService.AddCategoryAsync(model);

            var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name == model.Name);

            var categories = await this.categoryService.GetCategoriesAsync();

            Assert.AreEqual(3, categories.Count());
            Assert.IsNotNull(category);
            Assert.AreEqual(model.Name, category.Name);
        }

        [Test]
        public async Task RemoveAsync()
        {
            int categoryId = 1;

            await this.categoryService.RemoveAsync(categoryId);

            var category = await this.dbContext.Categories.FindAsync(categoryId);

            Assert.IsNull(category);
        }

        [Test]
        public async Task RemoveAsyncInvalidId()
        {
            int categoryId = 55;

            Assert.ThrowsAsync<ArgumentNullException>(() 
                => this.categoryService.RemoveAsync(categoryId));
        }

        [Test]
        public async Task FindCategory()
        {
            int categoryId = 1;

            var category = this.categoryService.FindCategory(categoryId);

            Assert.IsNotNull(category);
            Assert.AreEqual(categoryId, category.Id);
        }

        [Test]
        public async Task FindCategoryInvalidId()
        {
            int categoryId = 55;

            var category = this.categoryService.FindCategory(categoryId);

            Assert.IsNull(category);
        }

        [Test]
        public async Task EditCategoryAsync()
        {
            int categoryId = 1;
            var model = new CategoryViewModel()
            {
                Name = "New Name"
            };

            await this.categoryService.EditCategoryAsync(categoryId, model);

            var category = await this.dbContext.Categories.FindAsync(categoryId);

            Assert.IsNotNull(category);
            Assert.AreEqual(model.Name, category.Name);
        }

        [Test]
        public async Task EditCategoryAsyncInvalidId()
        {
            int categoryId = 55;
            var model = new CategoryViewModel()
            {
                Name = "New Name"
            };

            Assert.ThrowsAsync<NullReferenceException>(() 
                               => this.categoryService.EditCategoryAsync(categoryId, model));
        }

    }
}
