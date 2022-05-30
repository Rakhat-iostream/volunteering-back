using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.Dal.Migrations
{
    public partial class NewObjectsForEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Region",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Events");
        }
    }
}
