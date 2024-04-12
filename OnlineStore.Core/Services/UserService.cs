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
    public class UserService : IUserService
    {
        private OnlineStoreDbContext db;

        public UserService(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<ApplicationUser> GetUserById(Guid id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            IEnumerable<ApplicationUser> users = await db.Users.ToListAsync();

            return users;
        }
    }
}
