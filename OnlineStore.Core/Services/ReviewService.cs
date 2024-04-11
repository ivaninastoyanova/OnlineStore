using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Reviews;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services
{
    public class ReviewService : IReviewService
    {
        private OnlineStoreDbContext db;

        public ReviewService(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        public async Task Add(ReviewAddFormModel model)
        {
            Review newReview = new Review();

            newReview.Id = model.Id;
            newReview.ReviewerId = Guid.Parse(model.ReviewerId);
            newReview.ComicId = model.ComicId;
            newReview.ReviewText = model.ReviewText;
            newReview.StarRating = model.StarRating;
            newReview.ReviewerName = model.ReviewerName;

            await db.Reviews.AddAsync(newReview);
            await db.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var review = await db.Reviews.FindAsync(id);
            if (review != null)
            {
                db.Reviews.Remove(review);
                await db.SaveChangesAsync();
                
            }

            //db.Reviews.Remove(review);

            //await db.SaveChangesAsync();
        }

        public async Task<Review> FindReviewAsync(Guid id)
        {
            var review = await db.Reviews
                .FindAsync(id);

            return review;
        }
    }
}
