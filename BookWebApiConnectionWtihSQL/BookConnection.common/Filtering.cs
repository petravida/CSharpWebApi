using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookConnection.common
{
    public class Filtering
    {
        public Guid AuthorId { get; set; }  
        public int NumberOfBookPages { get; set; }  
        public string BookGenre { get; set; }   
        public string BookTitle  { get; set; }
    }
}
