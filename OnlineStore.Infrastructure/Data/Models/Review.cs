using OnlineStore.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Review
    {
        public Review()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [Range(DataConstants.ReviewStarRatingMin, DataConstants.ReviewStarRatingMax)]
        public int StarRating { get; set; }

        [Required]
        public Guid ReviewerId { get; set; }

        [Required]
        [ForeignKey(nameof(ReviewerId))]
        public ApplicationUser Reviewer { get; set; }

        public string ReviewerName { get; set; }

        [Required]
        [MaxLength(DataConstants.ReviewTextMaxLength)]
        public string ReviewText { get; set; } = null!;

        [Required]
        public int ComicId { get; set; }

        [Required]
        [ForeignKey(nameof(ComicId))]
        public Comic Comic { get; set; } = null!;
    }
}
