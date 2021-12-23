using Hive.Domain;
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

            builder.Entity<Organization>().HasIndex(o => o.Name).IsUnique();

            builder.Entity<OrganizationUser>()
                .HasOne(u => u.Member)
                .WithMany(u => u.OrganizationUsers)
                .HasForeignKey(u => u.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrganizationUser>()
                .HasOne(x => x.Organization)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectUser>()
                .HasOne(x => x.Member)
                .WithMany(x => x.ProjectUsers)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectUser>()
                .HasOne(x => x.Project)
                .WithMany(x => x.ProjectUsers)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);

            builder.SeedUserRoles();
            builder.SeedForecasts();
        }
    }
}
