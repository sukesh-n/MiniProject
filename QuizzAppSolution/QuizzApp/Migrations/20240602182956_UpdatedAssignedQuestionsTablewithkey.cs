using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class UpdatedAssignedQuestionsTablewithkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_assignedQuestions_AssignmentNumber",
                table: "assignedQuestions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "assignedQuestions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_assignedQuestions",
                table: "assignedQuestions",
                column: "AssignmentNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_assignedQuestions",
                table: "assignedQuestions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "assignedQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_assignedQuestions_AssignmentNumber",
                table: "assignedQuestions",
                column: "AssignmentNumber");
        }
    }
}
