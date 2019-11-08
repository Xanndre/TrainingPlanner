using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class UnlimitedEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ValidityPeriod",
                table: "TrainerPrices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Entries",
                table: "TrainerPrices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ValidityPeriod",
                table: "ClubPrices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Entries",
                table: "ClubPrices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValidityPeriod",
                table: "TrainerPrices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Entries",
                table: "TrainerPrices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ValidityPeriod",
                table: "ClubPrices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Entries",
                table: "ClubPrices",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
