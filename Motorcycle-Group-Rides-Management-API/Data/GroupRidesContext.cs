using System;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Data
{
	public class GroupRidesContext:DbContext
	{
		public GroupRidesContext(DbContextOptions<GroupRidesContext> options):base(options)
		{
		}

		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupRide> GroupRides { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<User> Users { get; set; }
		public DbSet<UserGroupRide> UserGroupRides { get; set; }
		public DbSet<Routes> Routes { get; set; }

        //because im changing the relationship to the grpuride to have many routes,
        //and a route to be associated only with one groupride
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between GroupRide and Routes
            modelBuilder.Entity<GroupRide>()
                .HasMany(gr => gr.Routes)
                .WithOne(r => r.GroupRide)
                .HasForeignKey(r => r.GroupRideId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}

