using OnlineStore.Core.Models.Comic;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Contracts
{
    public interface IComicService
    {
        public Task AddComicAsync(AddComicViewModel model, Creator creator);

        public Task<AllComicsFilteredAndOrdered> AllAsync(ComicAllQueryModel queryModel);

        public Task<ComicDetailsViewModel> GetComicAsync(ComicDetailsViewModel model, int id);

        public Task<bool> DeleteComic(int id);

        public Task EditComicAsync(AddComicViewModel model, int id);

        public AddComicViewModel FindComic(int id);

        public  Task<bool> ComicExistsAsync(int id);

    }
}
