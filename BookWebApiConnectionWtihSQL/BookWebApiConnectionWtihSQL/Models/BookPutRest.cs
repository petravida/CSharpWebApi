using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWebApiConnectionWtihSQL.Models
{
    public class BookPutRest
    {
        public string Title { get; set; }   
        public int NumberOfPages { get; set; }
        public string Genre { get; set; }   
    }
}