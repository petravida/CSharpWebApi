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

namespace BookConnecion.Repository 
{
    public class BookRepository : IBookRepositoryCommon
    {
        static string connectionString = "Data Source=LAPTOP-PT3M9TGC;Initial Catalog=Books;Integrated Security=True";
        
        public List<BookModel> GetBooks()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand getAll = new SqlCommand("SELECT * FROM Book", connection);
            List<BookModel> books = new List<BookModel>();
            connection.Open();
            SqlDataReader allreader = getAll.ExecuteReader();
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
        
        public BookModel GetOneBook(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand getBook = new SqlCommand("SELECT * FROM Book WHERE @Id = id", connection);
                getBook.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader getReader = getBook.ExecuteReader();
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
        public bool Post(BookModel newBook)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {

                SqlCommand commandInsert = new SqlCommand("INSERT INTO Book VALUES (@Id, @Title, @NumberOfPages, @Genre, @Author_Id)", connection);

                commandInsert.Parameters.AddWithValue("@Id", Guid.NewGuid());
                commandInsert.Parameters.AddWithValue("@Title", newBook.Title);
                commandInsert.Parameters.AddWithValue("@NumberOfPages", newBook.NumberOfPages);
                commandInsert.Parameters.AddWithValue("@Genre", newBook.Genre);
                commandInsert.Parameters.AddWithValue("@Author_Id", newBook.AuthorId);
                connection.Open();
                int numberOfRows = commandInsert.ExecuteNonQuery();
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
        public bool DeleteBook(Guid id)
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //public bool Put(Guid id, BookModel updateBook)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
           
        //    using (connection)
        //    {
        //        BookModel nowBook = new BookModel();
        //        nowBook.Id = id;
        //        updateBook.Id = id;
        //        SqlCommand putCommand = new SqlCommand("UPDATE Book set title=@Title, [number of pages]=@NumberOfPages, genre=@Genre, author_id=@AuthorId where @Id = id", connection);
        //        putCommand.Parameters.AddWithValue("@Id", id);
        //        putCommand.Parameters.AddWithValue("@Title", nowBook.Title == default ? updateBook.Title : nowBook.Title);
        //        putCommand.Parameters.AddWithValue("@NumberOfPages", nowBook.NumberOfPages == default ? updateBook.NumberOfPages : nowBook.NumberOfPages);
        //        putCommand.Parameters.AddWithValue("@Genre", nowBook.Genre = default ? updateBook.Genre : nowBook.Genre);
        //        putCommand.Parameters.AddWithValue("@AuthorId", nowBook.AuthorId = default ? updateBook.AuthorId : nowBook.AuthorId);
        //        connection.Open();
        //        int numberOfAffectedRows = putCommand.ExecuteNonQuery();
        //        connection.Close();
        //        if (numberOfAffectedRows > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        }  // }
}
