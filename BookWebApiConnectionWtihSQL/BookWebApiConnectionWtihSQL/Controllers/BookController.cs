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
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand getAll = new SqlCommand("SELECT * FROM Book", connection);
                    List<Book> books = new List<Book>();
                    connection.Open();
                    SqlDataReader allreader = getAll.ExecuteReader();
                    if (allreader.HasRows)
                    {
                        while (allreader.Read())
                        {
                            Book book = new Book();
                            book.Id = allreader.GetGuid(0);
                            book.Title = allreader.GetString(1);
                            book.NumberOfPages = allreader.GetInt32(2);
                            book.Genre = allreader.IsDBNull(3) ? null : allreader.GetString(3);
                            book.AuthorId = allreader.GetGuid(4);
                            books.Add(book);
                        }
                        allreader.Close();
                        return Request.CreateResponse(HttpStatusCode.OK, books);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"There is no books in this table.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }


        }
        [HttpGet]
        [Route("api/Book/getBookByPage")]
        public HttpResponseMessage GetBookByPage()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    SqlCommand getBook = new SqlCommand("SELECT * FROM Book WHERE [Number of pages] > 350", connection);
                    connection.Open();
                    SqlDataReader numberOfPagesReader = getBook.ExecuteReader();
                    List<Book> bigBooks = new List<Book>();

                    if (numberOfPagesReader.HasRows)
                    {

                        while (numberOfPagesReader.Read())
                        {
                            Book findBook = new Book();
                            findBook.Id = numberOfPagesReader.GetGuid(0);
                            findBook.Title = numberOfPagesReader.GetString(1);
                            findBook.NumberOfPages = numberOfPagesReader.GetInt32(2);
                            findBook.Genre = numberOfPagesReader.IsDBNull(3) ? null : numberOfPagesReader.GetString(3);
                            findBook.AuthorId = numberOfPagesReader.GetGuid(4);
                            bigBooks.Add(findBook);

                        }
                        numberOfPagesReader.Close();
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no book with more then 350 pages.");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, bigBooks);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

        [HttpGet]
        [Route("api/Book/{id}")]
        public HttpResponseMessage GetBook(Guid id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    SqlCommand getBook = new SqlCommand("SELECT * FROM Book WHERE @Id = id", connection);
                    getBook.Parameters.AddWithValue("@Id", id);
                    Book findBook = new Book();
                    connection.Open();
                    SqlDataReader getReader = getBook.ExecuteReader();

                    if (getReader.HasRows)
                    {

                        while (getReader.Read())

                        {
                            findBook.Id = getReader.GetGuid(0);
                            findBook.Title = getReader.GetString(1);
                            findBook.NumberOfPages = getReader.GetInt32(2);
                            findBook.Genre = getReader.IsDBNull(3) ? null : getReader.GetString(3);
                            findBook.AuthorId = getReader.GetGuid(4);

                        }
                        getReader.Close();
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no book with {id} Id.");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, findBook);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        [Route("api/Book/postBook")]
        public HttpResponseMessage PostBook([FromBody] Book addBook)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    addBook.Id = Guid.NewGuid();
                    SqlCommand commandInsert = new SqlCommand("INSERT INTO Book VALUES (@Id, @Title, @NumberOfPages, @Genre, @Author_Id)", connection);
           
                    commandInsert.Parameters.AddWithValue("@Id", addBook.Id);
                    commandInsert.Parameters.AddWithValue("@Title", addBook.Title);
                    commandInsert.Parameters.AddWithValue("@NumberOfPages", addBook.NumberOfPages);
                    commandInsert.Parameters.AddWithValue("@Genre", addBook.Genre);
                    commandInsert.Parameters.AddWithValue("@Author_Id", addBook.AuthorId);
                    connection.Open();
                    int numberOfRows = commandInsert.ExecuteNonQuery();
                    connection.Close();

                    if (numberOfRows > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Accepted, addBook);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.Accepted, "You added new book.");
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/Book/deleteBook/{id}")]
        public HttpResponseMessage DeleteBook(Guid id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
                {
                    SqlCommand deleteCommand = new SqlCommand("DELETE FROM Book WHERE @Id = id", connection);
                    deleteCommand.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    
                    int numberOfRows = deleteCommand.ExecuteNonQuery();
                    connection.Close();
                    if (numberOfRows > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Gone, "Book is deleted.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"There is no Book with {id} Id.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
        [HttpPut]
        [Route("api/Book/putBook/{id}")]
        public HttpResponseMessage PutBook([FromUri] Guid id, [FromBody] Book updateBook)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (connection)
                {
                    SqlCommand selectcommand = new SqlCommand("SELECT * FROM Book WHERE @Id = id", connection);
                    selectcommand.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    Book book = new Book();
                    SqlDataReader reader = selectcommand.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"There is no book with {id} Id.");
                    }
                    reader.Read();
                    book.Id = id;
                    book.Title = reader.GetString(1);
                    book.NumberOfPages = reader.GetInt32(2);
                    book.Genre = reader.IsDBNull(3) ? null : reader.GetString(3);
                    book.AuthorId = reader.GetGuid(4);
                    reader.Close();

                    SqlCommand putCommand = new SqlCommand("UPDATE Book set title=@Title, [number of pages]=@NumberOfPages, genre=@Genre, author_id=@AuthorId where @Id = id", connection);
                    putCommand.Parameters.AddWithValue("@Id", id);
                    putCommand.Parameters.AddWithValue("@Title", string.IsNullOrWhiteSpace(updateBook.Title) ? book.Title : updateBook.Title);
                    putCommand.Parameters.AddWithValue("@NumberOfPages", string.IsNullOrWhiteSpace(updateBook.NumberOfPages.ToString()) ? book.NumberOfPages.ToString() : updateBook.NumberOfPages.ToString());
                    putCommand.Parameters.AddWithValue("@Genre", string.IsNullOrWhiteSpace(updateBook.Genre) ? book.Genre : updateBook.Genre);
                    putCommand.Parameters.AddWithValue("@AuthorId", book.AuthorId.ToString());

                    int numberOfAffectedRows = putCommand.ExecuteNonQuery();
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.Gone, "Book is updated.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }






    }

}
