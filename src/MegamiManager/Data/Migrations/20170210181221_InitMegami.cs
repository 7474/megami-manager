using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegamiManager.Data.Migrations
{
    public partial class InitMegami : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    ImageType = table.Column<string>(maxLength: 32, nullable: true),
                    Key = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    PrivateThumbnailUri = table.Column<string>(maxLength: 512, nullable: true),
                    PrivateUri = table.Column<string>(maxLength: 512, nullable: false),
                    PublicThumbnailUri = table.Column<string>(maxLength: 512, nullable: true),
                    PublicUri = table.Column<string>(maxLength: 512, nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Megami",
                columns: table => new
                {
                    MegamiId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ActiveTime = table.Column<int>(nullable: false),
                    AerialMobility = table.Column<int>(nullable: false),
                    ArmorDefense = table.Column<int>(nullable: false),
                    CloseRangeBattle = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Design = table.Column<string>(maxLength: 64, nullable: false),
                    GroundMobility = table.Column<int>(nullable: false),
                    LongRangeBattle = table.Column<int>(nullable: false),
                    MidRangeBattle = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    Recon = table.Column<int>(nullable: false),
                    Stealth = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<string>(maxLength: 64, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Megami", x => x.MegamiId);
                    table.ForeignKey(
                        name: "FK_Megami_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
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
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MegamiImages",
                columns: table => new
                {
                    MegamiId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MegamiImages", x => new { x.MegamiId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_MegamiImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MegamiImages_Megami_MegamiId",
                        column: x => x.MegamiId,
                        principalTable: "Megami",
                        principalColumn: "MegamiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    WeaponId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    MegamiId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
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
                name: "IX_Images_OwnerId",
                table: "Images",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Megami_OwnerId",
                table: "Megami",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MegamiImages_ImageId",
                table: "MegamiImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_MegamiImages_MegamiId",
                table: "MegamiImages",
                column: "MegamiId");

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
                name: "IX_Teams_OwnerId",
                table: "Teams",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapon_MegamiId",
                table: "Weapon",
                column: "MegamiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MegamiImages");

            migrationBuilder.DropTable(
                name: "MegamiTag");

            migrationBuilder.DropTable(
                name: "MegamiTeam");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Megami");
        }
    }
}
