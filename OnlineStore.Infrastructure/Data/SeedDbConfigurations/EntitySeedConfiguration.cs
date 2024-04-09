using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Data.SeedDbConfigurations
{
    public class EntitySeedConfiguration
    {
        public static void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Creator>()
               .HasData(
               new Creator
               {
                   Id = 1,
                   FullName = "Frank Miller",
                   Biography = "Frank Miller (born January 27, 1957) is an American comic book artist, comic book writer, and screenwriter known for his comic book stories and graphic novels such as his run on Daredevil, for which he created the character Elektra, and subsequent Daredevil: Born Again, The Dark Knight Returns, Batman: Year One, Sin City, and 300.",
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d4/FrankMiller_JimLee_DC%27s_2018PopUpShop2.jpg"
                   
               },
               new Creator
               {
                   Id = 2,
                   FullName = "Art Spiegelman",
                   Biography = "Itzhak Avraham ben Zeev Spiegelman (born February 15, 1948), professionally known as Art Spiegelman, is an American cartoonist, editor, and comics advocate best known for his graphic novel Maus. His work as co-editor on the comics magazines Arcade and Raw has been influential, and from 1992 he spent a decade as contributing artist for The New Yorker. He is married to designer and editor Françoise Mouly and is the father of writer Nadja Spiegelman. In September 2022, the National Book Foundation announced that he would receive the Medal for Distinguished Contribution to American Letters.",
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a7/Paris_-_Salon_du_livre_2012_-_Art_Spiegelman_-_001.jpg"
               },
               new Creator
               {
                   Id = 3,
                   FullName = "Alan Moore",
                   Biography = "Alan Moore (born 18 November 1953) is an English author known primarily for his work in comic books including Watchmen, V for Vendetta, The Ballad of Halo Jones, Swamp Thing, Batman: The Killing Joke, and From Hell. He is widely recognised among his peers and critics as one of the best comic book writers in the English language. Moore has occasionally used such pseudonyms as Curt Vile, Jill de Ray, Brilburn Logue, and Translucia Baboon; also, reprints of some of his work have been credited to The Original Writer when Moore requested that his name be removed.",
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2d/Alan_Moore_%282%29.jpg"
               },
               new Creator
               {
                   Id = 4,
                   FullName = "Chris Claremont",
                   Biography = "Christopher S. Claremont (born November 25, 1950) is an American comic book writer and novelist, known for his 16-year stint on Uncanny X-Men from 1975 to 1991, far longer than that of any other writer,[3] during which he is credited with developing strong female characters as well as introducing complex literary themes into superhero narratives, turning the once underachieving comic into one of Marvel's most popular series",
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0b/10.8.16ChrisClaremontByLuigiNovi4.jpg"
               });

            modelBuilder.Entity<Category>()
               .HasData(
               new Category
               {
                   Id = 1,
                   Name = "Superhero"
               },
               new Category
               {
                   Id = 2,
                   Name = "Fantasy"
               },
               new Category
               {
                   Id = 3,
                   Name = "Horror"
               },
               new Category
               {
                   Id = 4,
                   Name = "Non-fiction"
               },
               new Category
               {
                   Id = 5,
                   Name = "Humor"
               },
               new Category
               {
                   Id = 6,
                   Name = "Slice-of-Life"
               });

            modelBuilder.Entity<Comic>()
               .HasData(
               new Comic
               {
                   Id = 1,
                   Title = "The Dark Knight Returns",
                   Description = "The Dark Knight Returns (alternatively titled Batman: The Dark Knight Returns) is a 1986 four-issue comic book miniseries starring Batman, written by Frank Miller, illustrated by Miller and Klaus Janson, with color by Lynn Varley, and published by DC Comics. It tells an alternative story of Bruce Wayne, who at 55 years old returns from retirement to fight crime while facing opposition from the Gotham City police force and the United States government. The story also features the return of classic foes Two-Face and the Joker, and culminates with a confrontation with Superman, who is now a pawn of the government.",
                   CategoryId = 1,
                   CreatorId = 1,
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/7/77/Dark_knight_returns.jpg",
                   Price = 25
               },
               new Comic
               {
                   Id = 2,
                   Title = "Maus",
                   Description = "Maus, often published as Maus: A Survivor's Tale, is a graphic novel by American cartoonist Art Spiegelman, serialized from 1980 to 1991. It depicts Spiegelman interviewing his father about his experiences as a Polish Jew and Holocaust survivor. The work employs postmodern techniques, and represents Jews as mice and other Germans and Poles as cats and pigs respectively. Critics have classified Maus as memoir, biography, history, fiction, autobiography, or a mix of genres. In 1992 it became the first graphic novel to win a Pulitzer Prize.",
                   CategoryId = 2,
                   CreatorId = 2,
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/7/7d/Maus_%28volume_1%29_cover.jpg",
                   Price = 20
               },
               new Comic
               {
                   Id = 3,
                   Title = "V for Vendetta",
                   Description = "V for Vendetta is a British graphic novel written by Alan Moore and illustrated by David Lloyd. Initially published between 1982 and 1985 in black and white as an ongoing serial in the British anthology Warrior, its serialization was completed in 1988–89 in a ten-issue colour limited series published by DC Comics in the United States. Subsequent collected editions were typically published under DC's specialized imprint, Vertigo, until that label was shut down in 2018. Since then it has been transferred to DC Black Label. The story depicts a dystopian and post-apocalyptic near-future history version of the United Kingdom in the 1990s, preceded by a nuclear war in the 1980s that devastated most of the rest of the world. The Nordic supremacist, neo-fascist, outwardly Christofascistic, and homophobic fictional Norsefire political party has exterminated its opponents in concentration camps, and now rules the country as a police state.",
                   CreatorId = 3,
                   CategoryId = 3,
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/c/c0/V_for_vendettax.jpg",
                   Price = 18
               },
               new Comic
               {
                   Id = 4,
                   Title = "X-Men: God Loves, Man Kills",
                   Description = "X-Men: God Loves, Man Kills (Marvel Graphic Novel #5) is an original graphic novel published in 1982 by Marvel Comics, starring their popular superhero team the X-Men. It was written by Christopher Claremont and illustrated by Brent Eric Anderson. The book served as the primary inspiration for the 2003 film X2, which saw Claremont return to write the novelization. Inspired by the rise of televangelism in the 1980s, the story deals with the overall religious extremism against mutants.",
                   CreatorId = 4,
                   CategoryId = 1,
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/0/03/X-Men_God_Loves_Man_Kills_cover.jpg",
                   Price = 30
               },
               new Comic
               {
                   Id = 5,
                   Title = "Daredevil",
                   Description = "Daredevil is the name of several comic book titles featuring the character Daredevil and published by Marvel Comics, beginning with the original Daredevil comic book series which debuted in 1964. While Daredevil had been home to the work of comic-book artists such as Everett, Kirby, Wally Wood, John Romita Sr., Gene Colan, and Joe Quesada, among others, Frank Miller's influential tenure on the title in the early 1980s cemented the character as a popular and influential part of the Marvel Universe.",
                   CreatorId = 1,
                   CategoryId = 1,
                   PhotoUrl = "https://upload.wikimedia.org/wikipedia/en/0/03/Daredevil_%281964%29_1.jpg",
                   Price = 34
               });


        }
    }
}
