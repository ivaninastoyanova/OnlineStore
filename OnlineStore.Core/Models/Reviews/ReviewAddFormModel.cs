using OnlineStore.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineStore.Infrastructure.Constants.DataConstants;

namespace OnlineStore.Core.Models.Reviews
{
    public class ReviewAddFormModel
    {
        public ReviewAddFormModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]

        [Range(ReviewStarRatingMin, ReviewStarRatingMax)]
        public int StarRating { get; set; }

        public string ReviewerId { get; set; }

        public string ReviewerName { get; set; }

        [Required]
        [StringLength(ReviewTextMaxLength, MinimumLength =ReviewTextMinLength)]
        public string ReviewText { get; set; } = null!;

        public int ComicId { get; set; }
    }
}
