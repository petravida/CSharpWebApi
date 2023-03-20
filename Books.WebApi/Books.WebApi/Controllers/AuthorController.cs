using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Xml.Linq;

namespace Books.WebApi.Controllers
{
    public class AuthorController : ApiController

    {
        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public int WrittenBooks { get; set; }

       static List<Author> authors = new List<Author>
        {
            new Author {Id = 1, FirstName = "Dorothy", LastName = "Koomson", WrittenBooks = 6},
            new Author {Id = 2, FirstName = "Liane", LastName = "Moriarty", WrittenBooks = 10},
            new Author {Id = 3, FirstName = "Stephen", LastName = "King", WrittenBooks = 29}
        };

        public IEnumerable<Author> GetAllAuthors()
        {
            return authors;
        }
        public Author GetAuthor(int id)
        {
            return authors.Where(a => a.Id == id).FirstOrDefault();
        }
  
        public List<Author> Post([FromBody] Author ath)
        {
            authors.Add(ath);
            return authors;

        }
        public List<Author> Put(int id)
        {
            Author authorToUpdate = authors.Find(a => a.Id == id);
            authorToUpdate.WrittenBooks = 78;
            return authors;
            
        }
        public List<Author> Delete(int id)
        {
            authors.Remove(authors.Where(a => a.Id == id).FirstOrDefault());
            return authors;
        }



    }
}
