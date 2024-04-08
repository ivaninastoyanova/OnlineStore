using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class UserFavoriteCreator
    {
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public int CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public Creator Creator { get; set; }

    }
}
