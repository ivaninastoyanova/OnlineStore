using OnlineStore.Core.Models.Comic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Models.Comic
{
    public class AllComicsFilteredAndOrdered
    {
        public AllComicsFilteredAndOrdered()
        {
            Comics = new List<ComicAllViewModel>();
        }

        public int TotalComicsCount { get; set; }

        public IEnumerable<ComicAllViewModel> Comics { get; set; }
    }
}