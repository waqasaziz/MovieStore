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
        public int Id { get; set; }

        public string UserName { get; set; }

        public string NormalisedUserName { get; set; }

        public string Email { get; set; }

        public List<MovieRating> Ratings { get; set; }
    }
}
