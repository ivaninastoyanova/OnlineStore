using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Reviews;
using OnlineStore.Infrastructure.Data.Models;
using System.Security.Claims;
using static OnlineStore.Core.Constants.MessageConstants;

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
                TempData[UserMessageError] = "No user found!";

                return RedirectToAction("All", "Comic");
            }
            
            if(!comicService.ComicExistsAsync(id).Result)
            {
                TempData[UserMessageError] = "Comic not found!";

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
                TempData[UserMessageError] = "No user found!";

                return RedirectToAction("Details", "Comic", new { id });
            }

            if (!comicService.ComicExistsAsync(id).Result)
            {
                TempData[UserMessageError] = "Comic not found!";

                return RedirectToAction("All", "Comic");
            }

            form.ReviewerId = userId;
            form.ReviewerName = email;
            form.ComicId = id;

            await reviewService.Add(form);

            TempData[UserMessageSuccess] = "Review added succesfully!";

            return RedirectToAction("Details", "Comic", new { id });
        }

        public async Task<IActionResult> Remove(string id)
        {
            var reviewId = Guid.Parse(id);

            var review = await reviewService.FindReviewAsync(reviewId);

            if (review == null)
            {
                TempData[UserMessageError] = "No review found!";

                return RedirectToAction("All", "Comic");
            }

            if(review.ReviewerId.ToString() != GetId(User) && !IsAdmin(User))
            {
                TempData[UserMessageError] = "You have no permission to remove the review!";

                return RedirectToAction("All", "Comic");
            }

            await reviewService.RemoveAsync(reviewId);

            TempData[UserMessageSuccess] = "Review removed succesfully!";

            return RedirectToAction("All", "Comic");
        }
    }
}
