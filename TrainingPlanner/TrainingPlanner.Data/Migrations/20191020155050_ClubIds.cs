using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class ClubIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubActivities_Clubs_ClubId",
                table: "ClubActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubTrainers_Clubs_ClubId",
                table: "ClubTrainers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubWorkingHours_Clubs_ClubId",
                table: "ClubWorkingHours");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubWorkingHours",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubTrainers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubActivities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubActivities_Clubs_ClubId",
                table: "ClubActivities",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubTrainers_Clubs_ClubId",
                table: "ClubTrainers",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubWorkingHours_Clubs_ClubId",
                table: "ClubWorkingHours",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubActivities_Clubs_ClubId",
                table: "ClubActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubTrainers_Clubs_ClubId",
                table: "ClubTrainers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubWorkingHours_Clubs_ClubId",
                table: "ClubWorkingHours");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubWorkingHours",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubTrainers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "ClubActivities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ClubActivities_Clubs_ClubId",
                table: "ClubActivities",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubTrainers_Clubs_ClubId",
                table: "ClubTrainers",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubWorkingHours_Clubs_ClubId",
                table: "ClubWorkingHours",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
