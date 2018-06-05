using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.DataContracts
{
    public class CreateMovieRatingResult
    {
        public int MovieId { get; set; }

        public double RatingId { get; set; }

        public double Rating { get; set; }
    }
}
