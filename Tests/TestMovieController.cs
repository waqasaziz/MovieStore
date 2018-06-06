using Domain.Model;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieStore.Controllers;
using MovieStore.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class TestMovieController
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMovieRepository _movieReposioty;

        public TestMovieController()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieStore")
                .Options;

            _dbContext = new MovieStoreDbContext(options);

            InMemomrySeedData.Initialize(_dbContext);

            _movieReposioty = new MovieRepository(_dbContext);
        }

        [Fact]
        public async Task GetMovies_ShouldReturnBadRequest()
        {
            var controller = new MoviesController(_movieReposioty);

            var result = await controller.GetMoviesAsync() as BadRequestResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMovies_ShouldReturnNotFound()
        {

            var controller = new MoviesController(_movieReposioty);

            var result = await controller.GetMoviesAsync("RedSky") as NotFoundResult;

            Assert.NotNull(result);

        }


        [Fact]
        public async Task GetMovies_ShouldReturnSingle()
        {
            const string MovieTitle = "infinity";

            var controller = new MoviesController(_movieReposioty);

            var result = await controller.GetMoviesAsync(MovieTitle) as OkObjectResult;

            var colelction = result.Value as IEnumerable<MovieSearchResult>;

            Assert.NotNull(result);
            Assert.Single(colelction);
            Assert.Contains(MovieTitle, colelction.Single().Title, StringComparison.InvariantCultureIgnoreCase);

        }

        [Fact]
        public async Task GetMovies_ShouldReturnTwo()
        {
            const string genre = "Mystery";

            var controller = new MoviesController(_movieReposioty);

            var result = await controller.GetMoviesAsync(genre: genre) as OkObjectResult;

            var movies = result.Value as IEnumerable<MovieSearchResult>;

            Assert.NotNull(result);
            Assert.Equal(2, movies.Count());
            Assert.Equal("Gone Girl", movies.First().Title);
            Assert.Equal("Sherlock Holmes", movies.Last().Title);

        }


        [Fact]
        public async Task GetTopFiveMovies_ShouldReturnAll()
        {
            var controller = new MoviesController(_movieReposioty);

            var result = await controller.GetTopFiveMoviesAsyn() as OkObjectResult;

            var movies = result.Value as IEnumerable<MovieSearchResult>;

            Assert.NotNull(result);
            Assert.Equal(5, movies.Count());

            Assert.Equal("Avengers: Infinity War", movies.First().Title);
            Assert.Equal("Gone Girl", movies.Last().Title);
        }

    }
}
