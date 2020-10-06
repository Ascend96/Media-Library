using System;
using System.Collections.Generic;
using System.IO;
using NLog.Web;
using System.Linq;

namespace MediaLibrary
{

    public class MovieFile {

        public string filePath { get; set; }

        public List<Movie> Movies { get; set; }

        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public MovieFile(string movieFilePath){
            filePath = movieFilePath;
            Movies = new List<Movie>();

            try {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while(!sr.EndOfStream){
                    Movie movie = new Movie();
                    string line = sr.ReadLine();

                    int idx = line.IndexOf('"');
                    if(idx == -1){
                        // splits file by comma and stores it into array
                        string[] movieDetails = line.Split(',');
                        // stores each property of movie to correct array index
                        movie.mediaId = UInt64.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                        movie.director = movieDetails[3];
                        movie.runningTime = TimeSpan.Parse(movieDetails[4]); 
                    }
                    else{
                        
                        movie.mediaId = UInt64.Parse(line.Substring(0, idx - 1));

                        line = line.Substring(idx + 1);

                        idx = line.IndexOf('"');

                        movie.title = line.Substring(0, idx);

                        line = line.Substring(idx + 2);

                        string [] details = line.Split(',');

                        movie.genres = details[0].Split('|').ToList();
                        

                     //   movie.director = details.Length > 1 ? details[1] : "unassigned";

                    //    movie.runningTime = details.Length > 2 ? TimeSpan.Parse(details[2]) : new TimeSpan(0);

                     movie.director = "unassigned";

                     //   movie.runningTime = new TimeSpan(2, 22, 22);
                    }
                    // adds movie from file into list
                    Movies.Add(movie);

                }
                sr.Close();
                logger.Info("Movies in file {count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddMovie(Movie movie){
            try{
                // creates id of movie based on highest id stores and adds 1
                movie.mediaId = Movies.Max(m => m.mediaId) + 1;
                // appends movie properties into file
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{movie.mediaId}, {movie.title},{string.Join('|', movie.genres)}, {movie.director}, {movie.runningTime}");
                sw.Close();

                Movies.Add(movie);

                logger.Info("Movie id {Id} added", movie.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            

        }
    }
}