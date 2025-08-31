using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using OleSync.Infrastructure.Persistence.Context;

#nullable disable

namespace OleSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_CommitteeMembers_CommitteeMeetings_And_Update_Committee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename column Purpose -> Description if it exists (from earlier schema)
            // Note: EF migrations do not support conditional existence checks directly.
            // This assumes the initial Committees table used 'Purpose' column.
            migrationBuilder.RenameColumn(
                name: "Purpose",
                table: "Committees",
                newName: "Description");

            // Add new Committee columns
            migrationBuilder.AddColumn<bool>(
                name: "IsLinkedToBoard",
                table: "Committees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CommitteeType",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Committees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Committees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "DocumentUrl",
                table: "Committees",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuorumPercentage",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "VotingMethod",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "MakeDecisionsPercentage",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TieBreaker",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalVotingOption",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "VotingPeriodInMinutes",
                table: "Committees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Create CommitteeMembers
            migrationBuilder.CreateTable(
                name: "CommitteeMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommitteeId = table.Column<int>(type: "int", nullable: false),
                    MemberType = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    GuestId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitteeMembers_Committees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommitteeMembers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommitteeMembers_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeMembers_CommitteeId_EmployeeId_GuestId",
                table: "CommitteeMembers",
                columns: new[] { "CommitteeId", "EmployeeId", "GuestId" });

            // Create CommitteeMeetings
            migrationBuilder.CreateTable(
                name: "CommitteeMeetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MeetingType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CommitteeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitteeMeetings_Committees_CommitteeId",
                        column: x => x.CommitteeId,
                        principalTable: "Committees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeMeetings_CommitteeId",
                table: "CommitteeMeetings",
                column: "CommitteeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommitteeMeetings");

            migrationBuilder.DropTable(
                name: "CommitteeMembers");

            migrationBuilder.DropColumn(
                name: "AdditionalVotingOption",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "CommitteeType",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "DocumentUrl",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "IsLinkedToBoard",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "MakeDecisionsPercentage",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "QuorumPercentage",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "Status",
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

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Committees",
                newName: "Purpose");
        }
    }
}

