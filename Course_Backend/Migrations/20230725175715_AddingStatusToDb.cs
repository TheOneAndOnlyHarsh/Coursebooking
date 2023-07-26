using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddingStatusToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CourseDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CourseDetails");
        }
    }
}
