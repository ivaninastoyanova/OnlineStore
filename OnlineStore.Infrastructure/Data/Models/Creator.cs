using OnlineStore.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Creator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.CreatorNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.CreatorBiographyMaxLength)]
        public string Biography { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.PhotoUrlMaxLength)]
        public string PhotoUrl { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<Comic> Comics { get; set; } = new List<Comic>();

        public ICollection<UserFavoriteCreator> UserFavoriteCreators { get; set; } = new List<UserFavoriteCreator>();
    }
}
