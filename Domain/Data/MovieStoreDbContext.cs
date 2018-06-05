using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MovieStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenere { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }


        public MovieStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<MovieGenre>()
                .HasKey(s => new { s.MovieId, s.GenreId });

            builder.Entity<MovieRating>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.Rating);


            builder.Entity<MovieRating>()
               .HasOne(x => x.User)
               .WithMany(x => x.Ratings);

            base.OnModelCreating(builder);
        }
    }
}
