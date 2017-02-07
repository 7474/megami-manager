using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MegamiManager.Models;
using MegamiManager.Models.MegamiModels;

namespace MegamiManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Magemi : Tag
            builder.Entity<MegamiTag>()
                .HasKey(t => new { t.MegamiId, t.TagId });
            builder.Entity<MegamiTag>()
                .HasOne(mt => mt.Megami)
                .WithMany(m => m.MegamiTags)
                .HasForeignKey(mt => mt.MegamiId);
            builder.Entity<MegamiTag>()
                .HasOne(mt => mt.Tag)
                .WithMany(t => t.MegamiTags)
                .HasForeignKey(mt => mt.TagId);

            // Team : Megami
            // Teamから引くか、Megamiから引くか微妙なところだがTeamを主とする
            builder.Entity<MegamiTeam>()
                .HasKey(t => new { t.TeamId, t.MegamiId });
            builder.Entity<MegamiTeam>()
                .HasOne(mt => mt.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(mt => mt.TeamId);
            builder.Entity<MegamiTeam>()
                .HasOne(mt => mt.Megami)
                .WithMany(m => m.Teams)
                .HasForeignKey(mt => mt.MegamiId);

            // MegamiImage
            builder.Entity<MegamiImage>()
                .HasKey(x => new { x.MegamiId, x.ImageId });
            builder.Entity<MegamiImage>()
                .HasOne(x => x.Megami)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.MegamiId);
            builder.Entity<MegamiImage>()
                .HasOne(x => x.Image)
                .WithMany(x => x.Megamis)
                .HasForeignKey(x => x.ImageId);

            //
            var entities = new Type[] {
               typeof(Megami),
               typeof(Team),
               typeof(Image),
               typeof(MegamiImage)
            };
            foreach (var entity in entities)
            {
                // XXX もしかしなくてもこれはSQL Server限定なのでは、、、？
                builder.Entity(entity)
                    .Property(typeof(DateTimeOffset), "CreatedAt")
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");
                builder.Entity(entity)
                    .Property(typeof(DateTimeOffset), "UpdatedAt")
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()");
                // XXX 全レコードに跳ねるワロタ
                builder.Entity(entity)
                    .Property(typeof(DateTimeOffset), "UpdatedAt")
                    .HasComputedColumnSql("SYSDATETIMEOFFSET()");
            }
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Megami> Megami { get; set; }
        public DbSet<MegamiTeam> MegamiTeam { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<MegamiImage> MegamiImages { get; set; }
    }
}
