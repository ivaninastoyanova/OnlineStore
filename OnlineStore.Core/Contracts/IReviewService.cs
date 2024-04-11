using OnlineStore.Core.Models.Reviews;
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
    }
}
