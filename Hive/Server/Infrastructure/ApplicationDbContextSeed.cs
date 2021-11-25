using Hive.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hive.Server.Infrastructure
{
    public static class ApplicationDbContextSeed
    {
        public static void SeedUserRoles (this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = UserRoles.SystemAdmin, NormalizedName = UserRoles.SystemAdmin.ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = UserRoles.ProjectOwner, NormalizedName = UserRoles.ProjectOwner.ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = UserRoles.Contributer, NormalizedName = UserRoles.Contributer.ToUpper() });
        }


        public static void SeedForecasts(this ModelBuilder builder)
        {
            string[] Summaries = new[]{ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering","Scorching"};
        
            var rng = new Random();

            var e = Enumerable.Range(1,5).Select(i => new WeatherForecast
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(i),
                TemperatureC = rng.Next(-20,55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            builder.Entity<WeatherForecast>().HasData(e);
        }
    }
}
