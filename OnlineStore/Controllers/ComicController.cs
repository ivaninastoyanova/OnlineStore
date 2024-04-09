using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Comic;

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
    }
}
