using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Creator;
using OnlineStore.Core.Services;
using OnlineStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests
{
    public class CreatorTests
    {
        private DbContextOptions<OnlineStoreDbContext> dbOptions;
        private OnlineStoreDbContext dbContext;

        private ICreatorService creatorService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase("OnlineStoreInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new OnlineStoreDbContext(this.dbOptions, false);


            this.dbContext.Database.EnsureCreated();

            DataSeeder.Seed(this.dbContext);

            this.creatorService = new CreatorService(this.dbContext);
        }

        [Test]
        public async Task GetCreatorByIdAsyncValidId_ShouldReturnCreator()
        {
            int creatorId = 1;
            var creator = await this.creatorService.GetGreatorByIdAsync(creatorId);

            Assert.IsNotNull(creator);
            Assert.AreEqual(creatorId, creator.Id);
            Assert.AreEqual(creator.FullName, DataSeeder.FrankMiller.FullName);
        }

        [Test]
        public async Task GetCreatorByIdAsyncInvalidId_ShouldReturnNull()
        {
            int creatorId = 6;
            var creator = await this.creatorService.GetGreatorByIdAsync(creatorId);

            Assert.IsNull(creator);
        }

        [Test]
        public async Task GetCreatorByNameAsync()
        {
            string creatorName = "Frank Miller";
            var creator = await this.creatorService.GetCreatorByNameAsync(creatorName);

            Assert.IsNotNull(creator);
            Assert.AreEqual(creatorName, creator.FullName);
        }

        [Test]
        public async Task GetCreatorByNameAsyncInvalidName_ShouldReturnNull()
        {
            string creatorName = "Invalid Name";

            Assert.ThrowsAsync<ArgumentNullException>(() => 
            this.creatorService.GetCreatorByNameAsync(creatorName));

        }

        [Test]
        public async Task ValidateCreator()
        {
            string creatorName = "frank miller";
            var isValid = await this.creatorService.ValidateCreator(creatorName);

            Assert.IsTrue(isValid);
        }

        [Test]
        public async Task ValidateCreatorInvalidName_ShouldReturnFalse()
        {
            string creatorName = "Invalid Name";

            var isValid = await this.creatorService.ValidateCreator(creatorName);

            Assert.IsFalse(isValid);
        }

        [Test]
        public async Task GetAllCreatorsAsync()
        {
            var creators = await this.creatorService.GetAllCreatorsAsync();

            Assert.IsNotNull(creators);
            Assert.AreEqual(3, creators.Count());
        }

        [Test]
        public async Task FillModelById_CreatorDetailsViewModel()
        {
            int creatorId = 1;
            var model = new CreatorDetailsViewModel();
            var creator = await this.creatorService.FillModelById(model, creatorId);

            Assert.IsNotNull(creator);
            Assert.AreEqual(creatorId, creator.Id);
            Assert.AreEqual(DataSeeder.FrankMiller.FullName, creator.FullName);
        }

        [Test]
        public async Task FillModelById_CreatorDetailsViewModel_InvalidId()
        {
            int creatorId = 6;
            var model = new CreatorDetailsViewModel();

            Assert.ThrowsAsync<NullReferenceException>(() => 
            this.creatorService.FillModelById(model, creatorId));
        }

        [Test]
        public async Task FillModelById_AddCreatorFormModel()
        {
            int creatorId = 1;
            var model = new AddCreatorFormModel();
            var creator = await this.creatorService.FillModelById(model, creatorId);

            Assert.IsNotNull(creator);
            Assert.AreEqual(creatorId, creator.Id);
            Assert.AreEqual(DataSeeder.FrankMiller.FullName, creator.FullName);
        }

        [Test]
        public async Task FillModelById_AddCreatorFormModel_InvalidId()
        {
            int creatorId = 6;
            var model = new AddCreatorFormModel();

            Assert.ThrowsAsync<NullReferenceException>(() => 
                       this.creatorService.FillModelById(model, creatorId));
        }

        [Test]
        public async Task AddCreatorAsync()
        {
            var model = new AddCreatorFormModel
            {
                Id = 3,
                FullName = "New Creator",
                Biography = "Biography",
                PhotoUrl = "https://newcreator.com"
            };

            await this.creatorService.AddCreatorAsync(model);

            var creator = await this.creatorService.GetCreatorByNameAsync(model.FullName);

            Assert.IsNotNull(creator);
            Assert.AreEqual(model.FullName, creator.FullName);
            Assert.AreEqual(model.Biography, creator.Biography);
            Assert.AreEqual(model.PhotoUrl, creator.PhotoUrl);
            Assert.AreEqual(model.Id, creator.Id);
        }

        [Test]
        public async Task CheckIfAnyComicByCertainCreator()
        {
            int creatorId = 1;
            var hasComics = this.creatorService.CheckIfAnyComicByCertainCreator(creatorId);

            Assert.IsTrue(hasComics);
        }

        [Test]
        public async Task CheckIfAnyComicByCertainCreator_NoComics()
        {
            int creatorId = 3;
            var hasComics = this.creatorService.CheckIfAnyComicByCertainCreator(creatorId);

            Assert.IsFalse(hasComics);
        }

        [Test]
        public async Task CheckIfAnyComicByCertainCreator_InvalidId()
        {
            int creatorId = 66;
            var hasComics = this.creatorService.CheckIfAnyComicByCertainCreator(creatorId);

            Assert.IsFalse(hasComics);
        }

        [Test]
        public async Task EditCreatorAsync()
        {
            int creatorId = 3;
            var creator = await this.creatorService.GetGreatorByIdAsync(creatorId);

            var model = new AddCreatorFormModel
            {
                Id = creatorId,
                FullName = "New Creator",
                Biography = "Biography edited",
                PhotoUrl = "https://newcreator.com"
            };

            await this.creatorService.EditCreatorAsync(model, creator);

            Assert.IsNotNull(creator);
            Assert.AreEqual(model.FullName, creator.FullName);
            Assert.AreEqual(model.PhotoUrl, creator.PhotoUrl);
            Assert.AreEqual("Biography edited", creator.Biography);

            creator.Biography = "Biography";
            dbContext.SaveChanges();
        }

        [Test]
        public async Task EditCreatorAsync_InvalidId()
        {
            int creatorId = 66;
            var creator = await this.creatorService.GetGreatorByIdAsync(creatorId);

            var model = new AddCreatorFormModel
            {
                Id = creatorId,
                FullName = "New Creator",
                Biography = "Biography edited",
                PhotoUrl = "https://newcreator.com"
            };

            Assert.ThrowsAsync<NullReferenceException>(() => 
                       this.creatorService.EditCreatorAsync(model, creator));
        }

        [Test]
        public async Task DeleteCreatorAsync()
        {
            int creatorId = 3;

            var creator = await this.creatorService.GetGreatorByIdAsync(creatorId);

            await this.creatorService.DeleteCreatorAsync(creatorId);

            Assert.IsTrue(creator.IsDeleted);
        }

        [Test]
        public async Task DeleteCreatorAsync_InvalidId()
        {
            int creatorId = 66;

            var creator = await this.creatorService.GetGreatorByIdAsync(creatorId);

            Assert.ThrowsAsync<NullReferenceException>(() => 
                                  this.creatorService.DeleteCreatorAsync(creatorId));
        }
    }
}
