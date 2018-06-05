using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.DataContracts
{
    public class MovieSearchResult
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTime { get; set; }

        public double AverageRating { get; set; }

        public static MovieSearchResult FromModel(Movie movie)
        {
            return new MovieSearchResult
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                RunningTime = movie.RunningTime,
                AverageRating = movie.AverageRating
            };
        }
    }
}
