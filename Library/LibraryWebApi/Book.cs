﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApi
{
    public class Book
    {
        public int Id { get; set; } 
        public string Title { get; set; }   
        public int NumberOfPages { get; set; }
        public string Genre { get; set; }   
    }
}