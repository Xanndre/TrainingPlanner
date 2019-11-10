using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class CardDeleteSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_Clubs_ClubId",
                table: "ClubCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCards_Trainers_TrainerId",
                table: "TrainerCards");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubCards_Clubs_ClubId",
                table: "ClubCards",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCards_Trainers_TrainerId",
                table: "TrainerCards",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_Clubs_ClubId",
                table: "ClubCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCards_Trainers_TrainerId",
                table: "TrainerCards");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubCards_Clubs_ClubId",
                table: "ClubCards",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCards_Trainers_TrainerId",
                table: "TrainerCards",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
