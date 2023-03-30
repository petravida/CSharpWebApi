using BookConnection.common;
using DAL;
using BookConnection.Model;
using BookConnection.Repository.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BookConnecion.Repository
{
    public class EFBookRepository : IBookRepository
    {
        protected BookContext context { get; set; }
        public EFBookRepository(BookContext Contex)
        {
            Contex = context;
        }

        public Task<List<BookModel>> GetBooksAsync(Pagination pagination, Sorting sorting, Filtering filtering)
        {
            throw new NotImplementedException();
        }

        public Task<BookModel> GetOneBookAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostOneBookAsync(BookModel book)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            Book bookToDelete = await context.Book.SingleOrDefaultAsync(b => b.Id == id);
            if (bookToDelete != null)
            {
                context.Book.Remove(bookToDelete);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Task<bool> PutBookAsync(Guid id, BookModel book)
        {
            throw new NotImplementedException();
        }
    }
}
