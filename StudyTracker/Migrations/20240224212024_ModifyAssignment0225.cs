using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyTracker.Migrations
{
    /// <inheritdoc />
    public partial class ModifyAssignment0225 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_UserId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_UserId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Assignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_UserId",
                table: "Assignments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_UserId",
                table: "Assignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
