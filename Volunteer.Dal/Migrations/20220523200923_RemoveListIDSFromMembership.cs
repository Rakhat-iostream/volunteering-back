using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.Dal.Migrations
{
    public partial class RemoveListIDSFromMembership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteerIds",
                table: "Memberships");

            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "Memberships",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Memberships");

            migrationBuilder.AddColumn<List<int>>(
                name: "VolunteerIds",
                table: "Memberships",
                type: "integer[]",
                nullable: true);
        }
    }
}
