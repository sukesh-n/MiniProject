using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class RestoreAssignedEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedTestAssignmentNo",
                table: "assignedTestEmails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_assignedTestEmails_AssignedTestAssignmentNo",
                table: "assignedTestEmails",
                column: "AssignedTestAssignmentNo");

            migrationBuilder.AddForeignKey(
                name: "FK_assignedTestEmails_assignedTests_AssignedTestAssignmentNo",
                table: "assignedTestEmails",
                column: "AssignedTestAssignmentNo",
                principalTable: "assignedTests",
                principalColumn: "AssignmentNo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignedTestEmails_assignedTests_AssignedTestAssignmentNo",
                table: "assignedTestEmails");

            migrationBuilder.DropIndex(
                name: "IX_assignedTestEmails_AssignedTestAssignmentNo",
                table: "assignedTestEmails");

            migrationBuilder.DropColumn(
                name: "AssignedTestAssignmentNo",
                table: "assignedTestEmails");
        }
    }
}
