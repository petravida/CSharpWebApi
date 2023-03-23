using BookConnection.Model;
using BookConnection.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml;

namespace BookWebApiConnectionWtihSQL.Controllers
{
   

    public class BookController : ApiController
    {
        static string connectionString = "Data Source=LAPTOP-PT3M9TGC;Initial Catalog=Books;Integrated Security=True";
        [HttpGet]
        [Route("api/Books")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                BookService bookService = new BookService();
                List<BookConnection.Model.BookModel> listOfBooks = bookService.Get();
                if (listOfBooks == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no Books.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, listOfBooks);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("api/getBook/{id}")]
        public HttpResponseMessage GetOneBook(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                var book = bookService.GetOneBook(id);
                
                if (book == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no Book with {id} Id.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, book);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        [Route("api/Book/postBook")]
        public HttpResponseMessage PostOneBook([FromBody] BookModel newBook)
        {
            try
            {
                BookService bookService = new BookService();
                var isInserted = bookService.PostBook(newBook);

                //BookConnection.Model.Book addedbook = bookService.PostBook(newBook);
                if (isInserted == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Uspjesno");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "There is a prolem with posting new book.");
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/Book/deleteBook/{id}")]
        public HttpResponseMessage DeleteOneBook(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                var isDeleted = bookService.DeleteBook(id);

                if (isDeleted == false)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no book with {id} Id");
                }
                else
                {
                    
                    return Request.CreateResponse(HttpStatusCode.OK, "Book has been deleted.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
        //[HttpPut]
        //[Route("api/Book/putBook/{id}")]
        //public HttpResponseMessage PutBook([FromUri] Guid id, [FromBody] BookModel updateBook)
        //{
        //    try
        //    {
        //        BookService bookService = new BookService();
        //        var isDeleted = bookService.PutBook(id);

        //        if (isDeleted == false)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no book with {id} Id");
        //        }
        //        else
        //        {

        //            return Request.CreateResponse(HttpStatusCode.OK, "Book has been deleted.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
        //    }
        //}






    }

}
