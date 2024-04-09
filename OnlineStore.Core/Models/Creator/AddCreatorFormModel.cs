using OnlineStore.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineStore.Infrastructure.Constants.DataConstants;

namespace OnlineStore.Core.Models.Creator
{
    public class AddCreatorFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CreatorNameMaxLength, MinimumLength = CreatorNameMinLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(CreatorBiographyMaxLength, MinimumLength = CreatorBiographyMinLength)]
        public string Biography { get; set; } = null!;

        [Required]
        [MaxLength(PhotoUrlMaxLength)]
        [Display(Name = "Photo Url")]
        public string PhotoUrl { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}

