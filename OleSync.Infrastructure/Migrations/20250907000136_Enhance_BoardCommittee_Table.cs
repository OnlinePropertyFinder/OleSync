using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OleSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Enhance_BoardCommittee_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommitteeId",
                table: "Boards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_CommitteeId",
                table: "Boards",
                column: "CommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Committees_CommitteeId",
                table: "Boards",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Committees_CommitteeId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_CommitteeId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CommitteeId",
                table: "Boards");
        }
    }
}
