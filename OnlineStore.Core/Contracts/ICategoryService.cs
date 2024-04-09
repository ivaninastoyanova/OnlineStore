﻿using OnlineStore.Core.Models.Category;
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
    }
}