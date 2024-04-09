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
    }
}
