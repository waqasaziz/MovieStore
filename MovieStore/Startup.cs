using System;
using System.Text;
using Domain;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace MovieStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieRatingRepository, MovieRatingRepository>();

            services.AddMvc();

            services.AddDbContext<MovieStoreDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration["DefalutConnectionString"]);
            });

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Info
                {
                    Title = "Movie Store API",
                    Version = "v1"
                });
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Store API V1");
            });

            app.UseMvc();

        }
    }
}
