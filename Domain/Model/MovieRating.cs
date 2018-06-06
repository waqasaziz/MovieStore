
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class MovieRating
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
