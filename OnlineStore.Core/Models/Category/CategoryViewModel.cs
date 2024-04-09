using OnlineStore.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineStore.Infrastructure.Constants.DataConstants;

namespace OnlineStore.Core.Models.Category
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; } = null!;
    }
}
