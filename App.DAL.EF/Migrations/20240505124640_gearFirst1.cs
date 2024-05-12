using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class gearFirst1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenteeSickLeaves_SickLeaveTypes_SickLeaveTypeId",
                table: "MenteeSickLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_EmployeeMentorships_EmployeeMentorshipId",
                table: "Mentors");

            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_InternMentorships_InternMentorshipId",
                table: "Mentors");

            migrationBuilder.DropTable(
                name: "EmployeeMentorshipUntilDates");

            migrationBuilder.DropTable(
                name: "InternMentorshipUntilDates");

            migrationBuilder.DropTable(
                name: "SickLeaveTypes");

            migrationBuilder.DropIndex(
                name: "IX_Mentors_EmployeeMentorshipId",
                table: "Mentors");

            migrationBuilder.DropIndex(
                name: "IX_Mentors_InternMentorshipId",
                table: "Mentors");

            migrationBuilder.DropIndex(
                name: "IX_MenteeSickLeaves_SickLeaveTypeId",
                table: "MenteeSickLeaves");

            migrationBuilder.DropColumn(
                name: "ChangeReason",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "EmployeeMentorshipId",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "InternMentorshipId",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "UntilDate",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "SickLeaveTypeId",
                table: "MenteeSickLeaves");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "MenteeSickLeaves",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InternType",
                table: "Interns",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UntilDate",
                table: "InternMentorships",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeType",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UntilDate",
                table: "EmployeeMentorships",
                type: "date",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeesMentors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MentorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalHours = table.Column<int>(type: "integer", nullable: true),
                    IsCurrentlyActive = table.Column<bool>(type: "boolean", nullable: false),
                    ChangeReason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesMentors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeesMentors_EmployeeMentorships_EmployeeMentorshipId",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeesMentors_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternsMentors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MentorId = table.Column<Guid>(type: "uuid", nullable: true),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalHours = table.Column<int>(type: "integer", nullable: true),
                    IsCurrentlyActive = table.Column<bool>(type: "boolean", nullable: false),
                    ChangeReason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternsMentors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternsMentors_InternMentorships_InternMentorshipId",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternsMentors_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesMentors_EmployeeMentorshipId",
                table: "EmployeesMentors",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesMentors_MentorId",
                table: "EmployeesMentors",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternsMentors_InternMentorshipId",
                table: "InternsMentors",
                column: "InternMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternsMentors_MentorId",
                table: "InternsMentors",
                column: "MentorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesMentors");

            migrationBuilder.DropTable(
                name: "InternsMentors");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "MenteeSickLeaves");

            migrationBuilder.DropColumn(
                name: "UntilDate",
                table: "InternMentorships");

            migrationBuilder.DropColumn(
                name: "UntilDate",
                table: "EmployeeMentorships");

            migrationBuilder.AddColumn<string>(
                name: "ChangeReason",
                table: "Mentors",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeMentorshipId",
                table: "Mentors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FromDate",
                table: "Mentors",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InternMentorshipId",
                table: "Mentors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalHours",
                table: "Mentors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UntilDate",
                table: "Mentors",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SickLeaveTypeId",
                table: "MenteeSickLeaves",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InternType",
                table: "Interns",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeType",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeMentorshipUntilDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMentorshipUntilDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorshipUntilDates_EmployeeMentorships_EmployeeMe~",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternMentorshipUntilDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternMentorshipUntilDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternMentorshipUntilDates_InternMentorships_InternMentorsh~",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SickLeaveTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SickLeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_EmployeeMentorshipId",
                table: "Mentors",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_InternMentorshipId",
                table: "Mentors",
                column: "InternMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_MenteeSickLeaves_SickLeaveTypeId",
                table: "MenteeSickLeaves",
                column: "SickLeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMentorshipUntilDates_EmployeeMentorshipId",
                table: "EmployeeMentorshipUntilDates",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternMentorshipUntilDates_InternMentorshipId",
                table: "InternMentorshipUntilDates",
                column: "InternMentorshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenteeSickLeaves_SickLeaveTypes_SickLeaveTypeId",
                table: "MenteeSickLeaves",
                column: "SickLeaveTypeId",
                principalTable: "SickLeaveTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_EmployeeMentorships_EmployeeMentorshipId",
                table: "Mentors",
                column: "EmployeeMentorshipId",
                principalTable: "EmployeeMentorships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_InternMentorships_InternMentorshipId",
                table: "Mentors",
                column: "InternMentorshipId",
                principalTable: "InternMentorships",
                principalColumn: "Id");
        }
    }
}
