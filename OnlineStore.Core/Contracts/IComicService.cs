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

    }
}
