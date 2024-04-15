using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Comic;
using OnlineStore.Core.Models.Comic.Enums;
using OnlineStore.Core.Models.Creator;
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
    public class ComicTests
    {
        private DbContextOptions<OnlineStoreDbContext> dbOptions;
        private OnlineStoreDbContext dbContext;

        private IComicService comicService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase("OnlineStoreInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new OnlineStoreDbContext(this.dbOptions, false);


            this.dbContext.Database.EnsureCreated();

            DataSeeder.Seed(this.dbContext);

            this.comicService = new ComicService(this.dbContext);

        }

        [Test]
        public async Task AllAsync()
        {
            var queryModel = new ComicAllQueryModel();

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(2, result.Comics.Count());
        }

        [Test]
        public async Task AllAsyncWithCategory()
        {
            var queryModel = new ComicAllQueryModel
            {
                CategoryName = "Superhero"
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(1, result.Comics.Count());
        }

        [Test]
        public async Task AllAsyncWithCategoryNoResults()
        {
            var queryModel = new ComicAllQueryModel
            {
                CategoryName = "Test"
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(0, result.Comics.Count());
        }

        [Test]
        public async Task AllAsyncWithSearchString()
        {
            var queryModel = new ComicAllQueryModel
            {
                SearchString = "Maus"
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(1, result.Comics.Count());
        }

        [Test]
        public async Task AllAsyncWithSearchStringNoResults()
        {
            var queryModel = new ComicAllQueryModel
            {
                SearchString = "test"
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(0, result.Comics.Count());
        }

        [Test]
        public async Task AllAsyncWithSorting()
        {
            var queryModel = new ComicAllQueryModel
            {
                ComicSorting = ComicSortEnum.PriceAscending
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(2, result.Comics.Count());
            Assert.AreEqual(2, result.Comics.First().Id);
        }

        [Test]
        public async Task AllAsyncWithPagination()
        {
            var queryModel = new ComicAllQueryModel
            {
                ComicsPerPage = 1,
                CurrentPage = 2
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(1, result.Comics.Count());
            Assert.AreEqual(1, result.Comics.First().Id);
        }

        [Test]
        public async Task AllAsyncWithSortingAndFiltering()
        {
            var queryModel = new ComicAllQueryModel
            {
                CategoryName = "Superhero",
                ComicSorting = ComicSortEnum.PriceAscending
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(1, result.Comics.Count());
            Assert.AreEqual(1, result.Comics.First().Id);
        }

        [Test]
        public async Task AllAsyncCategoryAndSearchStringNoResults()
        {
            var queryModel = new ComicAllQueryModel
            {
                CategoryName = "Superhero",
                SearchString = "Maus"
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(0, result.Comics.Count());
        }

        [Test]
        public async Task AllAsyncCategoryAndSearchString()
        {
            var queryModel = new ComicAllQueryModel
            {
                CategoryName = "Fantasy",
                SearchString = "Maus"
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(1, result.Comics.Count());
        }

        [Test]
        public async Task AllAsyncCategoryAndSearchStringAndSorting()
        {
            var queryModel = new ComicAllQueryModel
            {
                CategoryName = "Fantasy",
                SearchString = "Maus",
                ComicSorting = ComicSortEnum.PriceAscending
            };

            var result = await this.comicService.AllAsync(queryModel);

            Assert.AreEqual(1, result.Comics.Count());
            Assert.AreEqual(2, result.Comics.First().Id);
        }

        [Test]
        public async Task FindComic()
        {
            int comicId = 2;
            string wantedTitle = "Maus";

            var book = comicService.FindComic(comicId);

            Assert.That("Maus", Is.EqualTo(wantedTitle));
        }

        [Test]
        public async Task FindComicNoResults()
        {
            int comicId = 3;

            var comic = comicService.FindComic(comicId);

            Assert.IsNull(comic);
        }

        [Test]
        public async Task ComicExistsAsync()
        {
            int comicId = 1;

            var exists = comicService.ComicExistsAsync(comicId);

            Assert.IsTrue(exists.Result);
        }

        [Test]
        public async Task ComicExistsAsyncFalse()
        {
            int comicId = 77;

            var exists = comicService.ComicExistsAsync(comicId);

            Assert.IsFalse(exists.Result);
        }

        [Test]
        public async Task GetComicAsync()
        {
            var queryModel = new ComicDetailsViewModel();
            
            int comicId = 1;

            var comic = comicService.GetComicAsync(queryModel, comicId);

            Assert.AreEqual("The Dark Knight Returns", comic.Result.Title);
        }

        [Test]
        public async Task GetComicAsyncNoResults()
        {
            var queryModel = new ComicDetailsViewModel();

            int comicId = 3;

            var comic = comicService.GetComicAsync(queryModel, comicId);

            Assert.IsNull(comic.Result);
        }

        [Test]
        public async Task DeleteComicAsync()
        {
            int comicId = 1;

            await comicService.DeleteComic(comicId);

            var comic = dbContext.Comics.Find(comicId);

            Assert.IsTrue(comic.IsDeleted);

            comic.IsDeleted = false;
        }


        [Test]
        public async Task DeleteComicAsyncNoSuchComic()
        {
            int comicId = 88;

            bool deleted = await comicService.DeleteComic(comicId);

            Assert.IsFalse(deleted);

        }

        [Test]
        public async Task AddComicAsync()
        {
            int creatorId = 1;
            var creator = dbContext.Creators.FirstOrDefault(c => c.Id == creatorId);

            var model = new AddComicViewModel
            {
                Id = 5,
                Title = "Test",
                Description = "Test",
                CategoryId = "1",
                Price = 10,
                PhotoUrl = "Test",
                Creator = creator.FullName
            };

            await comicService.AddComicAsync(model, creator);

            var comic = dbContext.Comics.FirstOrDefault(c => c.Id == 5);

            Assert.IsNotNull(comic);
            Assert.AreEqual("Test", comic.Title);
            Assert.AreEqual(comic.CreatorId, creatorId);

            dbContext.Comics.Remove(comic);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task EditComicAsync()
        {
            int creatorId = 1;
            var creator = dbContext.Creators.FirstOrDefault(c => c.Id == creatorId);
            var Comic = dbContext.Comics.FirstOrDefault(c => c.Id == 1);

            var model = new AddComicViewModel
            {
                Id = 1,
                Title = "The Dark Knight Returns",
                Description = "The Dark Knight Returns (alternatively titled Batman: The Dark Knight Returns) is a 1986 four-issue comic book miniseries starring Batman, written by Frank Miller, illustrated by Miller and Klaus Janson, with color by Lynn Varley, and published by DC Comics. It tells an alternative story of Bruce Wayne, who at 55 years old returns from retirement to fight crime while facing opposition from the Gotham City police force and the United States government. The story also features the return of classic foes Two-Face and the Joker, and culminates with a confrontation with Superman, who is now a pawn of the government.",
                CategoryId = "2",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/7/77/Dark_knight_returns.jpg",
                Price = 20,
                Creator = creator.FullName
            };

            await comicService.EditComicAsync(model, 1);

            var comic = dbContext.Comics.FirstOrDefault(c => c.Id == 1);

            Assert.IsNotNull(comic);
            Assert.AreEqual(20, comic.Price);
            Assert.AreEqual(comic.CreatorId, creatorId);
            Assert.AreEqual(comic.CategoryId, 2);


            comic.CategoryId = 1;
            comic.Price = 25;
            dbContext.SaveChanges();
        }
    }
}
