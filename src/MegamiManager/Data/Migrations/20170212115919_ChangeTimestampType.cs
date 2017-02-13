using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegamiManager.Data.Migrations
{
    public partial class ChangeTimestampType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Weapon",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Teams",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Tag",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "MegamiImages",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Megami",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Images",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Timestamp",
                table: "Weapon",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Timestamp",
                table: "Teams",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Timestamp",
                table: "Tag",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Timestamp",
                table: "MegamiImages",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Timestamp",
                table: "Megami",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Timestamp",
                table: "Images",
                rowVersion: true,
                nullable: true);
        }
    }
}
