using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MegamiManager.Data;

namespace MegamiManager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170205154345_ModifyImage")]
    partial class ModifyImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MegamiManager.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("OwnerId");

                    b.Property<string>("OwnerId1");

                    b.Property<string>("PrivateThumbnailUri")
                        .HasAnnotation("MaxLength", 512);

                    b.Property<string>("PrivateUri")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 512);

                    b.Property<string>("PublicThumbnailUri")
                        .HasAnnotation("MaxLength", 512);

                    b.Property<string>("PublicUri")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 512);

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("SYSDATETIMEOFFSET()");

                    b.HasKey("ImageId");

                    b.HasIndex("OwnerId1");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Megami", b =>
                {
                    b.Property<int>("MegamiId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AerialMobility");

                    b.Property<int>("ArmorDefense");

                    b.Property<string>("Comment")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Design")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<int>("GroundMobility");

                    b.Property<int>("LongRangeBattle");

                    b.Property<int>("MediumRangeBattle");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<int>("OperationTime");

                    b.Property<int>("OwnerId");

                    b.Property<string>("OwnerId1");

                    b.Property<int>("SearchEnemy");

                    b.Property<int>("Secret");

                    b.Property<int>("ShortRangeBattle");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("SYSDATETIMEOFFSET()");

                    b.Property<int>("Weight");

                    b.HasKey("MegamiId");

                    b.HasIndex("OwnerId1");

                    b.ToTable("Megami");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.MegamiTag", b =>
                {
                    b.Property<int>("MegamiId");

                    b.Property<int>("TagId");

                    b.HasKey("MegamiId", "TagId");

                    b.HasIndex("MegamiId");

                    b.HasIndex("TagId");

                    b.ToTable("MegamiTag");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.MegamiTeam", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("MegamiId");

                    b.HasKey("TeamId", "MegamiId");

                    b.HasIndex("MegamiId");

                    b.HasIndex("TeamId");

                    b.ToTable("MegamiTeam");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("TagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<int>("OwnerId");

                    b.Property<string>("OwnerId1");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("SYSDATETIMEOFFSET()");

                    b.HasKey("TeamId");

                    b.HasIndex("OwnerId1");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Weapon", b =>
                {
                    b.Property<int>("WeaponId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<int?>("MegamiId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTimeOffset>("UpdatedAt");

                    b.HasKey("WeaponId");

                    b.HasIndex("MegamiId");

                    b.ToTable("Weapon");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Image", b =>
                {
                    b.HasOne("MegamiManager.Models.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId1");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Megami", b =>
                {
                    b.HasOne("MegamiManager.Models.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId1");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.MegamiTag", b =>
                {
                    b.HasOne("MegamiManager.Models.MegamiModels.Megami", "Megami")
                        .WithMany("MegamiTags")
                        .HasForeignKey("MegamiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MegamiManager.Models.MegamiModels.Tag", "Tag")
                        .WithMany("MegamiTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.MegamiTeam", b =>
                {
                    b.HasOne("MegamiManager.Models.MegamiModels.Megami", "Megami")
                        .WithMany("Teams")
                        .HasForeignKey("MegamiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MegamiManager.Models.MegamiModels.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Team", b =>
                {
                    b.HasOne("MegamiManager.Models.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId1");
                });

            modelBuilder.Entity("MegamiManager.Models.MegamiModels.Weapon", b =>
                {
                    b.HasOne("MegamiManager.Models.MegamiModels.Megami")
                        .WithMany("Armed")
                        .HasForeignKey("MegamiId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MegamiManager.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MegamiManager.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MegamiManager.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
