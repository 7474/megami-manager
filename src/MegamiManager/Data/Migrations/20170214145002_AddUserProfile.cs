using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegamiManager.Data.Migrations
{
    public partial class AddUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "AspNetUsers",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "AspNetUsers",
                maxLength: 32,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "AspNetUsers");
        }
    }
}
