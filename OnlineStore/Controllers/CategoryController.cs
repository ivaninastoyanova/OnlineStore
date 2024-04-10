using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Category;
using OnlineStore.Core.Services;

namespace OnlineStore.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<CategoryViewModel> categories = await categoryService.GetCategoriesAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            CategoryViewModel model = new CategoryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (await categoryService.ValidateCategory(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "This Category already exists! Add different one or check all categories.");

                return View(model);
            }

            await categoryService.AddCategoryAsync(model);

            TempData["Success"] = "Category added succesfully!";

            return RedirectToAction("All");
        }
    }
}
