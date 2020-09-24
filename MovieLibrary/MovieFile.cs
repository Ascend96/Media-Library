using System;

namespace MovieLibrary
{
    public class MovieFile
    {
        // public property
        public string filePath { get; set; }

        // constructor is a special method that is invoked
        // when an instance of a class is created
        public MovieFile(string movieFilePath)
        {
            filePath = movieFilePath;
        }
    }
}