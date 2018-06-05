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
    public class TestMovieRatingController
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMovieRepository _movieReposioty;
        private readonly IMovieRatingRepository _movieRatingRepsitory;
        private readonly IUserRepository _UserRepsitory;

        public TestMovieRatingController()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieStore")
                .Options;

            _dbContext = new MovieStoreDbContext(options);

            InMemomrySeedData.Initialize(_dbContext);

            _movieReposioty = new MovieRepository(_dbContext);
            _movieRatingRepsitory = new MovieRatingRepository(_dbContext);
            _UserRepsitory = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest()
        {
            var controller = new MovieRatingController(_movieReposioty, _UserRepsitory, _movieRatingRepsitory);

            controller.ModelState.AddModelError("MovieId", "The MovieId field is required.");

            var result = await controller.CreateAsync(new CreateMovieRatingModel()) as BadRequestObjectResult;

            Assert.NotNull(result);

        }

        [Fact]
        public async Task Create_ShouldReturnNotFound()
        {
            var controller = new MovieRatingController(_movieReposioty, _UserRepsitory, _movieRatingRepsitory);

            // Mocks could be used here, Moq framework
            var model = new CreateMovieRatingModel
            {
                MovieId = -1,
                UserName = "WaqasAziz",
                Rating = 5
            };
            var result = await controller.CreateAsync(model) as NotFoundObjectResult;

            Assert.NotNull(result);

        }

        [Fact]
        public async Task Create_ShouldReturnOK()
        {
            var controller = new MovieRatingController(_movieReposioty, _UserRepsitory, _movieRatingRepsitory);

            // Mocks could be used here, Moq framework
            var model = new CreateMovieRatingModel
            {
                MovieId = 5,
                UserName = "WaqasAziz",
                Rating = 3.5
            };

            var actionResult = await controller.CreateAsync(model) as OkObjectResult;

            var rating = actionResult.Value as CreateMovieRatingResult;

            Assert.NotNull(actionResult);
            Assert.Equal(model.MovieId, rating.MovieId);
            Assert.Equal(model.Rating, rating.Rating);


        }


        [Fact]
        public async Task Update_ShouldReturnBadRequest()
        {
            var controller = new MovieRatingController(_movieReposioty, _UserRepsitory, _movieRatingRepsitory);

            controller.ModelState.AddModelError("RatingId", "The RatingId field is required.");

            var result = await controller.UpdateAsync(new UpdateMovieRatingModel()) as BadRequestObjectResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound()
        {
            var controller = new MovieRatingController(_movieReposioty, _UserRepsitory, _movieRatingRepsitory);

            // Mocks could be used here, Moq framework
            var model = new UpdateMovieRatingModel
            {
                RatingId = -1,
                UserName = "WaqasAziz",
                Rating = 5
            };
            var result = await controller.UpdateAsync(model) as NotFoundObjectResult;

            Assert.NotNull(result);

        }

        [Fact]
        public async Task Update_ShouldReturnOK()
        {
            var controller = new MovieRatingController(_movieReposioty, _UserRepsitory, _movieRatingRepsitory);

            // Mocks could be used here, Moq framework
            var model = new UpdateMovieRatingModel
            {
                RatingId = 1,
                UserName = "WaqasAziz",
                Rating = 3.5
            };
            var actionResult = await controller.UpdateAsync(model) as OkObjectResult;

            var rating = actionResult.Value as UpdateMovieRatingResult;

            Assert.NotNull(actionResult);
            Assert.Equal(model.RatingId, rating.RatingId);
            Assert.Equal(model.Rating, rating.Rating);


        }
    }
}
