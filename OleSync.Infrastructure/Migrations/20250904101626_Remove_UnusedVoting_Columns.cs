using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OleSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_UnusedVoting_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalVotingOption",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "MakeDecisionsPercentage",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "TieBreaker",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "VotingMethod",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "VotingPeriodInMinutes",
                table: "Committees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalVotingOption",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MakeDecisionsPercentage",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TieBreaker",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VotingMethod",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VotingPeriodInMinutes",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
