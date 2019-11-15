using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class BodyMeasurements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    MuscleMass = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    MetabolicAge = table.Column<int>(nullable: false),
                    Fat = table.Column<int>(nullable: false),
                    FatMass = table.Column<int>(nullable: false),
                    IsInjured = table.Column<bool>(nullable: false),
                    Neck = table.Column<int>(nullable: true),
                    Forearm = table.Column<int>(nullable: true),
                    Chest = table.Column<int>(nullable: true),
                    Waist = table.Column<int>(nullable: true),
                    Thigh = table.Column<int>(nullable: true),
                    Shoulders = table.Column<int>(nullable: true),
                    Biceps = table.Column<int>(nullable: true),
                    Hips = table.Column<int>(nullable: true),
                    Calf = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_UserId",
                table: "BodyMeasurements",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyMeasurements");
        }
    }
}
