using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class CardDeleteSetNull2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_AspNetUsers_UserId",
                table: "ClubCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCards_AspNetUsers_UserId",
                table: "TrainerCards");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubCards_AspNetUsers_UserId",
                table: "ClubCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCards_AspNetUsers_UserId",
                table: "TrainerCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_AspNetUsers_UserId",
                table: "ClubCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCards_AspNetUsers_UserId",
                table: "TrainerCards");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubCards_AspNetUsers_UserId",
                table: "ClubCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCards_AspNetUsers_UserId",
                table: "TrainerCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
