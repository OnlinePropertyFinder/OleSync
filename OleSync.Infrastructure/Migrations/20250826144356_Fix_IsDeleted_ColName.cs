using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OleSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_IsDeleted_ColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Audit_IsDeleted",
                table: "Guests",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Audit_IsDeleted",
                table: "Employees",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Audit_IsDeleted",
                table: "Committees",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Audit_IsDeleted",
                table: "Boards",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Audit_IsDeleted",
                table: "BoardMembers",
                newName: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BoardMembers_EmployeeId",
                table: "BoardMembers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardMembers_GuestId",
                table: "BoardMembers",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardMembers_Employees_EmployeeId",
                table: "BoardMembers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardMembers_Guests_GuestId",
                table: "BoardMembers",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardMembers_Employees_EmployeeId",
                table: "BoardMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardMembers_Guests_GuestId",
                table: "BoardMembers");

            migrationBuilder.DropIndex(
                name: "IX_BoardMembers_EmployeeId",
                table: "BoardMembers");

            migrationBuilder.DropIndex(
                name: "IX_BoardMembers_GuestId",
                table: "BoardMembers");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Guests",
                newName: "Audit_IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Employees",
                newName: "Audit_IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Committees",
                newName: "Audit_IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Boards",
                newName: "Audit_IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "BoardMembers",
                newName: "Audit_IsDeleted");
        }
    }
}
