using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace BookWebApiConnectionWtihSQL.Models
{
    public class BookPostRest
    {
        private string genre;

        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public string Genre { get => genre; set => genre = value; }
        public Guid AuthorId { get; set; }
        public string TypeOfLiterature { get; set; }

    }
}