using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Constants
{
    public class DataConstants
    {
        //Comic Constants
        public const int ComicTitleMinLength = 5;
        public const int ComicTitleMaxLength = 50;

        public const int ComicDescriptionMinLength = 10;
        public const int ComicDescriptionMaxLength = 1000;

        public const double ComicPriceMin = 0.01;
        public const double ComicPriceMax = 10000;

        //Creator Constants
        public const int CreatorNameMinLength = 5;
        public const int CreatorNameMaxLength = 50;

        public const int CreatorBiographyMinLength = 5;
        public const int CreatorBiographyMaxLength = 1000;

        //Category Constants
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 30;

        //Review Constants
        public const int ReviewTextMinLength = 2;
        public const int ReviewTextMaxLength = 150;

        public const int ReviewStarRatingMin = 1;
        public const int ReviewStarRatingMax = 5;

        //Photo Constants
        public const int PhotoUrlMaxLength = 3000;
    }
}
