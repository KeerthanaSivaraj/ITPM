using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LibraryManagement.Data.Migrations
{
    public partial class vethu24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "AddToCard");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "AddToCard",
                newName: "des");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datetime",
                table: "AddToCard",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datetime",
                table: "AddToCard");

            migrationBuilder.RenameColumn(
                name: "des",
                table: "AddToCard",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "AddToCard",
                nullable: false,
                defaultValue: 0);
        }
    }
}
