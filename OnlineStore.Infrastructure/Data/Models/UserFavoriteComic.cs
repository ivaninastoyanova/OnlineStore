using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class UserFavoriteComic
    {
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }


        public int ComicId { get; set; }

        [ForeignKey(nameof(ComicId))]
        public Comic Comic { get; set; }
    }
}
