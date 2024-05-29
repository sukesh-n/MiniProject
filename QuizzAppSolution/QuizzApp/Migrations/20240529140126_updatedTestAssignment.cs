using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class updatedTestAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_tests_TestId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_questions_tests_TestId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_TestId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_categories_TestId",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "categories");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentNo",
                table: "tests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusOfTest",
                table: "tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestType",
                table: "tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "assignedQuestions",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "assignedTests",
                columns: table => new
                {
                    AssignmentNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedBy = table.Column<int>(type: "int", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTimeWindow = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTimeWindow = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestDurationInMinutes = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedTests", x => x.AssignmentNo);
                });

            migrationBuilder.CreateTable(
                name: "assignedTestEmails",
                columns: table => new
                {
                    AssignmentNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsOrganizer = table.Column<bool>(type: "bit", nullable: false),
                    IsCandidate = table.Column<bool>(type: "bit", nullable: false),
                    AssignedTestAssignmentNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedTestEmails", x => x.AssignmentNumber);
                    table.ForeignKey(
                        name: "FK_assignedTestEmails_assignedTests_AssignedTestAssignmentNo",
                        column: x => x.AssignedTestAssignmentNo,
                        principalTable: "assignedTests",
                        principalColumn: "AssignmentNo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignedTestEmails_AssignedTestAssignmentNo",
                table: "assignedTestEmails",
                column: "AssignedTestAssignmentNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignedQuestions");

            migrationBuilder.DropTable(
                name: "assignedTestEmails");

            migrationBuilder.DropTable(
                name: "assignedTests");

            migrationBuilder.DropColumn(
                name: "AssignmentNo",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "StatusOfTest",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "TestType",
                table: "tests");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_questions_TestId",
                table: "questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_TestId",
                table: "categories",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_tests_TestId",
                table: "categories",
                column: "TestId",
                principalTable: "tests",
                principalColumn: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_tests_TestId",
                table: "questions",
                column: "TestId",
                principalTable: "tests",
                principalColumn: "TestId");
        }
    }
}
