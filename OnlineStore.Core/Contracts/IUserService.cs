using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Contracts
{
    public  interface IUserService
    {
        public Task<IEnumerable<ApplicationUser>> GetUsers();

        public Task<ApplicationUser> GetUserById(Guid id);
    }
}
