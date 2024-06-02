using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class timeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestDurationInMinutes",
                table: "assignedTests");

            migrationBuilder.AddColumn<int>(
                name: "TestDuration",
                table: "assignedTests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestDuration",
                table: "assignedTests");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TestDurationInMinutes",
                table: "assignedTests",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
