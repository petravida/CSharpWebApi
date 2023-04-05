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
using System.Runtime.Remoting.Contexts;
using System.Runtime.InteropServices;
using System.Web.Http.Results;
using System.Reflection;
using System.Net.NetworkInformation;
using PagedList;

namespace BookConnecion.Repository
{
    public class EFBookRepository : IBookRepository
    {
        protected BookContext Context { get; set; }
        public EFBookRepository(BookContext contex)
        {
            Context = contex;
        }

        public async Task<IPagedList<BookModelDTO>> GetBooksAsync(SearchString searchString, Pagination pagination, Sorting sorting, Filtering filtering)
        {
            IQueryable<Book> query = Context.Book.AsQueryable();
            //int pageNumber = (pagination.PageNumber ?? 1);

            if (filtering != null)
            {
                if (filtering.BookTitle != null)
                {
                    query = query.Where(b => b.Title.Contains(filtering.BookTitle));
                }
                if (filtering.BookGenre != null)
                {
                    query = query.Where(b => b.Genre.Contains(filtering.BookGenre));
                }
                if (filtering.NumberOfBookPages > 0)
                {
                    query = query.Where(b => b.Number_of_pages >= filtering.NumberOfBookPages);
                }
            }
            if (!String.IsNullOrEmpty(searchString.SearchQueary))
            {
                string searchQuery = searchString.SearchQueary.ToLower();
                query = query.Where(b => b.Title.Contains(searchQuery));

            }
            if (pagination == null)
            {
                int offsetCount = (pagination.PageNumber - 1) * pagination.PageSize;
                int pageSize = pagination.PageSize;

                query = query.OrderBy(b => b.Id).Skip(offsetCount).Take(pageSize);
            }
            if (sorting != null)
            {
                string sortBy = sorting.SortBy;
                string sortOrder = sorting.SortOrder;
                int offsetCount = (pagination.PageNumber - 1) * pagination.PageSize;
                int pageSize = pagination.PageSize;

                switch (sortBy.ToLower())
                {
                    case "title":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(b => b.Title) : query.OrderBy(b => b.Title);
                        if (pagination != null)
                        {
                            query = query.OrderBy(b => b.Title).Skip(offsetCount).Take(pageSize);
                        }
                        break;   
                    case "numberofpages":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(b => b.Number_of_pages) : query.OrderBy(b => b.Number_of_pages);
                        if (pagination != null)
                        {
                            query = query.OrderBy(b => b.Number_of_pages).Skip(offsetCount).Take(pageSize);
                        }
                        break;
                    default:
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(b => b.Id) : query.OrderBy(b => b.Id);
             
                        break;
                }
            }
            
            if (query.Count() == 0)
            {
                return null;
            }
            IQueryable<BookModelDTO> books = query.Select(b => new BookModelDTO
            {
                Id = b.Id,
                Title = b.Title,
                Genre = b.Genre,
                NumberOfPages = b.Number_of_pages,
                AuthorId = b.Author_Id
            });
            
            return books.ToPagedList(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<BookModelDTO> GetOneBookAsync(Guid id)
        {
            Book book = new Book();
            book = await Context.Book.Where(b => b.Id == id).FirstOrDefaultAsync();
            BookModelDTO findedBook = new BookModelDTO();
            if (book != null)
            {
                findedBook.Id = id;
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
