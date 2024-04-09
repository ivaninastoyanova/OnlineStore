using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Contracts
{
    public interface ICreatorService
    {
        public Task<Creator> GetCreatorByNameAsync(string name);

        public Task<bool> ValidateCreator(string name);
    }
}
