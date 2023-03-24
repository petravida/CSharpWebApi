using BookConnection.Model;
using BookConnection.Service;
using Microsoft.Ajax.Utilities;
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
using System.Threading.Tasks;
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
        public async Task<HttpResponseMessage> GetBooksAsync()
        {
            try
            {
                BookService bookService = new BookService();
                Task<List<BookModel>> listOfBooks = bookService.GetBooksAsync();
                if (await listOfBooks == null)
                {
                   return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no Books.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, listOfBooks.Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("api/getBook/{id}")]
        public async Task<HttpResponseMessage> GetOneBookAsync(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                Task<BookModel> book = bookService.GetOneBookAsync(id);
                
                if (await book == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no Book with {id} Id.");
                }
                else 
                {
                    return Request.CreateResponse(HttpStatusCode.OK, book.Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        [Route("api/Book/postBook")]
        public async Task<HttpResponseMessage> PostOneBookAsync([FromBody] BookModel newBook)
        {
            try
            {
                BookService bookService = new BookService();
                Task<bool> isInserted = bookService.PostOneBookAsync(newBook);

                if (await isInserted == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, newBook);
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
        public async Task<HttpResponseMessage> DeleteBookAsync(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                Task<bool> isDeleted = bookService.DeleteBookAsync(id);

                if (await isDeleted == false)
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
        [HttpPut]
        [Route("api/Book/putOneBook/{id}")]
        public async Task<HttpResponseMessage> PutBookAsync(Guid id, BookModel updateBook)
        {
            try
            {
                BookService bookService = new BookService();
                Task<bool> isUpdated = bookService.PutBookAsync(id, updateBook);

                if (await isUpdated == false)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no book with {id} Id");
                }
        
                  return Request.CreateResponse(HttpStatusCode.OK, "Book is updated.");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }






    }

}
