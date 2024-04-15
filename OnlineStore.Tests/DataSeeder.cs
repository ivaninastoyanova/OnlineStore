using Microsoft.AspNetCore.Identity;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Constants;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static OnlineStore.Infrastructure.Constants.AdministratorConstants;

namespace OnlineStore.Tests
{
    public static class DataSeeder
    {
        public static ApplicationUser AdminUser;
        public static ApplicationUser ApplicationUser;

        public static Creator FrankMiller;
        public static Creator ArtSpiegelman;

        public static Category Superhero;
        public static Category Fantasy;

        public static Comic TheDarkKnightReturns;
        public static Comic Maus;

        public static Review Review;


        public static void Seed(OnlineStoreDbContext context)
        {
            //Users

            var hasher = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = AdministratorConstants.AdminEmail,
                NormalizedEmail = AdministratorConstants.AdminEmail,
                UserName = AdministratorConstants.AdminEmail,
                NormalizedUserName = AdministratorConstants.AdminEmail,
            };
            AdminUser.PasswordHash = hasher.HashPassword(AdminUser, AdministratorConstants.AdminPass);

            ApplicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = "user@mail.com",
                NormalizedEmail = "USER@MAIL.COM",
                UserName = "user@mail.com",
                NormalizedUserName = "USER@MAIL.COM"
            };
            ApplicationUser.PasswordHash = hasher.HashPassword(ApplicationUser, AdministratorConstants.AdminPass);

            //Creators

            FrankMiller = new Creator
            {
                Id = 1,
                FullName = "Frank Miller",
                Biography = "Frank Miller (born January 27, 1957) is an American comic book artist, comic book writer, and screenwriter known for his comic book stories and graphic novels such as his run on Daredevil, for which he created the character Elektra, and subsequent Daredevil: Born Again, The Dark Knight Returns, Batman: Year One, Sin City, and 300.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d4/FrankMiller_JimLee_DC%27s_2018PopUpShop2.jpg"
            };

            ArtSpiegelman = new Creator
            {
                Id = 2,
                FullName = "Art Spiegelman",
                Biography = "Itzhak Avraham ben Zeev Spiegelman (born February 15, 1948), professionally known as Art Spiegelman, is an American cartoonist, editor, and comics advocate best known for his graphic novel Maus. His work as co-editor on the comics magazines Arcade and Raw has been influential, and from 1992 he spent a decade as contributing artist for The New Yorker. He is married to designer and editor Françoise Mouly and is the father of writer Nadja Spiegelman. In September 2022, the National Book Foundation announced that he would receive the Medal for Distinguished Contribution to American Letters.",
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a7/Paris_-_Salon_du_livre_2012_-_Art_Spiegelman_-_001.jpg"
            };
            
            //Categories

            Superhero = new Category
            {
                Id = 1,
                Name = "Superhero"
            };

            Fantasy = new Category
            {
                Id = 2,
                Name = "Fantasy"
            };

            //Comics

            TheDarkKnightReturns = new Comic()
            {
                Id = 1,
                Title = "The Dark Knight Returns",
                Description = "The Dark Knight Returns (alternatively titled Batman: The Dark Knight Returns) is a 1986 four-issue comic book miniseries starring Batman, written by Frank Miller, illustrated by Miller and Klaus Janson, with color by Lynn Varley, and published by DC Comics. It tells an alternative story of Bruce Wayne, who at 55 years old returns from retirement to fight crime while facing opposition from the Gotham City police force and the United States government. The story also features the return of classic foes Two-Face and the Joker, and culminates with a confrontation with Superman, who is now a pawn of the government.",
                CategoryId = 1,
                CreatorId = 1,
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/7/77/Dark_knight_returns.jpg",
                Price = 25
            };

            Maus = new Comic()
            {
                Id = 2,
                Title = "Maus",
                Description = "Maus, often published as Maus: A Survivor's Tale, is a graphic novel by American cartoonist Art Spiegelman, serialized from 1980 to 1991. It depicts Spiegelman interviewing his father about his experiences as a Polish Jew and Holocaust survivor. The work employs postmodern techniques, and represents Jews as mice and other Germans and Poles as cats and pigs respectively. Critics have classified Maus as memoir, biography, history, fiction, autobiography, or a mix of genres. In 1992 it became the first graphic novel to win a Pulitzer Prize.",
                CategoryId = 2,
                CreatorId = 2,
                PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/7/7d/Maus_%28volume_1%29_cover.jpg",
                Price = 20
            };

            //Reviews

            Review = new Review
            {
                StarRating = 5,
                ReviewerId = ApplicationUser.Id,
                ReviewerName = ApplicationUser.Email,
                ReviewText = "Very nice comic.",
                ComicId = TheDarkKnightReturns.Id
            };

            context.Users.Add(ApplicationUser);
            context.Users.Add(AdminUser);

            context.Creators.Add(FrankMiller);
            context.Creators.Add(ArtSpiegelman);

            context.Categories.Add(Superhero);
            context.Categories.Add(Fantasy);

            context.Comics.Add(TheDarkKnightReturns);
            context.Comics.Add(Maus);

            context.Reviews.Add(Review);

            context.SaveChanges();
        }
    }
}

