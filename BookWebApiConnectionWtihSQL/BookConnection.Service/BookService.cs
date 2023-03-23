using BookConnecion.Repository;
using BookConnection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookConnection.Service
{
    public class BookService 
    {
       
        public List<BookModel> Get()
        {
            BookRepository bookRep = new BookRepository();
            var books = bookRep.GetBooks();
            return books;
        }
        //public bool Save(BookModel book)
        //{
        //    return true;
        //}
        
        public BookModel GetOneBook(Guid id)
        {
            BookRepository getOneBook = new BookRepository();
            var oneBook = getOneBook.GetOneBook(id);
            return oneBook;
        }
        public bool PostBook(BookModel newBook)
        {
            BookRepository postBook = new BookRepository();
            var isInserted = postBook.Post(newBook);
            return isInserted;
        }
        public bool DeleteBook(Guid id)
        {
            BookRepository goneBook = new BookRepository();
            var isDeleted = goneBook.DeleteBook(id);
            return isDeleted;
        }
        //public bool PutBook(Guid id, BookModel updateBook)
        //{
        //    BookRepository differentBook = new BookRepository();
        //    var isEdited = differentBook.Put(id, updateBook);
        //    return isEdited;
        //}


    }
}
