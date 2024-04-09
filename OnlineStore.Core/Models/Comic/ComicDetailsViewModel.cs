using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Models.Comic
{
    public class ComicDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }


        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public string PhotoUrl { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
