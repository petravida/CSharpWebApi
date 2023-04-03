using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookConnectionMVC.Models
{
    public class BookView
    {
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public string Genre { get; set; }
    }
}