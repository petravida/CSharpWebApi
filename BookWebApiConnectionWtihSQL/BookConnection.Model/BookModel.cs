using BookConnection.Model.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookConnection.Model
{
    public class BookModel : IBookModel
    {
        private string genre;

        public Guid Id { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public string Genre { get => genre; set => genre = value; }
        public Guid AuthorId { get; set; }
        public string TypeOfLiterature { get; set; }

    }
}
