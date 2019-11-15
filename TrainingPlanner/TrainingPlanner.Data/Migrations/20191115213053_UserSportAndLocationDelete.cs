using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class UserSportAndLocationDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_AspNetUsers_UserId",
                table: "UserLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSports_AspNetUsers_UserId",
                table: "UserSports");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_AspNetUsers_UserId",
                table: "UserLocations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSports_AspNetUsers_UserId",
                table: "UserSports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_AspNetUsers_UserId",
                table: "UserLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSports_AspNetUsers_UserId",
                table: "UserSports");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_AspNetUsers_UserId",
                table: "UserLocations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSports_AspNetUsers_UserId",
                table: "UserSports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
