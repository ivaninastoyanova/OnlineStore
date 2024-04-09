using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Category;
using OnlineStore.Core.Models.Comic;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Controllers
{
    public class ComicController : BaseController
    {
        private ICreatorService creatorService;
        private IComicService comicService;
        private ICategoryService categoryService;

        public ComicController(ICreatorService creatorService,
                    IComicService comicService, 
                    ICategoryService categoryService)
        {
            this.creatorService = creatorService;
            this.comicService = comicService;
            this.categoryService = categoryService;

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] ComicAllQueryModel query)
        {
            AllComicsFilteredAndOrdered model = await comicService.AllAsync(query);

            query.Comics = model.Comics;
            query.TotalComics = model.TotalComicsCount;
            query.Categories = await categoryService.AllCategoryNames();

            return View(query);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ComicDetailsViewModel model = new ComicDetailsViewModel();

            model = await comicService.GetComicAsync(model, id);

            if(model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //if (!IsAdmin(User))
            //{
            //    return Unauthorized();
            //}

            AddComicViewModel viewModel = new AddComicViewModel();

            IEnumerable<CategoryViewModel> categories = await categoryService.GetCategoriesAsync();

            viewModel.Categories = categories;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddComicViewModel model)
        {
            //if (!IsAdmin(User))
            //{
            //    return Unauthorized();
            //}

            IEnumerable<CategoryViewModel> categories = await categoryService.GetCategoriesAsync();

            model.Categories = categories;

            if (!await creatorService.ValidateCreator(model.Creator))
            {
                ModelState.AddModelError(nameof(model.Creator), "Creator does not exist in database! Please add the creator, before the comic!");

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid comic data!";

                return View(model);
            }

            Creator creator = await creatorService.GetCreatorByNameAsync(model.Creator);
            await comicService.AddComicAsync(model, creator);

            TempData["Success"] = "Comic added succesfully!";

            return RedirectToAction("All");
        }

    }
}
