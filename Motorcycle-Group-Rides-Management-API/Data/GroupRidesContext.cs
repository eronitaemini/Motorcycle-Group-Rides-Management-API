using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Data
{
    public class GroupRidesContext : IdentityDbContext<IdentityUser>
    {
        public GroupRidesContext(DbContextOptions<GroupRidesContext> options) : base(options)
        {
        }

       
        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<IncidentReport> IncidentReports { get; set; }
        public DbSet<GroupRide> GroupRides { get; set; }
        public DbSet<UserGroupRide> UserGroupRides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserGroupRide>()
                .HasKey(ug => new { ug.UserId, ug.GroupRideId });

            modelBuilder.Entity<UserGroupRide>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroupRide)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserGroupRide>()
                .HasOne(ug => ug.GroupRide)
                .WithMany(gr => gr.UserGroupRides)
                .HasForeignKey(ug => ug.GroupRideId);
        }


    }
}
