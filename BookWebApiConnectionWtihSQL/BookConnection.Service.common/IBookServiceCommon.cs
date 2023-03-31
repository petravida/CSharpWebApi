using BookConnection.common;
using BookConnection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookConnection.Service.common
{
    public interface IBookService
    {
        Task<List<BookModelDTO>> GetBooksAsync(Pagination pagination, Sorting sorting, Filtering filtering);
        Task<BookModelDTO> GetOneBookAsync(Guid id);
        Task<bool> PostOneBookAsync(BookModelDTO book);
        Task<bool> DeleteBookAsync(Guid id);
        Task<bool> PutBookAsync(Guid id, BookModelDTO book);


    }
}
