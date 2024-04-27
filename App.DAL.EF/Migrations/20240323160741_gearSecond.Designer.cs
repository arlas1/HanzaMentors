﻿// <auto-generated />
using System;
using App.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.DAL.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240323160741_gearSecond")]
    partial class gearSecond
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("App.Domain.DocumentSample", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Base64Code")
                        .HasColumnType("bytea");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("DocumentSamples");
                });

            modelBuilder.Entity("App.Domain.DoucmentSigningTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeMentorshipDocumentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InternMentorshipDocumentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Time")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeMentorshipDocumentId");

                    b.HasIndex("InternMentorshipDocumentId");

                    b.ToTable("DoucmentSigningTimes");
                });

            modelBuilder.Entity("App.Domain.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("EmployeeType")
                        .HasColumnType("integer");

                    b.Property<string>("Profession")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("App.Domain.EmployeeMentorship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FactorySupervisorId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("FromDate")
                        .HasColumnType("date");

                    b.Property<int>("TotalHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FactorySupervisorId");

                    b.ToTable("EmployeeMentorships");
                });

            modelBuilder.Entity("App.Domain.EmployeeMentorshipDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Base64Code")
                        .HasColumnType("bytea");

                    b.Property<Guid>("DocumentSampleId")
                        .HasColumnType("uuid");

                    b.Property<string>("DocumentStatus")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("EmployeeMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DocumentSampleId");

                    b.HasIndex("EmployeeMentorshipId");

                    b.ToTable("EmployeeMentorshipDocuments");
                });

            modelBuilder.Entity("App.Domain.EmployeeMentorshipUntilDate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("UntilDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeMentorshipId");

                    b.ToTable("EmployeeMentorshipUntilDates");
                });

            modelBuilder.Entity("App.Domain.FactorySupervisor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("FactorySupervisors");
                });

            modelBuilder.Entity("App.Domain.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("App.Domain.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<int>("PersonalCode")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("App.Domain.Identity.AppUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("App.Domain.Intern", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("InternType")
                        .HasColumnType("integer");

                    b.Property<string>("StudyProgram")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Interns");
                });

            modelBuilder.Entity("App.Domain.InternMentorship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FactorySupervisorId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("FromDate")
                        .HasColumnType("date");

                    b.Property<Guid>("InternId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InternSupervisorId")
                        .HasColumnType("uuid");

                    b.Property<int>("TotalHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FactorySupervisorId");

                    b.HasIndex("InternId");

                    b.HasIndex("InternSupervisorId");

                    b.ToTable("InternMentorships");
                });

            modelBuilder.Entity("App.Domain.InternMentorshipDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Base64Code")
                        .HasColumnType("bytea");

                    b.Property<Guid>("DocumentSampleId")
                        .HasColumnType("uuid");

                    b.Property<string>("DocumentStatus")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("InternMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DocumentSampleId");

                    b.HasIndex("InternMentorshipId");

                    b.ToTable("InternMentorshipDocuments");
                });

            modelBuilder.Entity("App.Domain.InternMentorshipUntilDate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InternMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("UntilDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("InternMentorshipId");

                    b.ToTable("InternMentorshipUntilDates");
                });

            modelBuilder.Entity("App.Domain.InternSupervisor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("InternSupervisors");
                });

            modelBuilder.Entity("App.Domain.MenteeSickLeave", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("FromDate")
                        .HasColumnType("date");

                    b.Property<Guid>("InternMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SickLeaveTypeId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("UntilDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeMentorshipId");

                    b.HasIndex("InternMentorshipId");

                    b.HasIndex("SickLeaveTypeId");

                    b.ToTable("MenteeSickLeaves");
                });

            modelBuilder.Entity("App.Domain.Mentor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ChangeReason")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("FromDate")
                        .HasColumnType("date");

                    b.Property<Guid>("InternMentorshipId")
                        .HasColumnType("uuid");

                    b.Property<string>("PaymentAmount")
                        .HasColumnType("text");

                    b.Property<DateOnly>("PaymentOrderDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("UntilDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EmployeeMentorshipId");

                    b.HasIndex("InternMentorshipId");

                    b.ToTable("Mentors");
                });

            modelBuilder.Entity("App.Domain.SickLeaveType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SickLeaveTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("App.Domain.DoucmentSigningTime", b =>
                {
                    b.HasOne("App.Domain.EmployeeMentorshipDocument", "EmployeeMentorshipDocument")
                        .WithMany()
                        .HasForeignKey("EmployeeMentorshipDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.InternMentorshipDocument", "InternMentorshipDocument")
                        .WithMany()
                        .HasForeignKey("InternMentorshipDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeMentorshipDocument");

                    b.Navigation("InternMentorshipDocument");
                });

            modelBuilder.Entity("App.Domain.Employee", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("App.Domain.EmployeeMentorship", b =>
                {
                    b.HasOne("App.Domain.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.FactorySupervisor", "FactorySupervisor")
                        .WithMany()
                        .HasForeignKey("FactorySupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("FactorySupervisor");
                });

            modelBuilder.Entity("App.Domain.EmployeeMentorshipDocument", b =>
                {
                    b.HasOne("App.Domain.DocumentSample", "DocumentSample")
                        .WithMany()
                        .HasForeignKey("DocumentSampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.EmployeeMentorship", "EmployeeMentorship")
                        .WithMany()
                        .HasForeignKey("EmployeeMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentSample");

                    b.Navigation("EmployeeMentorship");
                });

            modelBuilder.Entity("App.Domain.EmployeeMentorshipUntilDate", b =>
                {
                    b.HasOne("App.Domain.EmployeeMentorship", "EmployeeMentorship")
                        .WithMany()
                        .HasForeignKey("EmployeeMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeMentorship");
                });

            modelBuilder.Entity("App.Domain.Identity.AppUserRole", b =>
                {
                    b.HasOne("App.Domain.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.Intern", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("App.Domain.InternMentorship", b =>
                {
                    b.HasOne("App.Domain.FactorySupervisor", "FactorySupervisor")
                        .WithMany()
                        .HasForeignKey("FactorySupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Intern", "Intern")
                        .WithMany()
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.InternSupervisor", "InternSupervisor")
                        .WithMany()
                        .HasForeignKey("InternSupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FactorySupervisor");

                    b.Navigation("Intern");

                    b.Navigation("InternSupervisor");
                });

            modelBuilder.Entity("App.Domain.InternMentorshipDocument", b =>
                {
                    b.HasOne("App.Domain.DocumentSample", "DocumentSample")
                        .WithMany()
                        .HasForeignKey("DocumentSampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.InternMentorship", "InternMentorship")
                        .WithMany()
                        .HasForeignKey("InternMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentSample");

                    b.Navigation("InternMentorship");
                });

            modelBuilder.Entity("App.Domain.InternMentorshipUntilDate", b =>
                {
                    b.HasOne("App.Domain.InternMentorship", "InternMentorship")
                        .WithMany()
                        .HasForeignKey("InternMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InternMentorship");
                });

            modelBuilder.Entity("App.Domain.MenteeSickLeave", b =>
                {
                    b.HasOne("App.Domain.EmployeeMentorship", "EmployeeMentorship")
                        .WithMany()
                        .HasForeignKey("EmployeeMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.InternMentorship", "InternMentorship")
                        .WithMany()
                        .HasForeignKey("InternMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.SickLeaveType", "SickLeaveType")
                        .WithMany()
                        .HasForeignKey("SickLeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeMentorship");

                    b.Navigation("InternMentorship");

                    b.Navigation("SickLeaveType");
                });

            modelBuilder.Entity("App.Domain.Mentor", b =>
                {
                    b.HasOne("App.Domain.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.EmployeeMentorship", "EmployeeMentorship")
                        .WithMany()
                        .HasForeignKey("EmployeeMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.InternMentorship", "InternMentorship")
                        .WithMany()
                        .HasForeignKey("InternMentorshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("EmployeeMentorship");

                    b.Navigation("InternMentorship");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
