using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Infrastructure
{
    public class OnlineStoreDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options)
            : base(options)
        {

        }

        public DbSet<Creator> Creators { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Comic> Comics { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<UserFavoriteCreator> UserFavoriteCreators { get; set; } = null!;

        public DbSet<UserFavoriteComic> UserFavoriteComics { get; set; } = null!;

        public DbSet<UserBoughtComic> UsersBoughtComics { get; set; } = null!;

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserFavoriteCreator>()
                .HasKey(uc => new { uc.UserId, uc.CreatorId });

            builder.Entity<UserBoughtComic>()
                .HasKey(ub => new { ub.UserId, ub.ComicId });

            builder.Entity<UserFavoriteComic>()
                .HasKey(uc => new { uc.UserId, uc.ComicId });

            builder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(c => c.Cart)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Restrict);

            //EntitySeedDataConfiguration.Seed(builder);

            base.OnModelCreating(builder);
        }
    }
}
