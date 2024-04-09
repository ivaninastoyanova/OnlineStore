using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models.Comic;
using OnlineStore.Core.Models.Comic.Enums;
using OnlineStore.Infrastructure;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services
{
    public class ComicService : IComicService
    {
        private OnlineStoreDbContext db;

        public ComicService(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<AllComicsFilteredAndOrdered> AllAsync(ComicAllQueryModel queryModel)
        {
            IQueryable<Comic> comicQuery = this.db
                                .Comics
                                .Where(b => b.IsDeleted == false)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
            {
                comicQuery = comicQuery
                    .Where(h => h.Category.Name == queryModel.CategoryName);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                comicQuery = comicQuery
                    .Where(h => EF.Functions.Like(h.Title, wildCard) ||
                                EF.Functions.Like(h.Creator.FullName, wildCard) ||
                                EF.Functions.Like(h.Description, wildCard));
            }

            comicQuery = queryModel.ComicSorting switch
            {
                ComicSortEnum.AlphabeticallyAscending => comicQuery
                    .OrderBy(b => b.Title),
                ComicSortEnum.AlphabeticallyDescending => comicQuery
                    .OrderByDescending(b => b.Title),
                ComicSortEnum.PriceAscending => comicQuery
                    .OrderBy(b => b.Price),
                ComicSortEnum.PriceDescending => comicQuery
                    .OrderByDescending(b => b.Price),
                ComicSortEnum.ByCreatorDescending => comicQuery
                    .OrderByDescending(b => b.Creator.FullName),
                ComicSortEnum.ByCreatorAscending => comicQuery
                    .OrderBy(b => b.Creator.FullName),
                _ => comicQuery
                    .OrderBy(b => b.Title)
            };

            IEnumerable<ComicAllViewModel> comics = await comicQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ComicsPerPage)
                .Take(queryModel.ComicsPerPage) 
                .Select(b => new ComicAllViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    PhotoUrl = b.PhotoUrl,
                    Price = b.Price,
                    CreatorName = b.Creator.FullName,
                    CategoryName = b.Category.Name,
                    IsDeleted = b.IsDeleted
                })
                .ToArrayAsync();

            int totalComicsCount = comicQuery.Count();

            return new AllComicsFilteredAndOrdered()
            {
                TotalComicsCount = totalComicsCount,
                Comics = comics
            };
        }

        public async Task<ComicDetailsViewModel> GetComicAsync(ComicDetailsViewModel model, int id)
        {
            Comic? comic = await db.Comics
                .Include(cr => cr.Creator)
                .Include(c => c.Category)
                .Include(d => d.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (comic == null)
            {
                return null;
            }

            model.Id = comic.Id;
            model.Title = comic.Title;
            model.Description = comic.Description;
            model.Price = comic.Price;
            model.PhotoUrl = comic.PhotoUrl;
            model.CategoryId = comic.CategoryId;
            model.CreatorId = comic.CreatorId;
            model.Reviews = comic.Reviews;
            model.CreatorName = comic.Creator.FullName;
            model.CategoryName = comic.Category.Name;

            return model;
        }

        public async Task AddComicAsync(AddComicViewModel model, Creator creator)
        {
            Comic comic = new Comic
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatorId = creator.Id,
                CategoryId = int.Parse(model.CategoryId),
                Price = model.Price,
                PhotoUrl = model.PhotoUrl
            };

            await db.Comics.AddAsync(comic);
            await db.SaveChangesAsync();
        }
    }
}
