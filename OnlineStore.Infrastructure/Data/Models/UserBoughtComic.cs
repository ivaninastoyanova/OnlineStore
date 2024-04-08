using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class UserBoughtComic
    {
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public int ComicId { get; set; }

        [ForeignKey(nameof(ComicId))]
        public Comic Comic { get; set; }

    }
}
