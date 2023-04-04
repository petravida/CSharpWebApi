﻿using System;
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
                            Id = book.Id,
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
                BookView bookView = new BookView
                { 
                    Id = id,
                    Title = getBook.Title,
                    NumberOfPages = getBook.NumberOfPages,
                    Genre = getBook.Genre,
                };
               
                return View(bookView);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                bool isDeleted = await Service.DeleteBookAsync(id);

                if (isDeleted == false)
                {
                    return View();
                }
                else
                {

                    return RedirectToAction("GetBooksAsync");
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        
        public async Task<ActionResult> Edit(BookPutView bookForEdit)
        {
            try
            {
                BookModelDTO editBook = new BookModelDTO();
                editBook.Id = bookForEdit.Id;
                editBook.Title = bookForEdit.Title;
                editBook.NumberOfPages = bookForEdit.NumberOfPages;
                editBook.Genre = bookForEdit.Genre;
                bool isUpdated = await Service.PutBookAsync(bookForEdit.Id, editBook);

                if (isUpdated == false)
                {
                    return View();
                }
                
                return View(bookForEdit);
                 
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BookPostView newBook)
        {
            try
            {
                BookModelDTO insertedBook = new BookModelDTO();
                insertedBook.Title = newBook.Title;
                insertedBook.NumberOfPages = newBook.NumberOfPages;
                insertedBook.Genre = newBook.Genre;
                insertedBook.AuthorId = newBook.AuthorId;
                bool isInserted = await Service.PostOneBookAsync(insertedBook);
                if (isInserted == true)
                {
                    return RedirectToAction("GetBooksAsync");
                }
                else
                {
                    return View();
                }
            }

            catch (Exception ex)
            {
                return View(ex);
            }
        }

    }
}