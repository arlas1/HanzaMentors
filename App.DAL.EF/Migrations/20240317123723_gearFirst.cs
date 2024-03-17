using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class gearFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PersonalCode = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Base64Code = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactorySupervisors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorySupervisors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternSupervisors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternSupervisors", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmployeeType = table.Column<int>(type: "integer", nullable: false),
                    Profession = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Interns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    InternType = table.Column<int>(type: "integer", nullable: false),
                    StudyProgram = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interns_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMentorships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FactorySupervisorId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalHours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMentorships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorships_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorships_FactorySupervisors_FactorySupervisorId",
                        column: x => x.FactorySupervisorId,
                        principalTable: "FactorySupervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternMentorships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternSupervisorId = table.Column<Guid>(type: "uuid", nullable: false),
                    FactorySupervisorId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalHours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternMentorships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternMentorships_FactorySupervisors_FactorySupervisorId",
                        column: x => x.FactorySupervisorId,
                        principalTable: "FactorySupervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternMentorships_InternSupervisors_InternSupervisorId",
                        column: x => x.InternSupervisorId,
                        principalTable: "InternSupervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternMentorships_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMentorshipDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    Base64Code = table.Column<byte[]>(type: "bytea", nullable: true),
                    DocumentStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMentorshipDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorshipDocuments_DocumentSamples_DocumentSampleId",
                        column: x => x.DocumentSampleId,
                        principalTable: "DocumentSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorshipDocuments_EmployeeMentorships_EmployeeMen~",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMentorshipUntilDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMentorshipUntilDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorshipUntilDates_EmployeeMentorships_EmployeeMe~",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternMentorshipDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentSampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    Base64Code = table.Column<byte[]>(type: "bytea", nullable: true),
                    DocumentStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternMentorshipDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternMentorshipDocuments_DocumentSamples_DocumentSampleId",
                        column: x => x.DocumentSampleId,
                        principalTable: "DocumentSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternMentorshipDocuments_InternMentorships_InternMentorshi~",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternMentorshipUntilDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternMentorshipUntilDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternMentorshipUntilDates_InternMentorships_InternMentorsh~",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenteeSickLeaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    SickLeaveTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenteeSickLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenteeSickLeaves_EmployeeMentorships_EmployeeMentorshipId",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenteeSickLeaves_InternMentorships_InternMentorshipId",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenteeSickLeaves_SickLeaveTypes_SickLeaveTypeId",
                        column: x => x.SickLeaveTypeId,
                        principalTable: "SickLeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentAmount = table.Column<string>(type: "text", nullable: true),
                    PaymentOrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ChangeReason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mentors_EmployeeMentorships_EmployeeMentorshipId",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mentors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mentors_InternMentorships_InternMentorshipId",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMentorshipDocuments_DocumentSampleId",
                table: "EmployeeMentorshipDocuments",
                column: "DocumentSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMentorshipDocuments_EmployeeMentorshipId",
                table: "EmployeeMentorshipDocuments",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMentorships_EmployeeId",
                table: "EmployeeMentorships",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMentorships_FactorySupervisorId",
                table: "EmployeeMentorships",
                column: "FactorySupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMentorshipUntilDates_EmployeeMentorshipId",
                table: "EmployeeMentorshipUntilDates",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternMentorshipDocuments_DocumentSampleId",
                table: "InternMentorshipDocuments",
                column: "DocumentSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_InternMentorshipDocuments_InternMentorshipId",
                table: "InternMentorshipDocuments",
                column: "InternMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternMentorships_FactorySupervisorId",
                table: "InternMentorships",
                column: "FactorySupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternMentorships_InternId",
                table: "InternMentorships",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_InternMentorships_InternSupervisorId",
                table: "InternMentorships",
                column: "InternSupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternMentorshipUntilDates_InternMentorshipId",
                table: "InternMentorshipUntilDates",
                column: "InternMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Interns_AppUserId",
                table: "Interns",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenteeSickLeaves_EmployeeMentorshipId",
                table: "MenteeSickLeaves",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_MenteeSickLeaves_InternMentorshipId",
                table: "MenteeSickLeaves",
                column: "InternMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_MenteeSickLeaves_SickLeaveTypeId",
                table: "MenteeSickLeaves",
                column: "SickLeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_EmployeeId",
                table: "Mentors",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_EmployeeMentorshipId",
                table: "Mentors",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_InternMentorshipId",
                table: "Mentors",
                column: "InternMentorshipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmployeeMentorshipDocuments");

            migrationBuilder.DropTable(
                name: "EmployeeMentorshipUntilDates");

            migrationBuilder.DropTable(
                name: "InternMentorshipDocuments");

            migrationBuilder.DropTable(
                name: "InternMentorshipUntilDates");

            migrationBuilder.DropTable(
                name: "MenteeSickLeaves");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DocumentSamples");

            migrationBuilder.DropTable(
                name: "SickLeaveTypes");

            migrationBuilder.DropTable(
                name: "EmployeeMentorships");

            migrationBuilder.DropTable(
                name: "InternMentorships");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "FactorySupervisors");

            migrationBuilder.DropTable(
                name: "InternSupervisors");

            migrationBuilder.DropTable(
                name: "Interns");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
