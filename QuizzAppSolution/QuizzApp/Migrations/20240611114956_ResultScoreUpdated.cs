using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class ResultScoreUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignedQuestions");

            migrationBuilder.DropColumn(
                name: "score",
                table: "results");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "score",
                table: "results",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "assignedQuestions",
                columns: table => new
                {
                    AssignmentNumber = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_assignedQuestions_assignedTests_AssignmentNumber",
                        column: x => x.AssignmentNumber,
                        principalTable: "assignedTests",
                        principalColumn: "AssignmentNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignedQuestions_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignedQuestions_AssignmentNumber",
                table: "assignedQuestions",
                column: "AssignmentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_assignedQuestions_QuestionId",
                table: "assignedQuestions",
                column: "QuestionId");
        }
    }
}
