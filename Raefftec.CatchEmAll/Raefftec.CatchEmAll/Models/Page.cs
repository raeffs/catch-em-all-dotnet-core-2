using System.Collections.Generic;

namespace Raefftec.CatchEmAll.Models
{
    public class Page<T>
    {
        public int TotalItemCount { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public IList<T> Items { get; set; }
    }
}
