using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Creator;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services
{
    public class CreatorService : ICreatorService
    {
        private OnlineStoreDbContext db;

        public CreatorService(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<Creator> GetCreatorByNameAsync(string name)
        {
            Creator? creator = await db.Creators
                            .FirstOrDefaultAsync(c => c.FullName == name);

            if (creator == null)
            {
                throw new ArgumentNullException();
            }

            return creator;
        }

        public async Task<bool> ValidateCreator(string name)
        {
            Creator? creator = await db.Creators
                .FirstOrDefaultAsync(a => a.FullName.ToLower() == name.ToLower());

            if (creator == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<AllCreatorsViewModel>> GetAllCreatorsAsync()
        {
            IEnumerable<AllCreatorsViewModel> result = await db.Creators
                .Select(a => new AllCreatorsViewModel
                {
                    Id = a.Id,
                    Name = a.FullName,
                    IsDeleted = a.IsDeleted
                })
                .ToListAsync();

            return result;
        }

        public async Task<CreatorDetailsViewModel> FillModelById(CreatorDetailsViewModel model, int id)
        {
            Creator? creator = await db.Creators.FindAsync(id);

            model.Id = creator.Id;
            model.FullName = creator.FullName;
            model.Biography = creator.Biography;
            model.PhotoUrl = creator.PhotoUrl;
            model.IsDeleted = creator.IsDeleted;

            return model;
        }

        public async Task<Creator> GetGreatorByIdAsync(int id)
        {
            Creator? author = await db.Creators.FindAsync(id);

            if (author == null)
            {
                return null;
            }

            return author;
        }

        public async Task AddCreatorAsync(AddCreatorFormModel model)
        {
            Creator creator = new Creator
            {
                Id = model.Id,
                FullName = model.FullName,
                Biography = model.Biography,
                PhotoUrl = model.PhotoUrl
            };

            await db.Creators
                .AddAsync(creator);

            await db.SaveChangesAsync();
        }

        public async Task DeleteCreatorAsync(int id)
        {
            Creator? creator = await db.Creators.FindAsync(id);

            creator.IsDeleted = true;

            await db.SaveChangesAsync();
        }

        public bool CheckIfAnyComicByCertainCreator(int id)
        {
            Comic? comic = db.Comics
                .FirstOrDefault(c => c.CreatorId == id);

            if (comic != null && comic.IsDeleted == false)
            {
                return true;
            }

            return false;
        }

        public async Task<AddCreatorFormModel> FillModelById(AddCreatorFormModel model, int id)
        {
            Creator? creator = await db.Creators.FindAsync(id);

            model.Id = creator.Id;
            model.FullName = creator.FullName;
            model.Biography = creator.Biography;
            model.PhotoUrl = creator.PhotoUrl;
            model.IsDeleted = creator.IsDeleted;

            return model;
        }

        public async Task EditCreatorAsync(AddCreatorFormModel model, Creator creator)
        {
            creator.FullName = model.FullName;
            creator.Biography = model.Biography;
            creator.PhotoUrl = model.PhotoUrl;
            model.IsDeleted = creator.IsDeleted;

            await db.SaveChangesAsync();
        }
    }
}
