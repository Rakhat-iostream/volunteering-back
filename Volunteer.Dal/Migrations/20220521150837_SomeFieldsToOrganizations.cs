using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.Dal.Migrations
{
    public partial class SomeFieldsToOrganizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Users_UserId",
                table: "Organizations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Organizations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Organizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int[]>(
                name: "OrganizationTypes",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrganizedDate",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Region",
                table: "Organizations",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 5, 21, 15, 8, 37, 452, DateTimeKind.Utc).AddTicks(5417));

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Users_UserId",
                table: "Organizations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Users_UserId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationTypes",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizedDate",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Organizations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Organizations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 5, 19, 15, 47, 8, 845, DateTimeKind.Utc).AddTicks(646));

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Users_UserId",
                table: "Organizations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
