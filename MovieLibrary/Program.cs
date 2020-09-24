using System;
using NLog.Web;
using System.IO;
using System.Collections.Generic;

namespace MovieLibrary
{
    class Program
    {
                // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

                        Movie movie = new Movie
            {
                movieId = 1,
                title = "Jeff's Killer Movie (2019)",
                genres = new List<string> { "Action", "Romance", "Comedy" }
            };

            logger.Info("Program ended");
        }
    }
}
