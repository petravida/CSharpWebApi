using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BookConnection.Model;
using BookConnection.Service.common;
using BookConnection.common;
using BookConnectionMVC.Models;
using DAL;

namespace BookConnectionMVC.Controllers
{
    public class BookController : Controller
    {
        protected IBookService Service { get; set; }

        public BookController(IBookService service)
        {
            Service = service;
        }
        public async Task<ActionResult> GetBooksAsync(int pageNumber = 1, int pageSize = 100, string sortBy = "Id", string sortOrder = "Asc", string bookTitle = null, int numberofBookPages = 0, string bookGenre = null)
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
                List<BookModelDTO> listOfBooks = await Service.GetBooksAsync(pagination, sorting, filtering);
                List<BookView> booksMappedList = new List<BookView>();

                if (listOfBooks == null)
                {
                    return View();
                }
                else
                {
                    foreach (BookModelDTO book in listOfBooks)
                    {
                        BookView booksList = new BookView
                        {
                            Title = book.Title,
                            NumberOfPages = book.NumberOfPages,
                            Genre = book.Genre
                        };
                        booksMappedList.Add(booksList);

                    }
                    return View(booksMappedList);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        public async Task<ActionResult> Details(Guid id)
        {
            try
            {
                BookModelDTO getBook = await Service.GetOneBookAsync(id);
                if (getBook == null)
                {
                    return View();
                }
                else
                {
                    BookView bookView = new BookView();
                    bookView.Title = getBook.Title;
                    bookView.NumberOfPages = getBook.NumberOfPages;
                    bookView.Genre = getBook.Genre;
                    return View(bookView);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        //public async Task<ActionResult> Delete(Guid id)
        //{
        //    try
        //    {
        //        bool isDeleted = await Service.DeleteBookAsync(id);

        //        if (isDeleted == false)
        //        {
        //            return View();
        //        }
        //        else
        //        {

        //            return RedirectToAction("GetBooksAsync");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(ex);
        //    }

        //}
        //public async Task<ActionResult> PostOneBookAsync(BookPostView bookPost)
        //{
        //    try
        //    {

        //        BookModelDTO insertedBook = new BookModelDTO();
        //        insertedBook.Title = bookPost.Title;
        //        insertedBook.NumberOfPages = bookPost.NumberOfPages;
        //        insertedBook.Genre = bookPost.Genre;
        //        insertedBook.AuthorId = bookPost.AuthorId;
        //        insertedBook.TypeOfLiterature = bookPost.TypeOfLiterature;
        //        bool isInserted = await Service.PostOneBookAsync(insertedBook);
        //        if (isInserted == true)
        //        {
        //            return View(bookPost);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        return View(ex);
        //    }
        //}
        //public async Task<ActionResult> Edit(Guid id)
        //{
        //    try
        //    {
        //        BookModelDTO getBook = await Service.GetOneBookAsync(id);
        //        if (getBook == null)
        //        {
        //            return View();
        //        }
        //        else
        //        {
        //            BookView bookView = new BookView();
        //            bookView.Title = getBook.Title;
        //            bookView.NumberOfPages = getBook.NumberOfPages;
        //            bookView.Genre = getBook.Genre;
        //            return View(bookView);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(ex);
        //    }
        //}
        //public async Task<ActionResult> PutBookAsync(Guid id, BookPutView bookPut)
        //{
        //    try
        //    {
        //        BookModelDTO editBook = new BookModelDTO();
        //        editBook.Title = bookPut.Title;
        //        editBook.NumberOfPages = bookPut.NumberOfPages;
        //        editBook.Genre = bookPut.Genre;
        //        bool isUpdated = await Service.PutBookAsync(id, editBook);

        //        if (isUpdated == false)
        //        {
        //            return View("There is no book with {id} Id");
        //        }

        //        return View("Book is updated.");

        //    }
        //    catch (Exception ex)
        //    {
        //        return View(ex);
        //    }
        //}
        // GET: Book
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}