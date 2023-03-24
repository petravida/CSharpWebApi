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
       
        public List<BookModel> GetBooks()
        {
            BookRepository bookRep = new BookRepository();
            List<BookModel> books = bookRep.GetBooks();
            return books;
        }
        //public bool Save(BookModel book)
        //{
        //    return true;
        //}
        
        public BookModel GetOneBook(Guid id)
        {
            BookRepository getOneBook = new BookRepository();
            BookModel oneBook = getOneBook.GetOneBook(id);
            return oneBook;
        }
        public bool PostOneBook(BookModel newBook)
        {
            BookRepository postBook = new BookRepository();
            bool isInserted = postBook.PostOneBook(newBook);
            return isInserted;
        }
        public bool DeleteBook(Guid id)
        {
            BookRepository goneBook = new BookRepository();
            bool isDeleted = goneBook.DeleteBook(id);
            return isDeleted;
        }
        public bool PutBook(Guid id, BookModel updateBook)
        {
            BookModel findBook = GetOneBook(id);

            if (findBook == null)
            {
                return false;
            }
            BookModel bookForUpdate = new BookModel();
            bookForUpdate.Title = updateBook.Title == default ? findBook.Title : updateBook.Title;
            bookForUpdate.NumberOfPages = updateBook.NumberOfPages == default ? findBook.NumberOfPages : updateBook.NumberOfPages;
            bookForUpdate.Genre = updateBook.Genre == default ? findBook.Genre : updateBook.Genre;
            bookForUpdate.AuthorId = updateBook.AuthorId == default ? findBook.AuthorId : updateBook.AuthorId;
            BookRepository differentBook = new BookRepository();

            bool isEdited = differentBook.PutBook(id, bookForUpdate);
            return isEdited;
        }


    }
}
