using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Creator;

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


    }
}
