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
            // sets file path for scrubbed file
            string movieFilePath = "movies.scrubbed.csv";
                                        // creates movie file instance
                                     MovieFile movieFile = new MovieFile(movieFilePath);

                                     string choice = "";

            do {
                // 1 adds movie, 2 displays movie, entering nothing exits program
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display all movies");
                Console.WriteLine("Enter to quit");
                // takes user input
                choice = Console.ReadLine();
                // logs users choice
               logger.Info("User choice: {choice}", choice);

                if(choice == "1"){
                    // creates Movie object
                    Movie movie = new Movie();
                    // asks for title
                    Console.WriteLine("Enter movie title");
                    // stores what user entered into movie.title
                    movie.title = Console.ReadLine();
                    
                    string input;

                    do {
                        // asks for genres or enter done to quit
                        Console.WriteLine("Enter genre (or done to quit)");
                        // stores what user entered into variable
                        input = Console.ReadLine();
                        // if not done and the user entered something, add it to genre list
                        if(input != "done" && input.Length > 0)
                        {
                            movie.genres.Add(input);
                        }
                        // if user didnt enter done, but left it blank, stores no genres listed into list
                    } while (input != "done");
                        if (movie.genres.Count == 0){
                            movie.genres.Add("no genres listed");
                        }  
                        // asks for directors name
                        Console.WriteLine("Enter Directors name");
                        // stores it into movie.director
                        movie.director = Console.ReadLine();
                        // asks for running time of movie
                        Console.WriteLine("Enter running time (h:m:s)");
                        // parses it into timespan
                        movie.runningTime = TimeSpan.Parse(Console.ReadLine());
                        // adds it to the movie file
                        movieFile.AddMovie(movie);

                } else if (choice == "2"){
                    // foreach loop displays each movie in file
                    foreach(Movie m in movieFile.Movies){
                        // calls display method
                        Console.WriteLine(m.Display());
                    }
                }   
                // condition for menu
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
