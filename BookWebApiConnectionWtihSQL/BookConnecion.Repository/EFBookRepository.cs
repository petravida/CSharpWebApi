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
using System.Web.Http.ModelBinding;
using System.Data.Entity.Migrations;

namespace BookConnecion.Repository
{
    public class EFBookRepository : IBookRepository
    {
        protected BookContext Context { get; set; }
        public EFBookRepository(BookContext contex)
        {
            Context = contex;
        }

        public async Task<List<BookModelDTO>> GetBooksAsync(Pagination pagination, Sorting sorting, Filtering filtering)
        {
            //if (pagination != null)
            //{
            //    var bookPagination = Context.Book.Skip((pagination.PageNumber - 1) * pagination.PageSize)
            //        .Take(pagination.PageSize).ToListAsync();
            //    var totalBook = await Context.Book.CountAsync();
            //    return new List<BookModelDTO>(totalBook);

            //}
            List<BookModelDTO> booksList = new List<BookModelDTO>();
            List<Book> allBooks = await Context.Book.ToListAsync();
            foreach (var book in allBooks)
            {
                booksList.Add(new BookModelDTO()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    NumberOfPages = book.Number_of_pages,
                    AuthorId = book.Author_Id,
                });
            }
            return booksList;
        }

        public async Task<BookModelDTO> GetOneBookAsync(Guid id)
        {
            BookModelDTO findedBook = new BookModelDTO();
            Book book = await Context.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (id != null)
            {
                findedBook.Id = book.Id;
                findedBook.Title = book.Title;
                findedBook.Genre = book.Genre;
                findedBook.NumberOfPages = book.Number_of_pages;
                findedBook.AuthorId = book.Author_Id;
            }
            return findedBook;
            
        }

        public async Task<bool> PostOneBookAsync(BookModelDTO book)
        {
            book.Id = Guid.NewGuid();
            Context.Book.Add(new Book
            {
                Id = Guid.NewGuid(),
                Title = book.Title,
                Number_of_pages = book.NumberOfPages,
                Genre = book.Genre,
                Author_Id = book.AuthorId
            });
        
            var numberOfRows = await Context.SaveChangesAsync();

            if (numberOfRows > 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            BookModelDTO bookForDelete = new BookModelDTO();
            var book =  Context.Book.Where(b => b.Id == id).FirstOrDefault();
            if (id != null)
            {
                Context.Entry(book).State = EntityState.Deleted;
                await Context.SaveChangesAsync();
                return true;
            }
            return false;
            

        }    
        public async Task<bool> PutBookAsync(Guid id, BookModelDTO book)
        {
            Book updateBook = await Context.Book.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (updateBook != null)
            {
                updateBook.Title = book.Title;
                updateBook.Number_of_pages = book.NumberOfPages;
                updateBook.Genre = book.Genre;
                updateBook.Author_Id = book.AuthorId;

                Context.SaveChanges();
                return true;
            }
            else return false;

        }
    }
}
