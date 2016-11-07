using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barometr.Migrations
{
    public partial class updatetodouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Bars",
                nullable: false);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Bars",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Bars",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Bars",
                nullable: false);
        }
    }
}
