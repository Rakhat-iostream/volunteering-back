using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.Dal.Migrations
{
    public partial class Logo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Organizations");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Organizations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Organizations");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Organizations",
                type: "text",
                nullable: true);
        }
    }
}
