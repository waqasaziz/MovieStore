using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class Movie
    {
        public Movie()
        {
            Genres = new List<MovieGenre>();
            Rating = new List<MovieRating>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string NormalisedTitle { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTime { get; set; }

        public List<MovieRating> Rating { get; set; }

        public double AverageRating => Math.Ceiling(Rating.Average(x => x.Rating));

        public List<MovieGenre> Genres { get; set; }
    }
}
