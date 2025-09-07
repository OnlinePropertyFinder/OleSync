using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OleSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_DocumentUrl_Column_In_Borad_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentUrl",
                table: "Boards",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentUrl",
                table: "Boards");
        }
    }
}
