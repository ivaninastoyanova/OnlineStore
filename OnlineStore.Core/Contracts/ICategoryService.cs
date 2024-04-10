using OnlineStore.Core.Models.Category;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Contracts
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        public Task<IEnumerable<string>> AllCategoryNames();

        public bool CheckIfAnyComicWithGivenCategory(int id);

        public Task<bool> ValidateCategory(string name);

        public Task AddCategoryAsync(CategoryViewModel model);

        public Task RemoveAsync(int id);

        public Category FindCategory(int id);

        public Task EditCategoryAsync(int id, CategoryViewModel model);
    }
}
