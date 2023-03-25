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
        Task<List<BookModel>> GetBooksAsync();
        Task<BookModel> GetOneBookAsync(Guid id);
        Task<bool> PostOneBookAsync(BookModel book);
        Task<bool> DeleteBookAsync(Guid id);
        Task<bool> PutBookAsync(Guid id, BookModel book);


    }
}
