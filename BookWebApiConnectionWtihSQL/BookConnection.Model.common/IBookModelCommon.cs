using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookConnection.Model.common
{
    public interface IBookModelCommon 
    {
        Guid Id { get; set; }
        string Title { get; set; }
        int NumberOfPages { get; set; }
        string Genre { get; set; }
        Guid AuthorId { get; set; }
    }
}
