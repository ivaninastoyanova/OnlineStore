using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Creator;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Controllers
{
    public class CreatorController : BaseController
    {
        private ICreatorService creatorService;

        public CreatorController(ICreatorService creatorService)
        {
            this.creatorService = creatorService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllCreatorsViewModel> model = new List<AllCreatorsViewModel>();

            model = await creatorService.GetAllCreatorsAsync();

            model = model.Where(a => a.IsDeleted == false);

            return View(model);
        }

        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Creator creator = await creatorService.GetGreatorByIdAsync(id);

            if (creator == null || creator.IsDeleted == true)
            {
                TempData["ErrorMessage"] = "Creator does not exist!";

                return RedirectToAction("All");
            }

            CreatorDetailsViewModel emptyModel = new CreatorDetailsViewModel();

            CreatorDetailsViewModel model = await creatorService.FillModelById(emptyModel, id);

            return View(model);
        }
    }
}
