using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class ChangeUserIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubRatings_AspNetUsers_UserId1",
                table: "ClubRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerRatings_AspNetUsers_UserId1",
                table: "TrainerRatings");

            migrationBuilder.DropIndex(
                name: "IX_TrainerRatings_UserId1",
                table: "TrainerRatings");

            migrationBuilder.DropIndex(
                name: "IX_ClubRatings_UserId1",
                table: "ClubRatings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TrainerRatings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ClubRatings");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TrainerRatings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ClubRatings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRatings_UserId",
                table: "TrainerRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRatings_UserId",
                table: "ClubRatings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubRatings_AspNetUsers_UserId",
                table: "ClubRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerRatings_AspNetUsers_UserId",
                table: "TrainerRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubRatings_AspNetUsers_UserId",
                table: "ClubRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerRatings_AspNetUsers_UserId",
                table: "TrainerRatings");

            migrationBuilder.DropIndex(
                name: "IX_TrainerRatings_UserId",
                table: "TrainerRatings");

            migrationBuilder.DropIndex(
                name: "IX_ClubRatings_UserId",
                table: "ClubRatings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TrainerRatings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TrainerRatings",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ClubRatings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ClubRatings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRatings_UserId1",
                table: "TrainerRatings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClubRatings_UserId1",
                table: "ClubRatings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubRatings_AspNetUsers_UserId1",
                table: "ClubRatings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerRatings_AspNetUsers_UserId1",
                table: "TrainerRatings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
