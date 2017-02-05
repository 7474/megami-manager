using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegamiManager.Data.Migrations
{
    public partial class ModifyPropertyname2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_OwnerId1",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Megami_AspNetUsers_OwnerId1",
                table: "Megami");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_OwnerId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Megami_OwnerId1",
                table: "Megami");

            migrationBuilder.DropIndex(
                name: "IX_Images_OwnerId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Megami");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId",
                table: "Teams",
                column: "OwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Megami",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Megami_OwnerId",
                table: "Megami",
                column: "OwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_OwnerId",
                table: "Images",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_OwnerId",
                table: "Images",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Megami_AspNetUsers_OwnerId",
                table: "Megami",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId",
                table: "Teams",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_OwnerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Megami_AspNetUsers_OwnerId",
                table: "Megami");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_OwnerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Megami_OwnerId",
                table: "Megami");

            migrationBuilder.DropIndex(
                name: "IX_Images_OwnerId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Megami",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Images",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Teams",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId1",
                table: "Teams",
                column: "OwnerId1");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Megami",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Megami_OwnerId1",
                table: "Megami",
                column: "OwnerId1");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Images",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Images_OwnerId1",
                table: "Images",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_OwnerId1",
                table: "Images",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Megami_AspNetUsers_OwnerId1",
                table: "Megami",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId1",
                table: "Teams",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
