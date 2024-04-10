using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Category;
using OnlineStore.Core.Services;
using OnlineStore.Infrastructure.Data.Models;

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

        public async Task<IActionResult> Remove(int id)
        {
            Category? category = categoryService.FindCategory(id);

            if (category == null)
            {
                return RedirectToAction("All");
            }

            if (categoryService.CheckIfAnyComicWithGivenCategory(id))
            {
                TempData["ErrorMessage"] = "There are comics with this category! Remove them first!";

                return RedirectToAction("All");
            }

            await categoryService.RemoveAsync(id);

            TempData["Success"] = "Category removed succesfully!";

            return RedirectToAction("All");
        }
    }
}
