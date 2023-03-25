using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BookWebApiConnectionWtihSQL.Models
{
    public class BookGetRest
    {
        public string Title { get; set; }  
        public int NumberOfPages { get; set; }  
        public string Genre { get; set; }  
    }
}