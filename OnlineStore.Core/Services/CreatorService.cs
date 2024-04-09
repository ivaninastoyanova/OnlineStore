using Microsoft.EntityFrameworkCore;
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
    }
}
