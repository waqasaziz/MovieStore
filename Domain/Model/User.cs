using Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }


        [Required]
        [MaxLength(200)]
        public string NormalisedUserName { get; set; }


        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        public List<MovieRating> Ratings { get; set; }
    }
}
