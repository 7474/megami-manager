using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegamiManager.Data.Migrations
{
    public partial class ModifyPropertyname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediumRangeBattle",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "OperationTime",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "SearchEnemy",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "ShortRangeBattle",
                table: "Megami");

            migrationBuilder.AddColumn<int>(
                name: "ActiveTime",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CloseRangeBattle",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MidRangeBattle",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Recon",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stealth",
                table: "Megami",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTime",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "CloseRangeBattle",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "MidRangeBattle",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "Recon",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "Stealth",
                table: "Megami");

            migrationBuilder.AddColumn<int>(
                name: "MediumRangeBattle",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperationTime",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SearchEnemy",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Secret",
                table: "Megami",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortRangeBattle",
                table: "Megami",
                nullable: false,
                defaultValue: 0);
        }
    }
}
