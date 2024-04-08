using OnlineStore.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Comic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ComicTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.ComicDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int CreatorId { get; set; }

        [Required]
        [ForeignKey(nameof(CreatorId))]
        public Creator Creator { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.PhotoUrlMaxLength)]
        public string PhotoUrl { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<UserFavoriteComic> UserFavoriteComics { get; set; } = new List<UserFavoriteComic>();

        public ICollection<UserBoughtComic> UserBoughtComics { get; set; } = new List<UserBoughtComic>();
    }
}
