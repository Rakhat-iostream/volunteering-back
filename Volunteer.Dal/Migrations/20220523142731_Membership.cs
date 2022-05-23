using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Volunteer.Dal.Migrations
{
    public partial class Membership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Volunteers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MembershipId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VolunteerIds = table.Column<List<int>>(nullable: true),
                    MembershipStatus = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.MembershipId);
                    table.ForeignKey(
                        name: "FK_Memberships_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_MembershipId",
                table: "Volunteers",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_OrganizationId",
                table: "Memberships",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Memberships_MembershipId",
                table: "Volunteers",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "MembershipId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_Memberships_MembershipId",
                table: "Volunteers");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_MembershipId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Volunteers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Login", "PasswordHash", "Phone", "Role", "Status", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2022, 5, 22, 17, 29, 27, 107, DateTimeKind.Utc).AddTicks(6867), "admin@admin.com", "Admin", "Admin", "admin", "admin", "+77071234567", 4, 2, null });
        }
    }
}
