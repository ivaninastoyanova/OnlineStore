using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnlineStore.Core.Models.Comic
{
    public class ComicAllViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Creator")]
        public string CreatorName { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string PhotoUrl { get; set; }

        public decimal Price { get; set; }
    }
}
