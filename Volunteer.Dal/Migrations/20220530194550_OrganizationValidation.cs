using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.Dal.Migrations
{
    public partial class OrganizationValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValidationStatus",
                table: "Organizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "CreatedAt", "Email", "FirstName", "LastName", "Login", "PasswordHash", "Phone", "Role", "Status", "UpdatedAt" },
                values: new object[] { 1, null, new DateTime(2022, 5, 30, 19, 45, 49, 384, DateTimeKind.Utc).AddTicks(4070), "admin@admin.com", "Admin", "Admin", "admin", "admin", "+77071234567", 4, 2, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ValidationStatus",
                table: "Organizations");
        }
    }
}
