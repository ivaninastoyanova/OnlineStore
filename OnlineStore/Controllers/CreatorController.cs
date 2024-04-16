using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Creator;
using OnlineStore.Core.Services;
using OnlineStore.Infrastructure.Data.Models;
using static OnlineStore.Core.Constants.MessageConstants;

namespace OnlineStore.Controllers
{
    public class CreatorController : BaseController
    {
        private ICreatorService creatorService;

        public CreatorController(ICreatorService creatorService)
        {
            this.creatorService = creatorService;
        }

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
                TempData[UserMessageError] = "Creator does not exist!";

                return RedirectToAction("All");
            }

            CreatorDetailsViewModel emptyModel = new CreatorDetailsViewModel();

            CreatorDetailsViewModel model = await creatorService.FillModelById(emptyModel, id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            AddCreatorFormModel model = new AddCreatorFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCreatorFormModel model)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            if (await creatorService.ValidateCreator(model.FullName))
            {
                ModelState.AddModelError(nameof(model.FullName), "This Creator already exists! Add different one or check all creators.");

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                TempData[UserMessageError] = "Invalid creator information!";

                return View(model);
            }

            await creatorService.AddCreatorAsync(model);

            TempData[UserMessageSuccess] = "Creator added succesfully!";

            return RedirectToAction("All", "Creator");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            Creator creator = await creatorService.GetGreatorByIdAsync(id);

            if (creator == null || creator.IsDeleted == true)
            {
                TempData[UserMessageError] = "Creator does not exist!";

                return RedirectToAction("All");
            }

            if (creatorService.CheckIfAnyComicByCertainCreator(id))
            {
                TempData[UserMessageError] = "There are comics by the Creator that exist! Remove them first!";

                return RedirectToAction("All");
            }

            await creatorService.DeleteCreatorAsync(id);

            TempData[UserMessageSuccess] = "Creator removed succesfully!";

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            Creator creator = await creatorService.GetGreatorByIdAsync(id);

            if (creator == null || creator.IsDeleted == true)
            {
                TempData[UserMessageError] = "Creator does not exist!";

                return RedirectToAction("All");
            }

            AddCreatorFormModel emptyModel = new AddCreatorFormModel();

            AddCreatorFormModel model = await creatorService.FillModelById(emptyModel, id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddCreatorFormModel model)
        {
            if (!IsAdmin(User))
            {
                return Unauthorized();
            }

            Creator creator = await creatorService.GetGreatorByIdAsync(model.Id);

            await creatorService.EditCreatorAsync(model, creator);

            TempData[UserMessageSuccess] = "Creator edited!";

            return RedirectToAction("All");
        }
    }
}
