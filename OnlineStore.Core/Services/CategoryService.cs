using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Category;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private OnlineStoreDbContext db;

        public CategoryService(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return await db.Categories
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoryNames()
        {
            return await db.Categories
                .Select(c => c.Name)
                .ToListAsync();
        }

        public bool CheckIfAnyComicWithGivenCategory(int id)
        {
            if (db.Comics.Any(c => c.CategoryId == id))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ValidateCategory(string name)
        {
            Category? category = await db.Categories
                .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());

            if (category == null)
            {
                return false;
            }

            return true;
        }

        public async Task AddCategoryAsync(CategoryViewModel model)
        {
            Category category = new Category()
            {
                Name = model.Name
            };

            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
        }
    }
}
