using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Accounts",
                newName: "PasswordSalt");

            migrationBuilder.AlterColumn<string>(
                name: "OTPCode",
                table: "OTPs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredAt",
                table: "OTPs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 12, 12, 43, 807, DateTimeKind.Utc).AddTicks(4148),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 7, 7, 22, 34, DateTimeKind.Utc).AddTicks(182));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 806, DateTimeKind.Utc).AddTicks(6080),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 7, 6, 22, 33, DateTimeKind.Utc).AddTicks(716));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 805, DateTimeKind.Utc).AddTicks(8587),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 7, 6, 22, 32, DateTimeKind.Utc).AddTicks(690));

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OTPs_OTPCode",
                table: "OTPs",
                column: "OTPCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OTPs_OTPCode",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Accounts",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "OTPCode",
                table: "OTPs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredAt",
                table: "OTPs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 7, 7, 22, 34, DateTimeKind.Utc).AddTicks(182),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 12, 12, 43, 807, DateTimeKind.Utc).AddTicks(4148));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 7, 6, 22, 33, DateTimeKind.Utc).AddTicks(716),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 806, DateTimeKind.Utc).AddTicks(6080));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 7, 6, 22, 32, DateTimeKind.Utc).AddTicks(690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 805, DateTimeKind.Utc).AddTicks(8587));
        }
    }
}
