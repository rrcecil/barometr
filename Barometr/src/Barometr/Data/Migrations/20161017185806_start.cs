using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Barometr.Data.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DOB = table.Column<DateTime>(nullable: false),
                    Faction = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarDrinks",
                columns: table => new
                {
                    BarId = table.Column<int>(nullable: false),
                    DrinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarDrinks", x => new { x.BarId, x.DrinkId });
                });

            migrationBuilder.CreateTable(
                name: "Bars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BarDrinkBarId = table.Column<int>(nullable: true),
                    BarDrinkDrinkId = table.Column<int>(nullable: true),
                    HappyHour = table.Column<string>(nullable: true),
                    LatLong = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bars_BarDrinks_BarDrinkBarId_BarDrinkDrinkId",
                        columns: x => new { x.BarDrinkBarId, x.BarDrinkDrinkId },
                        principalTable: "BarDrinks",
                        principalColumns: new[] { "BarId", "DrinkId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BarDrinkBarId = table.Column<int>(nullable: true),
                    BarDrinkDrinkId = table.Column<int>(nullable: true),
                    Ingredient = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drinks_BarDrinks_BarDrinkBarId_BarDrinkDrinkId",
                        columns: x => new { x.BarDrinkBarId, x.BarDrinkDrinkId },
                        principalTable: "BarDrinks",
                        principalColumns: new[] { "BarId", "DrinkId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBars",
                columns: table => new
                {
                    BarId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBars", x => new { x.BarId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserBars_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BarId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    DrinkId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bars_BarDrinkBarId_BarDrinkDrinkId",
                table: "Bars",
                columns: new[] { "BarDrinkBarId", "BarDrinkDrinkId" });

            migrationBuilder.CreateIndex(
                name: "IX_BarDrinks_BarId",
                table: "BarDrinks",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_BarDrinks_DrinkId",
                table: "BarDrinks",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_BarDrinkBarId_BarDrinkDrinkId",
                table: "Drinks",
                columns: new[] { "BarDrinkBarId", "BarDrinkDrinkId" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BarId",
                table: "Reviews",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DrinkId",
                table: "Reviews",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBars_BarId",
                table: "UserBars",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBars_UserId",
                table: "UserBars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarDrinks_Bars_BarId",
                table: "BarDrinks",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarDrinks_Drinks_DrinkId",
                table: "BarDrinks",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bars_BarDrinks_BarDrinkBarId_BarDrinkDrinkId",
                table: "Bars");

            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_BarDrinks_BarDrinkBarId_BarDrinkDrinkId",
                table: "Drinks");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserBars");

            migrationBuilder.DropTable(
                name: "BarDrinks");

            migrationBuilder.DropTable(
                name: "Bars");

            migrationBuilder.DropTable(
                name: "Drinks");
        }
    }
}
