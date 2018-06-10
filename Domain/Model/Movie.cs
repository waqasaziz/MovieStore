using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]

        public string NormalisedTitle { get; set; }

        [Required]
        public int YearOfRelease { get; set; }

        [Required]
        public int RunningTime { get; set; }

        public List<MovieRating> Rating { get; set; }

        public List<MovieGenre> Genres { get; set; }

        public double AverageRating => Rating.Any() ? Math.Ceiling(Rating.Average(x => x.Rating)) : 0d;
    }
}
