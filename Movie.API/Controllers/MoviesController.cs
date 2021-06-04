using Microsoft.AspNetCore.Mvc;
using Movie.API.Entities;
using Movie.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movie.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;
        public MoviesController(IMovieRepository repository)
        {
            _repository = repository;
        
        }

        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MovieModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MovieModel>> GetAllMovies()
        {
            var movie = await _repository.GetAllMovies();

            if (!movie.Any())
            {
                //log the error
                return NotFound();
            }

            return Ok(movie);
        }

        /// <summary>
        /// Get Movies By Title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("{title}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MovieModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MovieModel>> GetMoviesByTitle(string title)
        {
            var movie = await _repository.GetMoviesByTitle(title);

            if (!movie.Any())
            {
                //log the error
                return NotFound();
            }

            return Ok(movie);
        }

        /// <summary>
        /// Get Movies By Franchise title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet("franchises/{title}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MovieModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MovieModel>> GetMoviesByFranchise(string title)
        {
            var movie = await _repository.GetMoviesByFranchise(title);

            if (!movie.Any())
            {
                //log the error
                return NotFound();
            }

            return Ok(movie);
        }

        /// <summary>
        /// Get Movies By ReleaseDates
        /// </summary>
        /// <param name="releaseStartDate"></param>
        /// <param name="releaseEndDate"></param>
        /// <returns></returns>
        [HttpGet("releases")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MovieModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MovieModel>> GetMoviesByReleaseDates(string releaseStartDate, string releaseEndDate )
        {
            if (string.IsNullOrEmpty(releaseStartDate) || string.IsNullOrEmpty(releaseEndDate))
            {
                return BadRequest("Enter valid dates in MM/dd/YYYY");
            }

            var movie = await _repository.GetMoviesByReleaseDates(releaseStartDate, releaseEndDate);

            if (!movie.Any())
            {
                //log the error
                return NotFound();
            }

            return Ok(movie);
        }

        /// <summary>
        /// Get Movies By Franchise
        /// </summary>
        /// <param name="rating"></param>
        /// <param name="operatorSymbol"></param>
        /// <returns></returns>

        [HttpGet("ratings/{rating}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MovieModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MovieModel>> GetMoviesByFranchise(double rating, string operatorSymbol = "gte")
        {

            if (operatorSymbol != "gte" && operatorSymbol != "lte")
            {
                return BadRequest("Enter valid operator either gte or lte");
            }

            var movie = await _repository.GetMoviesByRating(rating,operatorSymbol);

            if (!movie.Any())
            {
                //log the error
                return NotFound();
            }

            return Ok(movie);
        }
    }
}
