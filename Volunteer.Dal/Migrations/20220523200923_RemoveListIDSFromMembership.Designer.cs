﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Migrations
{
    [DbContext(typeof(VolContext))]
    [Migration("20220523200923_RemoveListIDSFromMembership")]
    partial class RemoveListIDSFromMembership
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EventName")
                        .HasColumnType("text");

                    b.Property<bool?>("IsFinished")
                        .HasColumnType("boolean");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("VolunteerIds")
                        .HasColumnType("integer[]");

                    b.Property<int?>("VolunteeringCategory")
                        .HasColumnType("integer");

                    b.HasKey("EventId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Membership", b =>
                {
                    b.Property<int>("MembershipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MembershipStatus")
                        .HasColumnType("integer");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("integer");

                    b.Property<int?>("VolunteerId")
                        .HasColumnType("integer");

                    b.HasKey("MembershipId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CEO")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int?>("Experience")
                        .HasColumnType("integer");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("text");

                    b.Property<int[]>("OrganizationTypes")
                        .HasColumnType("integer[]");

                    b.Property<DateTime?>("OrganizedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int?>("Region")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int[]>("VolunteeringCategories")
                        .HasColumnType("integer[]");

                    b.HasKey("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Volunteer", b =>
                {
                    b.Property<int>("VolunteerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("Experience")
                        .HasColumnType("integer");

                    b.Property<int?>("MembershipId")
                        .HasColumnType("integer");

                    b.Property<int?>("Region")
                        .HasColumnType("integer");

                    b.Property<bool?>("Sex")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<int[]>("VolunteeringCategories")
                        .HasColumnType("integer[]");

                    b.HasKey("VolunteerId");

                    b.HasIndex("MembershipId");

                    b.HasIndex("UserId");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Event", b =>
                {
                    b.HasOne("Volunteer.Common.Models.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Membership", b =>
                {
                    b.HasOne("Volunteer.Common.Models.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Organization", b =>
                {
                    b.HasOne("Volunteer.Common.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.RefreshToken", b =>
                {
                    b.HasOne("Volunteer.Common.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volunteer.Common.Models.Domain.Volunteer", b =>
                {
                    b.HasOne("Volunteer.Common.Models.Domain.Membership", "Membership")
                        .WithMany()
                        .HasForeignKey("MembershipId");

                    b.HasOne("Volunteer.Common.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
