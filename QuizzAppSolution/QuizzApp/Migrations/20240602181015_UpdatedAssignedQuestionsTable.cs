using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class UpdatedAssignedQuestionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "assignedQuestions",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentNumber",
                table: "assignedQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_assignedQuestions_AssignmentNumber",
                table: "assignedQuestions",
                column: "AssignmentNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_assignedQuestions_assignedTests_AssignmentNumber",
                table: "assignedQuestions",
                column: "AssignmentNumber",
                principalTable: "assignedTests",
                principalColumn: "AssignmentNo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignedQuestions_assignedTests_AssignmentNumber",
                table: "assignedQuestions");

            migrationBuilder.DropIndex(
                name: "IX_assignedQuestions_AssignmentNumber",
                table: "assignedQuestions");

            migrationBuilder.DropColumn(
                name: "AssignmentNumber",
                table: "assignedQuestions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "assignedQuestions",
                newName: "TestId");
        }
    }
}
