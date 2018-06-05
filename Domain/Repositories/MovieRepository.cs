using Domain.Model;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> FindByAsync(int id);
        Task<List<Movie>> GetListAsync(string tite, int? yearOfRelease, IEnumerable<string> genre);
        Task<List<Movie>> GetTopFiveAsync();
        Task<List<Movie>> GetTopFiveByAsync(string userName);
    }

    public class MovieRepository : IMovieRepository
    {
        private MovieStoreDbContext _dbContext;

        public MovieRepository(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> FindByAsync(int id) => await _dbContext.Movies.FindAsync(id);

        public async Task<List<Movie>> GetListAsync(string tite, int? yearOfRelease, IEnumerable<string> genre)
        {
            tite = tite?.ToUpper() ?? "-1";
            genre = genre?.Select(x => x.ToUpper()) ?? Enumerable.Empty<string>();

            var query = _dbContext.Movies
                                  .Include(x => x.Genres)
                                  .Include(x => x.Rating)
                                  .Where(x => x.NormalisedTitle.Contains(tite)
                                  || x.YearOfRelease == yearOfRelease
                                  || x.Genres.Any(g => genre.Contains(g.Genre.NormalisedName)));

            return await query.OrderBy(x => x.Title).ToListAsync();
        }

        public async Task<List<Movie>> GetTopFiveAsync()
        {
            var query = _dbContext.Movies.Include(x => x.Rating).Include(x => x.Genres)
                        .OrderByDescending(x => x.AverageRating)
                        .ThenBy(x => x.Title)
                        .Take(5);

            return await query.ToListAsync();
        }

        public async Task<List<Movie>> GetTopFiveByAsync(string userName)
        {
            var query = _dbContext.Movies
                        .Where(x => x.Rating.Any(r => r.User.NormalisedUserName.Contains(userName.ToUpper())))
                        .OrderByDescending(x => x.Rating.Sum(r => r.Rating))
                        .ThenBy(x => x.Title)
                        .Take(5)
                        .Include(x => x.Genres);

            return await query.ToListAsync();
        }
    }
}
