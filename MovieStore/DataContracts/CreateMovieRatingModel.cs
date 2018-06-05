using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.DataContracts
{
    public class CreateMovieRatingModel
    {
        [Required]
        public int? MovieId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Range(0, 5.0)]
        public double? Rating { get; set; }
    }
}
