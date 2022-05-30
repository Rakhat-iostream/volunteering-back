using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.Dal.Migrations
{
    public partial class AttendanceCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttendanceCode",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "AttendedVolunteerIds",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceCode",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AttendedVolunteerIds",
                table: "Events");
        }
    }
}
