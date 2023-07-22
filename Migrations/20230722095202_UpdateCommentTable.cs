using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_RepliedBy",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RepliedBy",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RepliedBy",
                table: "Comments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredAt",
                table: "OTPs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 22, 9, 57, 2, 486, DateTimeKind.Utc).AddTicks(5315),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 12, 12, 43, 807, DateTimeKind.Utc).AddTicks(4148));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 22, 9, 52, 2, 486, DateTimeKind.Utc).AddTicks(2902),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 806, DateTimeKind.Utc).AddTicks(6080));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 22, 9, 52, 2, 485, DateTimeKind.Utc).AddTicks(5472),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 805, DateTimeKind.Utc).AddTicks(8587));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredAt",
                table: "OTPs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 12, 12, 43, 807, DateTimeKind.Utc).AddTicks(4148),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 22, 9, 57, 2, 486, DateTimeKind.Utc).AddTicks(5315));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 806, DateTimeKind.Utc).AddTicks(6080),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 22, 9, 52, 2, 486, DateTimeKind.Utc).AddTicks(2902));

            migrationBuilder.AddColumn<Guid>(
                name: "RepliedBy",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 20, 12, 11, 43, 805, DateTimeKind.Utc).AddTicks(8587),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 22, 9, 52, 2, 485, DateTimeKind.Utc).AddTicks(5472));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RepliedBy",
                table: "Comments",
                column: "RepliedBy",
                unique: true,
                filter: "[RepliedBy] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_RepliedBy",
                table: "Comments",
                column: "RepliedBy",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
