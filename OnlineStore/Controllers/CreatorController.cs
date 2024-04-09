using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Controllers
{
    public class CreatorController : BaseController
    {
        private ICreatorService creatorService;

        public CreatorController(ICreatorService creatorService)
        {
            this.creatorService = creatorService;
        }
    }
}
