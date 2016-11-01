using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barometr.Migrations
{
    public partial class removedatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OpenTime",
                table: "BusinessHours",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CloseTime",
                table: "BusinessHours",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenTime",
                table: "BusinessHours",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseTime",
                table: "BusinessHours",
                nullable: false);
        }
    }
}
