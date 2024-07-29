using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleGroupRidesManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddGroupRide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupRides_Groups_GroupID",
                table: "GroupRides");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupRides_Routes_RouteID",
                table: "GroupRides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupRide_GroupRides_GroupRideId",
                table: "UserGroupRide");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupRide_Users_UserId",
                table: "UserGroupRide");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_GroupRides_GroupID",
                table: "GroupRides");

            migrationBuilder.DropIndex(
                name: "IX_GroupRides_RouteID",
                table: "GroupRides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroupRide",
                table: "UserGroupRide");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "GroupRides");

            migrationBuilder.DropColumn(
                name: "RouteID",
                table: "GroupRides");

            migrationBuilder.RenameTable(
                name: "UserGroupRide",
                newName: "UserGroupRides");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupRide_GroupRideId",
                table: "UserGroupRides",
                newName: "IX_UserGroupRides_GroupRideId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GroupRides",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroupRides",
                table: "UserGroupRides",
                columns: new[] { "UserId", "GroupRideId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupRides_GroupRides_GroupRideId",
                table: "UserGroupRides",
                column: "GroupRideId",
                principalTable: "GroupRides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupRides_Users_UserId",
                table: "UserGroupRides",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupRides_GroupRides_GroupRideId",
                table: "UserGroupRides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupRides_Users_UserId",
                table: "UserGroupRides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroupRides",
                table: "UserGroupRides");

            migrationBuilder.RenameTable(
                name: "UserGroupRides",
                newName: "UserGroupRide");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupRides_GroupRideId",
                table: "UserGroupRide",
                newName: "IX_UserGroupRide_GroupRideId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GroupRides",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupID",
                table: "GroupRides",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "RouteID",
                table: "GroupRides",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroupRide",
                table: "UserGroupRide",
                columns: new[] { "UserId", "GroupRideId" });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Distance = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndPoint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstimatedTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoogleMapsRouteData = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SafetyTips = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartPoint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.RouteID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRides_GroupID",
                table: "GroupRides",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRides_RouteID",
                table: "GroupRides",
                column: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupRides_Groups_GroupID",
                table: "GroupRides",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupRides_Routes_RouteID",
                table: "GroupRides",
                column: "RouteID",
                principalTable: "Routes",
                principalColumn: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupRide_GroupRides_GroupRideId",
                table: "UserGroupRide",
                column: "GroupRideId",
                principalTable: "GroupRides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupRide_Users_UserId",
                table: "UserGroupRide",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
