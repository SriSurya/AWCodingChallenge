using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Movie.API.Entities;

namespace Movie.API.Repositories.Interfaces
{
    public interface IMovieRepository
    {

        Task<IEnumerable<MovieModel>> GetAllMovies();
        Task<IEnumerable<MovieModel>> GetMoviesByTitle(string title);

        Task<IEnumerable<MovieModel>> GetMoviesByFranchise(string title);

        Task<IEnumerable<MovieModel>> GetMoviesByReleaseDates(string releaseStartDate, string releaseEndDate );

        Task<IEnumerable<MovieModel>> GetMoviesByRating(double rating, string operatorSymobl);



      

        
    }
}
