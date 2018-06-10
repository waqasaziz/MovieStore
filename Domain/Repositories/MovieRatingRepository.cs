using Domain.Model;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMovieRatingRepository
    {
        Task<MovieRating> FindByAsync(int id, string userName);

        Task<MovieRating> CreateAsync(Movie movie, User user, double rating);

        Task<MovieRating> UpdateAsync(MovieRating movieRating, double rating);

        Task<MovieRating> FirstAsyc();
    }

    public class MovieRatingRepository : IMovieRatingRepository
    {
        private MovieStoreDbContext _dbContext;

        public MovieRatingRepository(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MovieRating> FindByAsync(int id, string userName)
        {
            return await _dbContext.MovieRatings
                            .SingleOrDefaultAsync(x => x.Id == id
                                               && x.User.NormalisedUserName == userName.ToUpper());
        }

        public async Task<MovieRating> CreateAsync(Movie movie, User user, double rating)
        {
            var movieRating = new MovieRating
            {
                Movie = movie,
                Rating = rating,
                User = user,
            };

            _dbContext.MovieRatings.Add(movieRating);

            await _dbContext.SaveChangesAsync();

            return movieRating;
        }

        public async Task<MovieRating> UpdateAsync(MovieRating movieRating, double rating)
        {
            movieRating.Rating = rating;

            _dbContext.Update(movieRating);

            await _dbContext.SaveChangesAsync();

            return movieRating;
        }

        public Task<MovieRating> FirstAsyc() => _dbContext.MovieRatings.FirstAsync();
    }
}
