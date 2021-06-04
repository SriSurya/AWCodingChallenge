using Movie.API.Data.Interfaces;
using Movie.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Movie.API.Data
{
    public class MovieContext : IMovieContext
    {

        public MovieContext()
        {
            Movies = GetMovies(); 

           
        }

        public IEnumerable<MovieModel> Movies { get; }

        public IEnumerable<MovieModel> GetMovies()
        {
            return new List<MovieModel>()
            {
                new MovieModel()
                {
                    ID = 1,
                    Title = "Raiders of the Lost Ark",
                    ReleaseDate  = new DateTime(1981,05,01),
                    Rating = 8.4,
                    Franchise = "Indiana Jones"

                },
                  new MovieModel()
                {
                    ID = 2,
                    Title = "Indiana Jones and the Temple of Doom",
                     ReleaseDate  = new DateTime(1984,06,21),
                    Rating = 7.6,
                    Franchise = "Indiana Jones"

                },
                    new MovieModel()
                {
                    ID = 3,
                    Title = "Indiana Jones and the Last Crusade",
                     ReleaseDate  = new DateTime(1989,03,22),
                    Rating = 8.3,
                    Franchise = "Indiana Jones"

                },
           new MovieModel()
                {
                    ID = 4,
                    Title = "The Terminator",
                     ReleaseDate  = new DateTime(1984,01,24),
                    Rating = 8.1,
                    Franchise = "Terminator"

                }


            };
        }

     
    }


    

}
