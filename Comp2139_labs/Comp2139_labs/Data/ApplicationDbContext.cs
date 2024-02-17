using System;
using Comp2139_labs.Models;
using Microsoft.EntityFrameworkCore;
namespace Comp2139_labs
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTask> ProjectTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().HasData(

                new Project { ProjectId = 1, Name = "Assignment 1", Description ="Comp 2139 Assignment 1" },
                new Project { ProjectId = 2, Name = "Assignment 2", Description = "Comp 2139 Assignment 2" }

                );
        }

    }
}

