using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class RelationUpdateDataSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_solutions_OptionId",
                table: "solutions",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_solutions_QuestionId",
                table: "solutions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_security_UserId",
                table: "security",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_results_TestId",
                table: "results",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_options_QuestionId",
                table: "options",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_options_questions_QuestionId",
                table: "options",
                column: "QuestionId",
                principalTable: "questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_results_tests_TestId",
                table: "results",
                column: "TestId",
                principalTable: "tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_security_users_UserId",
                table: "security",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_solutions_options_OptionId",
                table: "solutions",
                column: "OptionId",
                principalTable: "options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_solutions_questions_QuestionId",
                table: "solutions",
                column: "QuestionId",
                principalTable: "questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_options_questions_QuestionId",
                table: "options");

            migrationBuilder.DropForeignKey(
                name: "FK_results_tests_TestId",
                table: "results");

            migrationBuilder.DropForeignKey(
                name: "FK_security_users_UserId",
                table: "security");

            migrationBuilder.DropForeignKey(
                name: "FK_solutions_options_OptionId",
                table: "solutions");

            migrationBuilder.DropForeignKey(
                name: "FK_solutions_questions_QuestionId",
                table: "solutions");

            migrationBuilder.DropIndex(
                name: "IX_solutions_OptionId",
                table: "solutions");

            migrationBuilder.DropIndex(
                name: "IX_solutions_QuestionId",
                table: "solutions");

            migrationBuilder.DropIndex(
                name: "IX_security_UserId",
                table: "security");

            migrationBuilder.DropIndex(
                name: "IX_results_TestId",
                table: "results");

            migrationBuilder.DropIndex(
                name: "IX_options_QuestionId",
                table: "options");
        }
    }
}
