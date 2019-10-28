using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class AddDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CloseHour",
                table: "ClubWorkingHours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenHour",
                table: "ClubWorkingHours",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseHour",
                table: "ClubWorkingHours");

            migrationBuilder.DropColumn(
                name: "OpenHour",
                table: "ClubWorkingHours");
        }
    }
}
