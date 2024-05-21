using System;
using Base.Domain;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class i18n : Migration
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
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    PersonalCode = table.Column<long>(type: "bigint", nullable: true),
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
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternSupervisors", x => x.Id);
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
                    FirstName = table.Column<LangStr>(type: "jsonb", maxLength: 128, nullable: true),
                    LastName = table.Column<LangStr>(type: "jsonb", maxLength: 128, nullable: true),
                    EmployeeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Profession = table.Column<LangStr>(type: "jsonb", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
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
                    FirstName = table.Column<LangStr>(type: "jsonb", maxLength: 128, nullable: true),
                    LastName = table.Column<LangStr>(type: "jsonb", maxLength: 128, nullable: true),
                    InternType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    StudyProgram = table.Column<LangStr>(type: "jsonb", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
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
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ExpirationDT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PreviousRefreshToken = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    PreviousExpirationDT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMentorships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    FactorySupervisorId = table.Column<Guid>(type: "uuid", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalHours = table.Column<int>(type: "integer", nullable: true),
                    IsCurrentlyActive = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentlyOnSickLeave = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMentorships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorships_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeMentorships_FactorySupervisors_FactorySupervisorId",
                        column: x => x.FactorySupervisorId,
                        principalTable: "FactorySupervisors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    PaymentAmount = table.Column<string>(type: "text", nullable: true),
                    PaymentOrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Profession = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mentors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternMentorships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternId = table.Column<Guid>(type: "uuid", nullable: true),
                    InternSupervisorId = table.Column<Guid>(type: "uuid", nullable: true),
                    FactorySupervisorId = table.Column<Guid>(type: "uuid", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalHours = table.Column<int>(type: "integer", nullable: true),
                    IsCurrentlyActive = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentlyOnSickLeave = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternMentorships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternMentorships_FactorySupervisors_FactorySupervisorId",
                        column: x => x.FactorySupervisorId,
                        principalTable: "FactorySupervisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternMentorships_InternSupervisors_InternSupervisorId",
                        column: x => x.InternSupervisorId,
                        principalTable: "InternSupervisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternMentorships_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMentorshipDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentSampleId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Base64Code = table.Column<byte[]>(type: "bytea", nullable: true),
                    DocumentStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChoosenSigningTime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    WayOfSigning = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMentorshipDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMentorshipDocuments_DocumentSamples_DocumentSampleId",
                        column: x => x.DocumentSampleId,
                        principalTable: "DocumentSamples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeMentorshipDocuments_EmployeeMentorships_EmployeeMen~",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id");
                });

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
                name: "InternMentorshipDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentSampleId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Base64Code = table.Column<byte[]>(type: "bytea", nullable: true),
                    DocumentStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChoosenSigningTime = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    WayOfSigning = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternMentorshipDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternMentorshipDocuments_DocumentSamples_DocumentSampleId",
                        column: x => x.DocumentSampleId,
                        principalTable: "DocumentSamples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternMentorshipDocuments_InternMentorships_InternMentorshi~",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
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

            migrationBuilder.CreateTable(
                name: "MenteeSickLeaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InternMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmployeeMentorshipId = table.Column<Guid>(type: "uuid", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UntilDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenteeSickLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenteeSickLeaves_EmployeeMentorships_EmployeeMentorshipId",
                        column: x => x.EmployeeMentorshipId,
                        principalTable: "EmployeeMentorships",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenteeSickLeaves_InternMentorships_InternMentorshipId",
                        column: x => x.InternMentorshipId,
                        principalTable: "InternMentorships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoucmentSigningTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeMentorshipDocumentId = table.Column<Guid>(type: "uuid", nullable: true),
                    InternMentorshipDocumentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Time = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoucmentSigningTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoucmentSigningTimes_EmployeeMentorshipDocuments_EmployeeMe~",
                        column: x => x.EmployeeMentorshipDocumentId,
                        principalTable: "EmployeeMentorshipDocuments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DoucmentSigningTimes_InternMentorshipDocuments_InternMentor~",
                        column: x => x.InternMentorshipDocumentId,
                        principalTable: "InternMentorshipDocuments",
                        principalColumn: "Id");
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
                name: "IX_DoucmentSigningTimes_EmployeeMentorshipDocumentId",
                table: "DoucmentSigningTimes",
                column: "EmployeeMentorshipDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DoucmentSigningTimes_InternMentorshipDocumentId",
                table: "DoucmentSigningTimes",
                column: "InternMentorshipDocumentId");

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
                name: "IX_Employees_AppUserId",
                table: "Employees",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesMentors_EmployeeMentorshipId",
                table: "EmployeesMentors",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesMentors_MentorId",
                table: "EmployeesMentors",
                column: "MentorId");

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
                name: "IX_Interns_AppUserId",
                table: "Interns",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternsMentors_InternMentorshipId",
                table: "InternsMentors",
                column: "InternMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternsMentors_MentorId",
                table: "InternsMentors",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_MenteeSickLeaves_EmployeeMentorshipId",
                table: "MenteeSickLeaves",
                column: "EmployeeMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_MenteeSickLeaves_InternMentorshipId",
                table: "MenteeSickLeaves",
                column: "InternMentorshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_EmployeeId",
                table: "Mentors",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId");
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
                name: "DoucmentSigningTimes");

            migrationBuilder.DropTable(
                name: "EmployeesMentors");

            migrationBuilder.DropTable(
                name: "InternsMentors");

            migrationBuilder.DropTable(
                name: "MenteeSickLeaves");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EmployeeMentorshipDocuments");

            migrationBuilder.DropTable(
                name: "InternMentorshipDocuments");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "EmployeeMentorships");

            migrationBuilder.DropTable(
                name: "DocumentSamples");

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
