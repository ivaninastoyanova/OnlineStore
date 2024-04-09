using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Models.Creator
{
    public class CreatorDetailsViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Biography { get; set; } = null!;

        public string PhotoUrl { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
