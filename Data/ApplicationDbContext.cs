
using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace COMP2139_Labs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> projects { get; set; }
        public DbSet<ProjectTask>? tasks { get; set; }
        public DbSet<ProjectComment> comments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Project>().HasData(
                new Project
                {
                    ProjectId = 1,
                    Name = "Sample Assignment",
                    Description = "This Is Sample...",
                    StartDate = new DateTime(2024, 1, 8),
                    EndDate = new DateTime(2024, 9, 4),
                    Status = "Closed"
                },
                new Project
                {
                    ProjectId = 2,
                    Name = "Sample Assignment 2",
                    Description = "This Is Sample too...",
                    StartDate = new DateTime(2024, 2, 8),
                    EndDate = new DateTime(2024, 10, 4),
                    Status = "Closed"
                }
                );
            builder.Entity<ProjectTask>().HasData(
                new ProjectTask
                {
                    ProjectTaskId = 1,
                    Title = "Sample Task 1",
                    Description = "This Is Sample Task for Project...",
                    ProjectId = 1,
                },
                new ProjectTask
                {
                    ProjectTaskId = 2,
                    Title = "Sample Task 2",
                    Description = "This Is Sample Task for Project too...",
                    ProjectId = 1,

                });

            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<IdentityUserRole<string>>(entity => 
            
            { entity.ToTable(name: "UserRoles"); 
            
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable(name: "UserTokens");
            });

        }

    }
}
