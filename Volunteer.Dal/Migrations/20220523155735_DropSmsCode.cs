using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.Dal.Migrations
{
    public partial class DropSmsCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmsCodes_Users_UserId",
                table: "SmsCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmsCodes",
                table: "SmsCodes");

            migrationBuilder.RenameTable(
                name: "SmsCodes",
                newName: "SmsCode");

            migrationBuilder.RenameIndex(
                name: "IX_SmsCodes_UserId",
                table: "SmsCode",
                newName: "IX_SmsCode_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmsCode",
                table: "SmsCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsCode_Users_UserId",
                table: "SmsCode",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmsCode_Users_UserId",
                table: "SmsCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmsCode",
                table: "SmsCode");

            migrationBuilder.RenameTable(
                name: "SmsCode",
                newName: "SmsCodes");

            migrationBuilder.RenameIndex(
                name: "IX_SmsCode_UserId",
                table: "SmsCodes",
                newName: "IX_SmsCodes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmsCodes",
                table: "SmsCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsCodes_Users_UserId",
                table: "SmsCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
