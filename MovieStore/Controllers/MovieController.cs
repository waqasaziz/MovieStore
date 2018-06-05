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
    //[Authorize]
    [Route("api/Movies")]
    public class MovieController : Controller
    {
        private IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet()]
        [Route("{title?}/{yearofrelease?}/{genre?}")]
        public async Task<IActionResult> GetMoviesAsync(string title = null, int? yearofrelease = null, string genre = null)
        {

            if (string.IsNullOrEmpty(title) && yearofrelease == null && string.IsNullOrEmpty(genre))
                return BadRequest();

            var genres = genre?.Split(',') ?? Enumerable.Empty<string>();

            var result = (await _movieRepository.GetListAsync(title, yearofrelease, genres)).Select(MovieSearchResult.FromModel);

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