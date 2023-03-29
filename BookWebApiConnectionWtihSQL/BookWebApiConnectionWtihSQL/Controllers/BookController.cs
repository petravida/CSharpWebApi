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
using System.Threading.Tasks;
using System.Web.Http;
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

        static string connectionString = "Data Source=LAPTOP-PT3M9TGC;Initial Catalog=Books;Integrated Security=True";
        [HttpGet]
        [Route("api/Books")]
        public async Task<HttpResponseMessage> GetBooksAsync(Pagination pagination, Sorting sorting)
        {
            try
            {
                //BookService bookService = new BookService();
                
                List<BookModel> listOfBooks = await Service.GetBooksAsync(pagination, sorting);
                List<BookGetRest> bookRestList = new List<BookGetRest>();
                Pagination newPagination = new Pagination();
                {
                    newPagination = pagination;
                    pagination.PageSize = newPagination.PageSize;
                    pagination.PageNumber = newPagination.PageNumber;

                }
                Sorting newsorting = new Sorting();
                {
                    newsorting = sorting;
                    sorting.SortBy = newsorting.SortBy;
                    sorting.SortOrder = newsorting.SortOrder;

                }

                if ( listOfBooks == null)
                {
                    return  Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    foreach (BookModel book in listOfBooks) 
                    { 
                        BookGetRest bookRest = new BookGetRest();
                        bookRest.Title = book.Title;
                        bookRest.NumberOfPages = book.NumberOfPages;
                        bookRest.Genre = book.Genre;
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
                BookModel getBook = await Service.GetOneBookAsync(id);
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
                BookModel insertedBook = new BookModel();
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
                BookModel editBook = new BookModel();
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
