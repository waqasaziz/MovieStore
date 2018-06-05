using Domain.Model;
using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public static class InMemomrySeedData
    {
        public static void Initialize(MovieStoreDbContext context)
        {
            var user = new User
            {
                UserName = "WaqasAziz",
                NormalisedUserName = "WAQASAZIZ",
                Email = "waqas_aziz@ymail.com",
            };

            var action = new Genre { Name = "Action", NormalisedName = "ACTION" };
            var mystery = new Genre { Name = "Mystery", NormalisedName = "MYSTERY" };
            var scifi = new Genre { Name = "SciFi", NormalisedName = "SCIFI" };
            var adventure = new Genre { Name = "Adventure", NormalisedName = "ADVENTURE" };


            var avengers = new Movie
            {
                Title = "Avengers: Infinity War",
                NormalisedTitle = "AVENGERS:_INFINITY_WAR",
                RunningTime = 149,
                YearOfRelease = 2018,
                Rating = new List<MovieRating> {
                     new MovieRating{ Rating=5.0, User = user},
                     new MovieRating{ Rating=5.0, User = user},
                     new MovieRating{ Rating=5.0, User = user},

                 },
                Genres = new List<MovieGenre> {
                    new MovieGenre{ Genre = action },
                    new MovieGenre{ Genre = scifi }
                }
            };

            var interstellar = new Movie
            {
                Title = "Interstellar",
                NormalisedTitle = "INTERSTELLAR",
                RunningTime = 149,
                YearOfRelease = 2018,
                Rating = new List<MovieRating> {
                     new MovieRating{ Rating=5.0, User = user},
                     new MovieRating{ Rating=5.0, User = user},
                     new MovieRating{ Rating=5.0, User = user},

                 },
                Genres = new List<MovieGenre> {
                    new MovieGenre{ Genre = scifi },
                }
            };

            var sherlockHolmes = new Movie
            {
                Title = "Sherlock Holmes",
                NormalisedTitle = "SHERLOCK_HOLMES",
                RunningTime = 149,
                YearOfRelease = 2018,
                Rating = new List<MovieRating> {
                     new MovieRating{ Rating=5.0, User = user},
                     new MovieRating{ Rating=5.0, User = user},
                     new MovieRating{ Rating=5.0, User = user},

                 },
                Genres = new List<MovieGenre> {
                    new MovieGenre{ Genre = mystery },
                }
            };

            var jumanji = new Movie
            {
                Title = "Jumanji: Welcome to the Jungle",
                NormalisedTitle = "JUMANJI:_WELCOME_TO_THE_JUNGLE",
                RunningTime = 149,
                YearOfRelease = 2018,
                Rating = new List<MovieRating> {
                     new MovieRating{ Rating=5.0, User = user},
                     new MovieRating{ Rating=4.0, User = user},
                     new MovieRating{ Rating=5.0, User = user},

                 },
                Genres = new List<MovieGenre> {
                    new MovieGenre{ Genre = adventure },
                    new MovieGenre{ Genre = action },
                }
            };

            var goneGirl = new Movie
            {
                Title = "Gone Girl",
                NormalisedTitle = "GONE_GIRL",
                RunningTime = 149,
                YearOfRelease = 2018,
                Rating = new List<MovieRating> {
                     new MovieRating{ Rating=3.2, User = user},
                     new MovieRating{ Rating=4.0, User = user},
                     new MovieRating{ Rating=3.5, User = user},

                 },
                Genres = new List<MovieGenre> {
                    new MovieGenre{ Genre = mystery },
                }
            };

            context.Movies.Add(avengers);

            context.Movies.Add(interstellar);

            context.Movies.Add(sherlockHolmes);

            context.Movies.Add(jumanji);

            context.Movies.Add(goneGirl);

            context.SaveChanges();
        }
    }
}
