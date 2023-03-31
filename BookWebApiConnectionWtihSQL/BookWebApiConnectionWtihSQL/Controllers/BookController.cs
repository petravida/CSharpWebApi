using BookConnection.common;
using BookConnection.Model;
using BookConnection.Repository.common;
using BookConnection.Service;
using BookConnection.Service.common;
using BookWebApiConnectionWtihSQL.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BookWebApiConnectionWtihSQL.Controllers
{


    public class BookController : ApiController
    {
        protected IBookService Service { get; set; }

        public BookController(IBookService service)
        {
            Service = service;
        }

        //static string connectionString = "Data Source=LAPTOP-PT3M9TGC;Initial Catalog=Books;Integrated Security=True";
        [HttpGet]
        [Route("api/Books")]
        //public async Task<HttpResponseMessage> GetBooksAsync(Pagination pagination, Sorting sorting)
        public async Task<HttpResponseMessage> GetBooksAsync(int pageNumber = 1, int pageSize = 5, string sortBy = "Id", string sortOrder = "Asc", string bookTitle = null, int numberofBookPages = 0, string bookGenre = null)
        {
            try
            {
                Pagination pagination = new Pagination
                {
                    PageNumber = pageNumber,    
                    PageSize = pageSize
                };

                Sorting sorting = new Sorting
                {
                    SortBy = sortBy,
                    SortOrder = sortOrder
                };
                Filtering filtering = new Filtering
                {
                    BookGenre = bookGenre ?? null,
                    BookTitle = bookTitle ?? null,
                    NumberOfBookPages = numberofBookPages

                };
                //BookService bookService = new BookService();

                List<BookModelDTO> listOfBooks = await Service.GetBooksAsync(pagination, sorting, filtering);
                List<BookGetRest> bookRestList = new List<BookGetRest>();
                
                if ( listOfBooks == null)
                {
                    return  Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    foreach (BookModelDTO book in listOfBooks) 
                    {
                        BookGetRest bookRest = new BookGetRest
                        {
                            Title = book.Title,
                            NumberOfPages = book.NumberOfPages,
                            Genre = book.Genre
                        };
                        bookRestList.Add(bookRest);

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, bookRestList);   
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
                //BookService bookService = new BookService();
                BookModelDTO getBook = await Service.GetOneBookAsync(id);
                if ( getBook == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no Book with {id} Id.");
                }
                else 
                {
                    BookGetRest bookRest = new BookGetRest();
                    bookRest.Title = getBook.Title;
                    bookRest.NumberOfPages = getBook.NumberOfPages;
                    bookRest.Genre = getBook.Genre;
                    return Request.CreateResponse(HttpStatusCode.OK, bookRest);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        [Route("api/postBook")]
        public async Task<HttpResponseMessage> PostOneBookAsync(BookPostRest bookPost)
        {
            try
            {
                //BookService bookService = new BookService();
                BookModelDTO insertedBook = new BookModelDTO();
                insertedBook.Title = bookPost.Title;
                insertedBook.NumberOfPages = bookPost.NumberOfPages;
                insertedBook.Genre = bookPost.Genre;
                insertedBook.AuthorId = bookPost.AuthorId;
                insertedBook.TypeOfLiterature = bookPost.TypeOfLiterature;
                bool isInserted = await Service.PostOneBookAsync(insertedBook);
                if ( isInserted == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, insertedBook);
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
        [Route("api/deleteBook/{id}")]
        public async Task<HttpResponseMessage> DeleteBookAsync(Guid id)
        {
            try
            {
                //BookService bookService = new BookService();
                Task<bool> isDeleted = Service.DeleteBookAsync(id);

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
        [Route("api/putBook/{id}")]
        public async Task<HttpResponseMessage> PutBookAsync(Guid id, BookPutRest bookPut)
        {
            try
            {
                BookModelDTO editBook = new BookModelDTO();
                editBook.Title = bookPut.Title;
                editBook.NumberOfPages = bookPut.NumberOfPages;
                editBook.Genre = bookPut.Genre;
                //BookService bookService = new BookService();
                bool isUpdated = await Service.PutBookAsync(id, editBook);

                if ( isUpdated == false)
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
