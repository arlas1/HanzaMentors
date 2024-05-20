using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class gearFirst4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "InternMentorshipDocuments");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "EmployeeMentorshipDocuments");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "InternMentorshipDocuments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "EmployeeMentorshipDocuments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "InternMentorshipDocuments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "EmployeeMentorshipDocuments");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverId",
                table: "InternMentorshipDocuments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverId",
                table: "EmployeeMentorshipDocuments",
                type: "uuid",
                nullable: true);
        }
    }
}
