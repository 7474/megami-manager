using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MegamiManager.Data.Migrations
{
    public partial class InitMegami : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Megami",
                columns: table => new
                {
                    MegamiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AerialMobility = table.Column<int>(nullable: false),
                    ArmorDefense = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Design = table.Column<string>(maxLength: 64, nullable: false),
                    GroundMobility = table.Column<int>(nullable: false),
                    LongRangeBattle = table.Column<int>(nullable: false),
                    MediumRangeBattle = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    OperationTime = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    OwnerId1 = table.Column<string>(nullable: true),
                    SearchEnemy = table.Column<int>(nullable: false),
                    Secret = table.Column<int>(nullable: false),
                    ShortRangeBattle = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 64, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Megami", x => x.MegamiId);
                    table.ForeignKey(
                        name: "FK_Megami_AspNetUsers_OwnerId1",
                        column: x => x.OwnerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    OwnerId1 = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_OwnerId1",
                        column: x => x.OwnerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    WeaponId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    MegamiId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon", x => x.WeaponId);
                    table.ForeignKey(
                        name: "FK_Weapon_Megami_MegamiId",
                        column: x => x.MegamiId,
                        principalTable: "Megami",
                        principalColumn: "MegamiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MegamiTag",
                columns: table => new
                {
                    MegamiId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MegamiTag", x => new { x.MegamiId, x.TagId });
                    table.ForeignKey(
                        name: "FK_MegamiTag_Megami_MegamiId",
                        column: x => x.MegamiId,
                        principalTable: "Megami",
                        principalColumn: "MegamiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MegamiTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MegamiTeam",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    MegamiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MegamiTeam", x => new { x.TeamId, x.MegamiId });
                    table.ForeignKey(
                        name: "FK_MegamiTeam_Megami_MegamiId",
                        column: x => x.MegamiId,
                        principalTable: "Megami",
                        principalColumn: "MegamiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MegamiTeam_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Megami_OwnerId1",
                table: "Megami",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_MegamiTag_MegamiId",
                table: "MegamiTag",
                column: "MegamiId");

            migrationBuilder.CreateIndex(
                name: "IX_MegamiTag_TagId",
                table: "MegamiTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_MegamiTeam_MegamiId",
                table: "MegamiTeam",
                column: "MegamiId");

            migrationBuilder.CreateIndex(
                name: "IX_MegamiTeam_TeamId",
                table: "MegamiTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId1",
                table: "Teams",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Weapon_MegamiId",
                table: "Weapon",
                column: "MegamiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MegamiTag");

            migrationBuilder.DropTable(
                name: "MegamiTeam");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Megami");
        }
    }
}
