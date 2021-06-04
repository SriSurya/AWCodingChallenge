
using Microsoft.Extensions.Configuration;
using Movie.API.Data.Interfaces;
using Movie.API.Entities;
using Movie.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        //private readonly IConfiguration _Configuration;
        //private readonly ILibraryService _libraryService;


        //private  string MoviesFilePath { get { return _Configuration.GetSection("FilePaths")["LibraryPath"]; } }
        //private  string FranchiseFilePath { get { return _Configuration.GetSection("FilePaths")["FranchiseListPath"]; } }

        private readonly IMovieContext _context;
        public MovieRepository(IMovieContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        
          public async Task<IEnumerable<MovieModel>> GetAllMovies()
        {
            var movies = _context.Movies.GroupBy(x => x.Title).Select(x => x.FirstOrDefault());

            return await Task.FromResult(movies);

        }
     

        public async Task<IEnumerable<MovieModel>> GetMoviesByTitle(string title)
        {
            var movies = _context.Movies.Where(s => s.Title.ToLower().Contains(title.ToLower())).GroupBy(x => x.Title).Select(x => x.FirstOrDefault());

            return await Task.FromResult(movies);

        }

        public async Task<IEnumerable<MovieModel>> GetMoviesByFranchise(string title)
        {
            var movies = _context.Movies.Where(s => s.Franchise.ToLower().Contains(title.ToLower())).GroupBy(x => x.Title).Select(x => x.FirstOrDefault());

            return await Task.FromResult(movies);

        }

        

        public async Task<IEnumerable<MovieModel>> GetMoviesByReleaseDates(string releaseStartDate, string releaseEndDate)
        {
            DateTime? startDate = !string.IsNullOrEmpty(releaseStartDate) ? Convert.ToDateTime(releaseStartDate) : null;
            DateTime? endDate = !string.IsNullOrEmpty(releaseEndDate) ? Convert.ToDateTime(releaseEndDate) : null;

            var movies = _context.Movies.Where(s => s.ReleaseDate >= startDate && s.ReleaseDate <= endDate).GroupBy(x => x.Title).Select(x => x.FirstOrDefault());

            // var movies = _context.Movies.Where(s => s.Franchise.ToLower().Contains(releaseStartDate.ToLower())).GroupBy(x => x.Title).Select(x => x.FirstOrDefault());

            return await Task.FromResult(movies);

        }

        public async Task<IEnumerable<MovieModel>> GetMoviesByRating(double rating, string operatorSymobl)
        {

            var movies = _context.Movies.Where(m => operatorSymobl == "gte" ? m.Rating >= rating : m.Rating <= rating).ToList();

            return await Task.FromResult(movies);

        }
       



    }
}
