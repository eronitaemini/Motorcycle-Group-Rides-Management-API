﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motorcycle_Group_Rides_Management_API.Data;

#nullable disable

namespace MotorcycleGroupRidesManagementAPI.Migrations
{
    [DbContext(typeof(GroupRidesContext))]
    partial class GroupRidesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            //modelBuilder.Entity("GroupRideUser", b =>
            //    {
            //        b.Property<Guid>("GroupRidesGroupRideId")
            //            .HasColumnType("char(36)");

            //        b.Property<Guid>("ParticipantsId")
            //            .HasColumnType("char(36)");

            //        b.HasKey("GroupRidesGroupRideId", "ParticipantsId");

            //        b.HasIndex("ParticipantsId");

            //        b.ToTable("GroupRideUser");
            //    });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.Group", b =>
                {
                    b.Property<Guid>("GroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("GroupID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.GroupRide", b =>
                {
                    b.Property<Guid>("GroupRideId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("GroupID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RouteID")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("GroupRideId");

                    b.HasIndex("GroupID");

                    b.HasIndex("RouteID")
                        .IsUnique();

                    b.ToTable("GroupRides");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.Motorcycle", b =>
                {
                    b.Property<int>("MotorcycleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EngineSize")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("char(36)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("MotorcycleID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Motorcycles");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.Routes", b =>
                {
                    b.Property<Guid>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("EndingPoint")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("EstimatedTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("StartingPoint")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("routeType")
                        .HasColumnType("int");

                    b.HasKey("RouteId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.UserGroupRide", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GroupRideID")
                        .HasColumnType("char(36)");

                    b.HasKey("UserID", "GroupRideID");

                    b.HasIndex("GroupRideID");

                    b.ToTable("UserGroupRide");
                });

            //modelBuilder.Entity("GroupRideUser", b =>
            //    {
            //        b.HasOne("Motorcycle_Group_Rides_Management_API.Models.GroupRide", null)
            //            .WithMany()
            //            .HasForeignKey("GroupRidesGroupRideId")
            //            .OnDelete(DeleteBehavior.Cascade)
            //            .IsRequired();

            //        b.HasOne("Motorcycle_Group_Rides_Management_API.Models.User", null)
            //            .WithMany()
            //            .HasForeignKey("ParticipantsId")
            //            .OnDelete(DeleteBehavior.Cascade)
            //            .IsRequired();
            //    });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.GroupRide", b =>
                {
                    b.HasOne("Motorcycle_Group_Rides_Management_API.Models.Group", "Group")
                        .WithMany("GroupRides")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Motorcycle_Group_Rides_Management_API.Models.Routes", "Route")
                        .WithOne("groupRide")
                        .HasForeignKey("Motorcycle_Group_Rides_Management_API.Models.GroupRide", "RouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.Motorcycle", b =>
                {
                    b.HasOne("Motorcycle_Group_Rides_Management_API.Models.User", "Owner")
                        .WithMany("Motorcycles")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.UserGroupRide", b =>
                {
                    b.HasOne("Motorcycle_Group_Rides_Management_API.Models.GroupRide", "GroupRide")
                        .WithMany()
                        .HasForeignKey("GroupRideID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Motorcycle_Group_Rides_Management_API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupRide");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.Group", b =>
                {
                    b.Navigation("GroupRides");
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.Routes", b =>
                {
                    b.Navigation("groupRide")
                        .IsRequired();
                });

            modelBuilder.Entity("Motorcycle_Group_Rides_Management_API.Models.User", b =>
                {
                    b.Navigation("Motorcycles");
                });
#pragma warning restore 612, 618
        }
    }
}
