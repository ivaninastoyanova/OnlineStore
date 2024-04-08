using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        public int CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<UserBoughtComic> BoughtComics { get; set; } = new List<UserBoughtComic>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
