using BookConnection.common;
using BookConnection.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookConnection.Repository.common
{
    public interface IBookRepository
    {
        Task<IPagedList<BookModelDTO>> GetBooksAsync(SearchString searchString, Pagination pagination, Sorting sorting, Filtering filtering);
        Task<BookModelDTO> GetOneBookAsync(Guid id);
        Task<bool> PostOneBookAsync(BookModelDTO book);
        Task<bool> DeleteBookAsync(Guid id);
        Task<bool> PutBookAsync(Guid id, BookModelDTO book);

    }
}
