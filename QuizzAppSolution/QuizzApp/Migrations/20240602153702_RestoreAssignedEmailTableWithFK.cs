using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApp.Migrations
{
    public partial class RestoreAssignedEmailTableWithFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentNumber",
                table: "assignedTestEmails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_assignedTestEmails_AssignmentNumber",
                table: "assignedTestEmails",
                column: "AssignmentNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_assignedTestEmails_assignedTests_AssignmentNumber",
                table: "assignedTestEmails",
                column: "AssignmentNumber",
                principalTable: "assignedTests",
                principalColumn: "AssignmentNo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignedTestEmails_assignedTests_AssignmentNumber",
                table: "assignedTestEmails");

            migrationBuilder.DropIndex(
                name: "IX_assignedTestEmails_AssignmentNumber",
                table: "assignedTestEmails");

            migrationBuilder.DropColumn(
                name: "AssignmentNumber",
                table: "assignedTestEmails");
        }
    }
}
