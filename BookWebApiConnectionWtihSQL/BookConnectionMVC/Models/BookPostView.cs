using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookConnectionMVC.Models
{
    public class BookPostView
    {
        private string genre;

        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public string Genre { get => genre; set => genre = value; }
        public Guid AuthorId { get; set; }
        public string TypeOfLiterature { get; set; }
    }
}