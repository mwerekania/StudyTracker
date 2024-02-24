using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyTracker.Migrations
{
    /// <inheritdoc />
    public partial class addAppUserIDToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Assignments");
        }
    }
}
