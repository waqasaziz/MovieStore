using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.DataContracts;

namespace MovieStore.Controllers
{
    //[Authorize]
    [Route("api/MovieRatings")]
    public class MovieRatingController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRatingRepository _movieRatingRepository;

        public MovieRatingController(IMovieRepository movieRepository, IUserRepository userRepository, IMovieRatingRepository movieRatingRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _movieRatingRepository = movieRatingRepository;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody]CreateMovieRatingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var movie = await _movieRepository.FindByAsync(model.MovieId.Value);
            if (movie == null)
                return NotFound($"Coulnd't find any movie with id {model.MovieId}");

            var user = await _userRepository.FindByUserNameAsync(model.UserName);
            if (user == null)
                return NotFound($"Coulnd't find any user with user name: {model.UserName}");

            try
            {
                var movieRating = await _movieRatingRepository.CreateAsync(movie, user, model.Rating.Value);
                return Ok(new CreateMovieRatingResult
                {
                    MovieId = movieRating.MovieId,
                    RatingId = movieRating.Id,
                    Rating = movieRating.Rating
                });
            }
            catch (Exception)
            {
                //Log Exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut()]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateMovieRatingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var movieRating = await _movieRatingRepository.FindByAsync(model.RatingId.Value, model.UserName);
            if (movieRating == null)
                return NotFound($"Coulnd't find any movie rating with id {model.RatingId} and user name {model.UserName}");

            try
            {
                movieRating = await _movieRatingRepository.UpdateAsync(movieRating, model.Rating.Value);
                return Ok(new UpdateMovieRatingResult
                {
                    MovieId = movieRating.MovieId,
                    RatingId = movieRating.Id,
                    Rating = movieRating.Rating
                });
            }
            catch (Exception)
            {
                //Log Exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}