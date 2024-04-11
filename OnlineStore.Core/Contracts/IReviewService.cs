using OnlineStore.Core.Models.Reviews;
using OnlineStore.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Contracts
{
    public interface IReviewService
    {
        public Task Add(ReviewAddFormModel model);

        public Task RemoveAsync(Guid id);

        public Task<Review> FindReviewAsync(Guid id);
    }
}
