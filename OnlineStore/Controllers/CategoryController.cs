using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Category;
using OnlineStore.Core.Services;
using OnlineStore.Infrastructure.Data.Models;
using static OnlineStore.Core.Constants.MessageConstants;

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
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            CategoryViewModel model = new CategoryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            if (await categoryService.ValidateCategory(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "This Category already exists! Add different one or check all categories.");

                return View(model);
            }

            await categoryService.AddCategoryAsync(model);

            TempData[UserMessageSuccess] = "Category added succesfully!";

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Remove(int id)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            Category? category = categoryService.FindCategory(id);

            if (category == null)
            {
                return RedirectToAction("All");
            }

            if (categoryService.CheckIfAnyComicWithGivenCategory(id))
            {
                TempData[UserMessageError] = "There are comics with this category! Remove them first!";

                return RedirectToAction("All");
            }

            await categoryService.RemoveAsync(id);

            TempData[UserMessageSuccess] = "Category removed succesfully!";

            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            Category categoryToEdit = categoryService.FindCategory(id);

            if (categoryToEdit == null)
            {
                return RedirectToAction("All");
            }

            CategoryViewModel categoryModel = new CategoryViewModel()
            {
                Name = categoryToEdit.Name
            };

            return View(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModel categoryModel)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            var categoryToEdit = categoryService.FindCategory(id);

            if (categoryToEdit == null)
            {
                return RedirectToAction("All");
            }

            await categoryService.EditCategoryAsync(id, categoryModel);

            TempData[UserMessageSuccess] = "Category edited succesfully!";

            return RedirectToAction("All");
        }
    }
}
