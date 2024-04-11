using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Category;
using OnlineStore.Core.Models.Comic;
using OnlineStore.Infrastructure.Data.Models;
using static OnlineStore.Core.Constants.MessageConstants;

namespace OnlineStore.Controllers
{
    public class ComicController : BaseController
    {
        private ICreatorService creatorService;
        private IComicService comicService;
        private ICategoryService categoryService;
        private IReviewService reviewService;

        public ComicController(ICreatorService creatorService,
                    IComicService comicService, 
                    ICategoryService categoryService,
                    IReviewService reviewService)
        {
            this.creatorService = creatorService;
            this.comicService = comicService;
            this.categoryService = categoryService;
            this.reviewService = reviewService;
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

        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ComicDetailsViewModel model = new ComicDetailsViewModel();

            model = await comicService.GetComicAsync(model, id);

            if(model == null)
            {
                TempData[UserMessageError] = "Comic does not exist!";

                return RedirectToAction("All");
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
                TempData[UserMessageError] = "Invalid comic data!";

                return View(model);
            }

            Creator creator = await creatorService.GetCreatorByNameAsync(model.Creator);
            await comicService.AddComicAsync(model, creator);

            TempData[UserMessageSuccess] = "Comic added succesfully!";

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool result = await comicService.DeleteComic(id);

            if (result)
            {
                TempData[UserMessageSuccess] = "Comic deleted succesfully!";

                return RedirectToAction("All");
            }
            else
            {
                TempData[UserMessageError] = "Comic does not exist!";

                return RedirectToAction("All");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddComicViewModel model = new AddComicViewModel();

            model = comicService.FindComic(id);

           if (model == null)
            {
                return RedirectToAction("All");
            }

            IEnumerable<CategoryViewModel> categories = await categoryService.GetCategoriesAsync();

            model.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddComicViewModel model)
        {
            var comic = comicService.FindComic(id);
            IEnumerable<CategoryViewModel> categories = await categoryService.GetCategoriesAsync();

            if (comic == null)
            {
                return RedirectToAction("All");
            }

            if (!await creatorService.ValidateCreator(model.Creator))
            {
                ModelState.AddModelError(nameof(model.Creator), "Creator does not exist in database! Please add the creator, before you can proceed.");

                model.Categories = categories;

                return View(model);
            }

            await comicService.EditComicAsync(model, id);

            TempData[UserMessageSuccess] = "Comic edited succesfully!";

            return RedirectToAction("All");
        }
    }
}
