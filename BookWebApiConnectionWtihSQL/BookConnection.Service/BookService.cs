using BookConnecion.Repository;
using BookConnection.common;
using BookConnection.Model;
using BookConnection.Repository.common;
using BookConnection.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookConnection.Service 
{
    public class BookService : IBookService
    {
        protected IBookRepository Repository { get; set; }

        public BookService(IBookRepository repository)
        {
            Repository = repository;
        }

    
        

        public async Task<List<BookModel>> GetBooksAsync(Pagination pagination, Sorting sorting, Filtering filtering)
        {
            //BookRepository bookRep = new BookRepository();
            Task<List<BookModel>> books = Repository.GetBooksAsync(pagination, sorting, filtering);

            return await books;
        }
        //public bool Save(BookModel book)
        //{
        //    return true;
        //}
        public async Task<BookModel> GetOneBookAsync(Guid id)
        {
            //BookRepository getOneBook = new BookRepository();
            Task<BookModel> oneBook = Repository.GetOneBookAsync(id);
            return await oneBook;
        }
        public async Task<bool> PostOneBookAsync(BookModel book)
        {
            //BookRepository postBook = new BookRepository();
            Task<bool> isInserted = Repository.PostOneBookAsync(book);
            return await isInserted;
        }
        public async Task<bool> DeleteBookAsync(Guid id)
        {
            //BookRepository goneBook = new BookRepository();
            Task<bool> isDeleted = Repository.DeleteBookAsync(id);
            return await isDeleted;
        }
        public async Task<bool> PutBookAsync(Guid id, BookModel book)
        {
            BookModel findBook = await GetOneBookAsync(id);

            if ( findBook == null)
            {
             
               return false;
            }
            
           BookModel bookForUpdate = new BookModel();
            bookForUpdate.Title = book.Title == default ? findBook.Title : book.Title;
           bookForUpdate.NumberOfPages = book.NumberOfPages == default ? findBook.NumberOfPages : book.NumberOfPages;
            bookForUpdate.Genre = book.Genre == default ? findBook.Genre : book.Genre;
            bookForUpdate.AuthorId = book.AuthorId == default ? findBook.AuthorId : book.AuthorId;
            //BookRepository differentBook = new BookRepository();

            Task<bool> isEdited = Repository.PutBookAsync(id, bookForUpdate);
            return await isEdited;
        }


    }
}
