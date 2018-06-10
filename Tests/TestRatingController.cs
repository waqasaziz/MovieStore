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
    public class TestRatingController
    {
        [Fact]
        public async Task Create_ShouldReturnBadRequest()
        {
            var controller = new RatingsController(Mock.Of<IMovieRepository>(),
                                                    Mock.Of<IUserRepository>(),
                                                    Mock.Of<IMovieRatingRepository>());

            controller.ModelState.AddModelError("Rating", "The Rating field must ben between 0 and 5.");

            var result = await controller.CreateAsync(1, new CreateMovieRatingModel()) as BadRequestObjectResult;

            Assert.NotNull(result);


        }

        [Fact]
        public async Task Create_ShouldReturnNotFound()
        {
            using (var context = InMemomryDbContext.Create())
            {
                var movieRepository = new MovieRepository(context);
                var userRepository = new UserRepository(context);
                var ratingRepository = new MovieRatingRepository(context);

                using (var controller = new RatingsController(movieRepository, userRepository, ratingRepository))
                {

                    var model = new CreateMovieRatingModel
                    {
                        UserName = "WaqasAziz",
                        Rating = 5
                    };
                    var result = await controller.CreateAsync(-1, model) as NotFoundObjectResult;

                    Assert.NotNull(result);

                }
            }
        }

        [Fact]
        public async Task Create_ShouldReturnOK()
        {
            using (var context = InMemomryDbContext.Create())
            {
                var movieRepository = new MovieRepository(context);
                var userRepository = new UserRepository(context);
                var ratingRepository = new MovieRatingRepository(context);

                using (var controller = new RatingsController(movieRepository, userRepository, ratingRepository))
                {
                    var model = new CreateMovieRatingModel
                    {
                        UserName = "WaqasAziz",
                        Rating = 3.5
                    };

                    var movieId = (await movieRepository.FirstAsyc()).Id;
                    var actionResult = await controller.CreateAsync(movieId, model) as OkObjectResult;
                    var rating = actionResult.Value as CreateMovieRatingResult;

                    Assert.NotNull(actionResult);
                    Assert.Equal(movieId, rating.MovieId);
                    Assert.Equal(model.Rating, rating.Rating);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest()
        {
            using (var context = InMemomryDbContext.Create())
            {
                var movieRepository = new MovieRepository(context);
                var userRepository = new UserRepository(context);
                var ratingRepository = new MovieRatingRepository(context);

                using (var controller = new RatingsController(movieRepository, userRepository, ratingRepository))
                {
                    controller.ModelState.AddModelError("Rating", "The Rating field must be between 0 and 5.");

                    var result = await controller.UpdateAsync(1, 1, new UpdateMovieRatingModel()) as BadRequestObjectResult;

                    Assert.NotNull(result);
                }
            }
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound()
        {
            using (var context = InMemomryDbContext.Create())
            {
                var movieRepository = new MovieRepository(context);
                var userRepository = new UserRepository(context);
                var ratingRepository = new MovieRatingRepository(context);

                using (var controller = new RatingsController(movieRepository, userRepository, ratingRepository))
                {
                    var model = new UpdateMovieRatingModel
                    {
                        UserName = "Waqas",
                        Rating = 5
                    };
                    var result = await controller.UpdateAsync(-1, -1, model) as NotFoundObjectResult;

                    Assert.NotNull(result);
                }
            }

        }

        [Fact]
        public async Task Update_ShouldReturnOK()
        {
            using (var context = InMemomryDbContext.Create())
            {
                var movieRepository = new MovieRepository(context);
                var userRepository = new UserRepository(context);
                var ratingRepository = new MovieRatingRepository(context);

                using (var controller = new RatingsController(movieRepository, userRepository, ratingRepository))
                {
                    var model = new UpdateMovieRatingModel
                    {
                        UserName = "WaqasAziz",
                        Rating = 3.5
                    };

                    var movieRating  = await ratingRepository.FirstAsyc();

                    var actionResult = await controller.UpdateAsync(movieRating.MovieId, movieRating.Id, model) as OkObjectResult;
                    var rating = actionResult.Value as UpdateMovieRatingResult;

                    Assert.NotNull(actionResult);
                    Assert.Equal(movieRating.MovieId, rating.MovieId);
                    Assert.Equal(movieRating.Id, rating.RatingId);
                    Assert.Equal(model.Rating, rating.Rating);
                }
            }
        }
    }
}
