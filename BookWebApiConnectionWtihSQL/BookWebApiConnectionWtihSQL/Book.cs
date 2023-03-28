using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookWebApiConnectionWtihSQL
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }   
        public int NumberOfPages { get; set; }  
        public string Genre { get; set; }   
        public Guid AuthorId { get; set; }

        //4FB26061-DF13-475F-8763-3DB3763337C9
        //982C8FCA-C2D5-44F7-8711-F6071742FCEF
    }
}