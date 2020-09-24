  
using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class Movie
    {
        public UInt64 movieId { get; set; }
        public string title { get; set; }
        public List<string> genres { get; set; }
    }
}