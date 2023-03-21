using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;

namespace LibraryWebApi.Controllers
{
    public class BookController : ApiController

    {
        static List<Book> books = new List<Book>()
        {
            new Book {Id = 1, Title = "Laku noc, ljepoto", NumberOfPages = 406, Genre = "Triler"},
            new Book {Id = 2, Title = "Laku noc, ljepoto", NumberOfPages = 367, Genre = "Drama"},
            new Book {Id = 4, Title = "Sjeme razdora", NumberOfPages = 321, Genre = "Triler"},
            new Book {Id = 5, Title = "Laku noc, ljepoto", NumberOfPages = 795, Genre = "Horor"},
            new Book {Id = 6, Title = "Barsunasto obecanje", NumberOfPages = 302, Genre = "Ljubavni"},
            new Book {Id = 9, Title = "Cesta lijesova", NumberOfPages = 319, Genre = "Kriminalistika"},
        };
        public IEnumerable<Book> GetAllBooks()
        {
            return books;

        }
        public HttpResponseMessage GetBook(int id)
        {
            Book getbook = books.Where(b => b.Id == id).FirstOrDefault();
            if (getbook != null)
            {
                return Request.CreateResponse<Book>(HttpStatusCode.OK, getbook);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book not found.");
            }
        }
        public HttpResponseMessage Post([FromBody] Book addedBook)
        {
            if (addedBook != null)
            {
                books.Add(addedBook);
                return Request.CreateResponse<Book>(HttpStatusCode.Created, addedBook);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "You can not add it.");
            }
        }
        public HttpResponseMessage Put(int id, string newgenre)
        {
            Book bookToUpdate = books.Find(b => b.Id == id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Genre = "Thriler";
                return Request.CreateResponse(HttpStatusCode.OK, "You changed genre to the selected book.");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The selected book does not exist.");
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            Book removedbook = books.Find(b => b.Id == id);
            if (removedbook != null)
            {
                books.Remove(removedbook);
                return Request.CreateResponse(HttpStatusCode.Gone, "Book has been deleted");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no book with inserted Id");
            }
        }
        public HttpResponseMessage PutUri([FromUri] Book uriBook)
        {
            Book bookToUpdate = books.Find(b => b.Id == uriBook.Id);
            if (bookToUpdate != null)
            {
                //if(string.IsNullOrWhiteSpace(uriBook.Title))
                //{
                //    bookToUpdate.Title = bookToUpdate.Title;
                //}
                //else
                //{
                //    bookToUpdate.Title = uriBook.Title;
                //}

                bookToUpdate.Title = string.IsNullOrWhiteSpace(uriBook.Title) ? bookToUpdate.Title : uriBook.Title;
                return Request.CreateResponse(HttpStatusCode.OK, "You made changes.");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The selected book does not exist.");
            }
        }

        //localhost:44391/api/Book/1?Title=fndig&Genre=Ljubavni








    }
}
