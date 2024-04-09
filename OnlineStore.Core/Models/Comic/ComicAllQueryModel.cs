using OnlineStore.Core.Models.Comic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnlineStore.Core.Models.Comic
{
    public class ComicAllQueryModel
    {
        public ComicAllQueryModel()
        {
            CurrentPage = 1;
            ComicsPerPage = 6;

            Categories = new List<string>();
            Comics = new List<ComicAllViewModel>();
        }

        public string? CategoryName { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Comics by")]
        public ComicSortEnum ComicSorting { get; set; }

        public int CurrentPage { get; set; }

        public int ComicsPerPage { get; set; }

        public int TotalComics { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<ComicAllViewModel> Comics { get; set; }
    }
}
