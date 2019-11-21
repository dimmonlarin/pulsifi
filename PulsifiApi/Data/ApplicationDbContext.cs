using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PulsifiApp.Models;

namespace PulsifiApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seeding default roles
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole() { Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole() { Name = "Recruiter", NormalizedName = "Recruiter".ToUpper() },
                new IdentityRole() { Name = "Candidate", NormalizedName = "Candidate".ToUpper() }
                );

            // seeding locations
            modelBuilder.Entity<Location>()
                .HasData(
                new Location() { ID = 1, Description = "Everywhere" },
                new Location() { ID = 2, Description = "Kuala Lumpur" },
                new Location() { ID = 3, Description = "Ipoh" });

            // setting conversion for the JobStatus property
            modelBuilder
                .Entity<Job>()
                .Property(e => e.Status)
                .HasConversion(new EnumToNumberConverter<JobStatus, int>());
        }
    }
}
