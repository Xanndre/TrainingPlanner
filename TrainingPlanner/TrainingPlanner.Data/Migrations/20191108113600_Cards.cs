using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlanner.Data.Migrations
{
    public partial class Cards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ValidityPeriod = table.Column<string>(nullable: true),
                    Entries = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    EntriesLeft = table.Column<string>(nullable: true),
                    ClubName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubCards_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubCards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainerCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrainerId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ValidityPeriod = table.Column<string>(nullable: true),
                    Entries = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    EntriesLeft = table.Column<string>(nullable: true),
                    TrainerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerCards_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerCards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubCards_ClubId",
                table: "ClubCards",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubCards_UserId",
                table: "ClubCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerCards_TrainerId",
                table: "TrainerCards",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerCards_UserId",
                table: "TrainerCards",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubCards");

            migrationBuilder.DropTable(
                name: "TrainerCards");
        }
    }
}
