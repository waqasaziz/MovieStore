using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.DataContracts;

namespace MovieStore.Controllers
{
    [Route("api/[Controller]")]
    public class MoviesController : Controller
    {
        private IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet()]
        [Route("Search")]
        public async Task<IActionResult> GetMoviesAsync(string title = null, int? yearOfRelease = null, string genre = null)
        {

            if (string.IsNullOrEmpty(title) && yearOfRelease == null && string.IsNullOrEmpty(genre))
                return BadRequest();

            var genres = genre?.Split(',') ?? Enumerable.Empty<string>();

            var result = (await _movieRepository.GetListAsync(title, yearOfRelease, genres)).Select(MovieSearchResult.FromModel);

            if (!result.Any())
                return NotFound();

            return Ok(result);
        }


        [HttpGet("TopFive")]
        public async Task<IActionResult> GetTopFiveMoviesAsyn()
        {
            var result = (await _movieRepository.GetTopFiveAsync()).Select(MovieSearchResult.FromModel);

            if (!result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{username}/TopFive")]
        public async Task<IActionResult> GetTopFiveMoviesAsyn(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest();

            var result = (await _movieRepository.GetTopFiveByAsync(username))
                                         .Select(MovieSearchResult.FromModel);

            if (!result.Any())
                return NotFound();

            return Ok(result);
        }
    }
}