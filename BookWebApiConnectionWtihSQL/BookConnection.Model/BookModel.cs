using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookConnection.Model
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public string Genre { get; set; }
        public Guid AuthorId { get; set; }
    }
}
