using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Reviews;
using OnlineStore.Infrastructure.Data.Models;
using System.Security.Claims;

namespace OnlineStore.Controllers
{
    public class ReviewController : BaseController
    {
        private IReviewService reviewService;
        private IComicService comicService;

        public ReviewController(IReviewService reviewService,
            IComicService comicService)
        {
            this.reviewService = reviewService;
            this.comicService = comicService;
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            string? userId = GetId(User);
            string? email = GetEmail(User);

            if (userId == null || email == null)
            {
                TempData["ErrorMessage"] = "No user found!";

                return RedirectToAction("All", "Comic");
            }
            
            if(!comicService.ComicExistsAsync(id).Result)
            {
                TempData["ErrorMessage"] = "Comic not found!";

                return RedirectToAction("All", "Comic");
            }

            ReviewAddFormModel form = new ReviewAddFormModel();
            

            form.ReviewerId = userId;
            form.ReviewerName = email;
            form.ComicId = id;

            ViewBag.Id = id;
            ViewBag.ReviewerId = userId;
            ViewBag.ReviewerName = email;

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReviewAddFormModel form, int id)
        {

            string? userId = GetId(User);
            string? email = GetEmail(User);

            if (userId == null || email == null)
            {
                TempData["ErrorMessage"] = "No user found!";

                return RedirectToAction("Details", "Comic", new { id });
            }

            if (!comicService.ComicExistsAsync(id).Result)
            {
                TempData["ErrorMessage"] = "Comic not found!";

                return RedirectToAction("All", "Comic");
            }

            form.ReviewerId = userId;
            form.ReviewerName = email;
            form.ComicId = id;

            await reviewService.Add(form);

            TempData["Success"] = "Review added succesfully!";

            return RedirectToAction("Details", "Comic", new { id });
        }
    }
}
