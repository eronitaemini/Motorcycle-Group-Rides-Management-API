using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Data
{
	public class GroupRidesContext: IdentityDbContext<IdentityUser>
    {
		public GroupRidesContext(DbContextOptions<GroupRidesContext> options):base(options)
		{
		}

		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupRide> GroupRides { get; set; }
		public DbSet<Motorcycle> Motorcycles { get; set; }
		public DbSet<Models.Route> Routes { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserGroupRide> UserGroupRides { get; set; }

	}
}

