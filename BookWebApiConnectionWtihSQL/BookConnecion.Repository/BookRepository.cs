using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookConnection.common;
using BookConnection.Repository.common;
using BookConnection.Model;
using System.Web.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace BookConnecion.Repository 
{
    public class BookRepository : IBookRepository
    {
        public BookRepository()
        {

        }
        static string connectionString = "Data Source=LAPTOP-PT3M9TGC;Initial Catalog=Books;Integrated Security=True";

        //public async Task<List<BookModel>> GetBooksAsync(Pagination pagination, Sorting sorting)
        public async Task<List<BookModel>> GetBooksAsync(Pagination pagination, Sorting sorting)

        {
            StringBuilder stringBuilder = new StringBuilder("SELECT * FROM Book ");
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand getAll = new SqlCommand();
            List<BookModel> books = new List<BookModel>();
            if (sorting != null)
            {
                stringBuilder.Append($"ORDER BY {sorting.SortBy} {sorting.SortOrder} ");
                getAll.Parameters.AddWithValue(sorting.SortBy, sorting.SortOrder);
            }
            else
            {
                stringBuilder.Append($"ORDER BY Id ");
            }

            if (pagination != null)
            {
                stringBuilder.Append("OFFSET @OffsetCount ROWS FETCH NEXT @PageSize ROWS ONLY");
                getAll.Parameters.AddWithValue("@OffsetCount", ((pagination.PageNumber - 1) * pagination.PageSize));
                getAll.Parameters.AddWithValue("@PageSize", pagination.PageSize);
            }
            getAll.CommandText = stringBuilder.ToString();
            getAll.Connection = connection;
            connection.Open();
            SqlDataReader allreader = await getAll.ExecuteReaderAsync();
            
            if (allreader.HasRows)
            {
                while (allreader.Read())
                {
                    BookModel book = new BookModel();
                    book.Id = allreader.GetGuid(0);
                    book.Title = allreader.GetString(1);
                    book.NumberOfPages = allreader.GetInt32(2);
                    book.Genre = allreader.IsDBNull(3) ? null : allreader.GetString(3);
                    book.AuthorId = allreader.GetGuid(4);
                    books.Add(book);
                }
                allreader.Close();
            }
           
            return books;
        }

        public async Task<BookModel> GetOneBookAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand getBook = new SqlCommand("SELECT * FROM Book WHERE @Id = id", connection);
                getBook.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader getReader = await getBook.ExecuteReaderAsync();
                BookModel findBook = new BookModel();

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
                    return findBook;
                }
                else
                {
                    return null;
                }

            }
        }
        public async Task<bool> PostOneBookAsync(BookModel newBook)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                newBook.Id = Guid.NewGuid();
                SqlCommand commandInsert = new SqlCommand("INSERT INTO Book VALUES (@Id, @Title, @NumberOfPages, @Genre, @Author_Id)", connection);
                commandInsert.Parameters.AddWithValue("@Id", newBook.Id);
                commandInsert.Parameters.AddWithValue("@Title", newBook.Title);
                commandInsert.Parameters.AddWithValue("@NumberOfPages", newBook.NumberOfPages);
                commandInsert.Parameters.AddWithValue("@Genre", newBook.Genre);
                commandInsert.Parameters.AddWithValue("@Author_Id", newBook.AuthorId);
                connection.Open();
                int numberOfRows = await commandInsert.ExecuteNonQueryAsync();
                connection.Close();

                if (numberOfRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public async Task<bool> DeleteBookAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {

                SqlCommand deleteCommand = new SqlCommand("DELETE FROM Book WHERE @Id = id", connection);
                deleteCommand.Parameters.AddWithValue("@Id", id);
                connection.Open();
                int numberOfRows = await deleteCommand.ExecuteNonQueryAsync();
                connection.Close();
                if (numberOfRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<bool> PutBookAsync(Guid id, BookModel updateBook)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            
            using (connection)
            {
                
                SqlCommand putCommand = new SqlCommand("UPDATE Book set title=@Title, [number of pages]=@NumberOfPages, genre=@Genre, author_id=@AuthorId where @Id = id", connection);

                putCommand.Parameters.AddWithValue("@Id", id);
                putCommand.Parameters.AddWithValue("@Title", updateBook.Title);
                putCommand.Parameters.AddWithValue("@NumberOfPages", updateBook.NumberOfPages);
                putCommand.Parameters.AddWithValue("@Genre", updateBook.Genre);
                putCommand.Parameters.AddWithValue("@AuthorId", updateBook.AuthorId);
                putCommand.Connection.Open();
                
                int numberOfAffectedRows = await putCommand.ExecuteNonQueryAsync();
               if (numberOfAffectedRows > 0)
                {
                    return true;
                }

                putCommand.Connection.Close();
                return false;

            }

        }
    }
}
