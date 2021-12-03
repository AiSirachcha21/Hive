using Hive.Server.Domain;
using Hive.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hive.Server.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<WeatherForecast> Forecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<OrganizationUser>()
                .HasOne(u => u.Member)
                .WithMany(u => u.OrganizationUsers)
                .HasForeignKey(u => u.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrganizationUser>()
                .HasOne(x => x.Organization)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProjectUser>()
                .HasOne(x => x.Member)
                .WithMany(x => x.ProjectUsers)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProjectUser>()
                .HasOne(x => x.Project)
                .WithMany(x => x.ProjectUsers)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            //TODO: There is a issue where the Project Table is creating a ProjectOwnerId and ProjectOwnerId1 column in the DB. This needs to be fixed before proceeding

            base.OnModelCreating(builder);

            builder.SeedUserRoles();
            builder.SeedForecasts();
        }
    }
}
