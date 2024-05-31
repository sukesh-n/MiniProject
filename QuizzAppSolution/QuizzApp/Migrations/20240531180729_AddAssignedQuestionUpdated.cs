using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class AddAssignedQuestionUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "assignedQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_assignedQuestions_QuestionId",
                table: "assignedQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_assignedQuestions_questions_QuestionId",
                table: "assignedQuestions",
                column: "QuestionId",
                principalTable: "questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignedQuestions_questions_QuestionId",
                table: "assignedQuestions");

            migrationBuilder.DropIndex(
                name: "IX_assignedQuestions_QuestionId",
                table: "assignedQuestions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "assignedQuestions");
        }
    }
}
