using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using CodingChallenge.Utilities;

namespace CodingChallenge.DataAccess
{
    public class LibraryService : ILibraryService
    {

        public string MoviesFilePath { get; set; }
        public string FranchiseFilePath { get; set; }

        public LibraryService() {
            MoviesFilePath = ConfigurationManager.AppSettings["LibraryPath"];
            FranchiseFilePath = ConfigurationManager.AppSettings["FranchiseListPath"];
        }

 
        private IEnumerable<Movie> GetMovies()
        {
            var movies = _movies ?? (_movies = MoviesFilePath.FromFileInExecutingDirectory().DeserializeFromXml<Library>().Movies);

           // Request #2: Show movies by franchise
            if (movies != null)
            {
                var franchiseMovieList = GetFranchises();
                movies.ToList().ForEach(m => {
                    var franchisename = franchiseMovieList?.Where(f => f.MovieTitles.Contains(m.Title))?.FirstOrDefault()?.Name;
                    m.Franchise = !string.IsNullOrWhiteSpace(franchisename) ? franchisename : "";
                });
                
            }
            return movies;
        }


        /// <summary>
        /// Get Franchise List from text file
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Franchise> GetFranchises()
        {
            List<Franchise> franchiseList = new List<Franchise>();

            

            var franchises = Regex.Split(FranchiseFilePath.FromFileInExecutingDirectory(), "\n\n\n");

            foreach (var objFranchise in franchises)
            {
                var franchiseArray = Regex.Split(objFranchise, "\n------------------------------\n");
                var franchise = new Franchise
                {
                    Name = franchiseArray[0],
                    MovieTitles = Regex.Split(franchiseArray[1], "\n").ToList()
                };
                franchiseList.Add(franchise);
            }

            return franchiseList;
        }
        private IEnumerable<Movie> _movies { get; set; }

        public int SearchMoviesCount(string title)
        {
            return SearchMovies(title).Count();
        }

        public IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = null, SortDirection sortDirection = SortDirection.Ascending)
        {
            var movies = GetMovies();

            //Request #1: Filter movies by title
            //Bug #2: Remove duplicate movies
            
            if (movies != null )
            {
                movies = !string.IsNullOrEmpty(title)
              ? movies.Where(s => s.Title.ToLower().Contains(title.ToLower())).GroupBy(x => x.Title).Select(x => x.FirstOrDefault()) : movies.GroupBy(x => x.Title).Select(x => x.FirstOrDefault());

            }

            if (skip.HasValue && take.HasValue)
            {
               
                movies = movies.Skip(skip.Value).Take(take.Value);
            }
            //ignore  leading articles (a/an/the) in the titles
            string ignoreLeadingPrefix= "^(a?\\s)|^(an?\\s)|^(the?\\s)";

            //if sortcolumn is not null
            //Bug #1: Movies are not sortable
            if (!string.IsNullOrEmpty(sortColumn))
             {
                var propertyInfo = typeof(Movie).GetProperty(sortColumn);
                if (sortDirection == SortDirection.Ascending)
                {
                   movies = sortColumn.ToLower() == "title" ? 
                        movies.OrderBy(m => Regex.Replace(m.Title, ignoreLeadingPrefix, "", RegexOptions.IgnoreCase)) :
                        movies.OrderBy(x => propertyInfo.GetValue(x, null));

                }
                else
                {
                    movies = sortColumn.ToLower() == "title" ?
                                   movies.OrderByDescending(m => Regex.Replace(m.Title, ignoreLeadingPrefix, "", RegexOptions.IgnoreCase)) :
                                  movies.OrderByDescending(x => propertyInfo.GetValue(x, null));
                 
                }
             }

            return movies.ToList();
        }

    
    }
}
