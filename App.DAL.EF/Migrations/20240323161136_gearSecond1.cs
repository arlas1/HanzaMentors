using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class gearSecond1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChoosenSigningTime",
                table: "InternMentorshipDocuments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WayOfSigning",
                table: "InternMentorshipDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChoosenSigningTime",
                table: "EmployeeMentorshipDocuments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WayOfSigning",
                table: "EmployeeMentorshipDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChoosenSigningTime",
                table: "InternMentorshipDocuments");

            migrationBuilder.DropColumn(
                name: "WayOfSigning",
                table: "InternMentorshipDocuments");

            migrationBuilder.DropColumn(
                name: "ChoosenSigningTime",
                table: "EmployeeMentorshipDocuments");

            migrationBuilder.DropColumn(
                name: "WayOfSigning",
                table: "EmployeeMentorshipDocuments");
        }
    }
}
