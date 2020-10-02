using System;
using NLog.Web;
using System.IO;
using System.Collections.Generic;

namespace MediaLibrary
{
    class Program
    {
        // creates static instance of logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            string movieFilePath = "movies.scrubbed.csv";

                                     MovieFile movieFile = new MovieFile(movieFilePath);

                                     string choice = "";

            do {
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display all movies");
                Console.WriteLine("Enter to quit");

                choice = Console.ReadLine();
               logger.Info("User choice: {choice}", choice);
                if(choice == "1"){
                    Movie movie = new Movie();
                    Console.WriteLine("Enter movie title");

                    movie.title = Console.ReadLine();

                    string input;

                    do {
                        Console.WriteLine("Enter genre (or done to quit)");

                        input = Console.ReadLine();

                        if(input != "done" && input.Length > 0)
                        {
                            movie.genres.Add(input);
                        }
                    } while (input != "done");
                        if (movie.genres.Count == 0){
                            movie.genres.Add("no genres listed");
                        }
                        Console.WriteLine("Enter Directors name");
                        movie.director = Console.ReadLine();

                        Console.WriteLine("Enter running time (h:m:s)");
                        movie.runningTime = TimeSpan.Parse(Console.ReadLine());
                        movieFile.AddMovie(movie);

                } else if (choice == "2"){
                    foreach(Movie m in movieFile.Movies){

                        Console.WriteLine(m.Display());
                    }
                }
            }  while (choice == "1" || choice == "2");



          //   {
           //      mediaId = 123,
           //     title = "Greatest Movie Ever, The (2020)",
           //     director = "Jeff Grissom",
           //     // timespan (hours, minutes, seconds)
           //     runningTime = new TimeSpan(2, 21, 23),
         //       genres = { "Comedy", "Romance" }
          //   };
             

            // Console.WriteLine(movie.Display());

            // Album album = new Album
            // {
            //     mediaId = 321,
            //     title = "Greatest Album Ever, The (2020)",
            //     artist = "Jeff's Awesome Band",
            //     recordLabel = "Universal Music Group",
            //     genres = { "Rock" }
            // };
            // Console.WriteLine(album.Display());

            // Book book = new Book
            // {
            //     mediaId = 111,
            //     title = "Super Cool Book",
            //     author = "Jeff Grissom",
            //     pageCount = 101,
            //     publisher = "",
            //     genres = { "Suspense", "Mystery" }
            // };
            // Console.WriteLine(book.Display());

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            logger.Info(scrubbedFile);

            

            logger.Info("Program ended");
        }
    }
}
