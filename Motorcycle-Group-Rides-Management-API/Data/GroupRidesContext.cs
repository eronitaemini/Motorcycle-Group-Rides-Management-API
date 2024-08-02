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

//<<<<<<< HEAD
		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupRide> GroupRides { get; set; }
		public DbSet<Motorcycle> Motorcycles { get; set; }
		public DbSet<Models.Route> Routes { get; set; }
		public DbSet<User> Users { get; set; }
	//	public DbSet<UserGroupRide> UserGroupRides { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Compatibility> Compatibilities { get; set; }

//=======
       
    //    public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<IncidentReport> IncidentReports { get; set; }
   //     public DbSet<GroupRide> GroupRides { get; set; }
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

//>>>>>>> 1a3bb258d5330293170811db8d51acc71641a842

    }
}
