using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class AddClubTrainerUserToCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_Clubs_ClubId",
                table: "ClubCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCards_Trainers_TrainerId",
                table: "TrainerCards");

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "TrainerCards",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubCards",
                nullable: true,
                oldClrType: typeof(int));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubCards_Clubs_ClubId",
                table: "ClubCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerCards_Trainers_TrainerId",
                table: "TrainerCards");

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "TrainerCards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubCards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubCards_Clubs_ClubId",
                table: "ClubCards",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerCards_Trainers_TrainerId",
                table: "TrainerCards",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
