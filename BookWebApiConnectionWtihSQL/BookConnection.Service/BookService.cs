using BookConnecion.Repository;
using BookConnection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookConnection.Service
{
    public class BookService 
    {
       
        public async Task<List<BookModel>> GetBooksAsync()
        {
            BookRepository bookRep = new BookRepository();
            Task<List<BookModel>> books = bookRep.GetBooksAsync();
            return await books;
        }
        //public bool Save(BookModel book)
        //{
        //    return true;
        //}
        
        public async Task<BookModel> GetOneBookAsync(Guid id)
        {
            BookRepository getOneBook = new BookRepository();
            Task<BookModel> oneBook = getOneBook.GetOneBookAsync(id);
            return await oneBook;
        }
        public async Task<bool> PostOneBookAsync(BookModel newBook)
        {
            BookRepository postBook = new BookRepository();
            Task<bool> isInserted = postBook.PostOneBookAsync(newBook);
            return await isInserted;
        }
        public async Task<bool> DeleteBookAsync(Guid id)
        {
            BookRepository goneBook = new BookRepository();
            Task<bool> isDeleted = goneBook.DeleteBookAsync(id);
            return await isDeleted;
        }
        public async Task<bool> PutBookAsync(Guid id, BookModel updateBook)
        {
            BookModel findBook = await GetOneBookAsync(id);

            if ( findBook == null)
            {
             
               return false;
            }
            
           BookModel bookForUpdate = new BookModel();
            bookForUpdate.Title = updateBook.Title == default ? findBook.Title : updateBook.Title;
           bookForUpdate.NumberOfPages = updateBook.NumberOfPages == default ? findBook.NumberOfPages : updateBook.NumberOfPages;
            bookForUpdate.Genre = updateBook.Genre == default ? findBook.Genre : updateBook.Genre;
          bookForUpdate.AuthorId = updateBook.AuthorId == default ? findBook.AuthorId : updateBook.AuthorId;
            BookRepository differentBook = new BookRepository();

            Task<bool> isEdited = differentBook.PutBookAsync(id, bookForUpdate);
            return await isEdited;
        }


    }
}
