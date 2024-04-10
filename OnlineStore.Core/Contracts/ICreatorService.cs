using OnlineStore.Core.Models.Creator;
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

        public Task<IEnumerable<AllCreatorsViewModel>> GetAllCreatorsAsync();

        public Task<CreatorDetailsViewModel> FillModelById(CreatorDetailsViewModel model, int id);

        public Task<Creator> GetGreatorByIdAsync(int id);

        public Task AddCreatorAsync(AddCreatorFormModel model);

        public bool CheckIfAnyComicByCertainCreator(int id);

        public Task DeleteCreatorAsync(int id);
    }
}
