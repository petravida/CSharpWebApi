using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookConnection.common
{
    public class Pagination
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public Pagination()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public Pagination(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber <1 ? 1 : pageNumber;
            this.PageSize = pageSize < 10 ? 10 : pageSize;
        }
    }
}
