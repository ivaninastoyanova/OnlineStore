using OnlineStore.Core.Models.Category;
using OnlineStore.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static OnlineStore.Infrastructure.Constants.DataConstants;

namespace OnlineStore.Core.Models.Comic
{
    public class AddComicViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ComicTitleMaxLength, MinimumLength = ComicTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ComicDescriptionMaxLength, MinimumLength = ComicDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(CreatorNameMaxLength, MinimumLength = CreatorNameMinLength)]
        public string Creator { get; set; } = null!;

        [Required]
        [Range(ComicPriceMin, ComicPriceMax)]
        public decimal Price { get; set; }

        [Display(Name = "Photo Url")]
        [Required]
        [StringLength(PhotoUrlMaxLength)]
        public string PhotoUrl { get; set; } = null!;

        public string CategoryId { get; set; } = null!;

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
