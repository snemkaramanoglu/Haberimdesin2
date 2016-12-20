using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Haberimdesin2.Data;

namespace Haberimdesin2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161220183559_added primarykeys")]
    partial class addedprimarykeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Haberimdesin2.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<byte>("Gender");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfileImgURL");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

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

            modelBuilder.Entity("Haberimdesin2.Models.CategoryEntity", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Haberimdesin2.Models.CommentEntity", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("HaberID");

                    b.Property<string>("Id");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserID");

                    b.HasKey("CommentID");

                    b.HasIndex("HaberID");

                    b.HasIndex("Id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Haberimdesin2.Models.DislikeCommentEntity", b =>
                {
                    b.Property<int>("dislikeCommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentID");

                    b.Property<string>("Id");

                    b.Property<string>("UserID");

                    b.HasKey("dislikeCommentID");

                    b.HasIndex("CommentID");

                    b.HasIndex("Id");

                    b.ToTable("DislikeComment");
                });

            modelBuilder.Entity("Haberimdesin2.Models.DislikeHaberEntity", b =>
                {
                    b.Property<int>("dislikeHaberID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HaberID");

                    b.Property<string>("Id");

                    b.Property<string>("UserID");

                    b.HasKey("dislikeHaberID");

                    b.HasIndex("HaberID");

                    b.HasIndex("Id");

                    b.ToTable("DislikeHaber");
                });

            modelBuilder.Entity("Haberimdesin2.Models.HaberEntity", b =>
                {
                    b.Property<int>("HaberID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Detail");

                    b.Property<string>("HeadLine");

                    b.Property<string>("Id");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("PrimaryImgURL");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("Title");

                    b.HasKey("HaberID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("Id");

                    b.ToTable("Haber");
                });

            modelBuilder.Entity("Haberimdesin2.Models.ImageEntity", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HaberID");

                    b.Property<string>("Id");

                    b.Property<string>("ImageURL");

                    b.Property<string>("UserID");

                    b.HasKey("ImageID");

                    b.HasIndex("HaberID");

                    b.HasIndex("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Haberimdesin2.Models.LikeCommentEntity", b =>
                {
                    b.Property<int>("likeCommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentID");

                    b.Property<string>("Id");

                    b.Property<string>("UserID");

                    b.HasKey("likeCommentID");

                    b.HasIndex("CommentID");

                    b.HasIndex("Id");

                    b.ToTable("LikeComment");
                });

            modelBuilder.Entity("Haberimdesin2.Models.LikeHaberEntity", b =>
                {
                    b.Property<int>("likeHaberID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HaberID");

                    b.Property<string>("Id");

                    b.Property<string>("UserID");

                    b.HasKey("likeHaberID");

                    b.HasIndex("HaberID");

                    b.HasIndex("Id");

                    b.ToTable("LikeHaber");
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

            modelBuilder.Entity("Haberimdesin2.Models.CommentEntity", b =>
                {
                    b.HasOne("Haberimdesin2.Models.HaberEntity", "haber")
                        .WithMany()
                        .HasForeignKey("HaberID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Haberimdesin2.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("Id");
                });

            modelBuilder.Entity("Haberimdesin2.Models.DislikeCommentEntity", b =>
                {
                    b.HasOne("Haberimdesin2.Models.CommentEntity", "comment")
                        .WithMany()
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Haberimdesin2.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("Id");
                });

            modelBuilder.Entity("Haberimdesin2.Models.DislikeHaberEntity", b =>
                {
                    b.HasOne("Haberimdesin2.Models.HaberEntity", "haber")
                        .WithMany()
                        .HasForeignKey("HaberID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Haberimdesin2.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("Id");
                });

            modelBuilder.Entity("Haberimdesin2.Models.HaberEntity", b =>
                {
                    b.HasOne("Haberimdesin2.Models.CategoryEntity", "category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Haberimdesin2.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("Id");
                });

            modelBuilder.Entity("Haberimdesin2.Models.ImageEntity", b =>
                {
                    b.HasOne("Haberimdesin2.Models.HaberEntity", "haber")
                        .WithMany()
                        .HasForeignKey("HaberID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Haberimdesin2.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("Id");
                });

            modelBuilder.Entity("Haberimdesin2.Models.LikeCommentEntity", b =>
                {
                    b.HasOne("Haberimdesin2.Models.CommentEntity", "comment")
                        .WithMany()
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Haberimdesin2.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("Id");
                });

            modelBuilder.Entity("Haberimdesin2.Models.LikeHaberEntity", b =>
                {
                    b.HasOne("Haberimdesin2.Models.HaberEntity", "comment")
                        .WithMany()
                        .HasForeignKey("HaberID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Haberimdesin2.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("Id");
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
                    b.HasOne("Haberimdesin2.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Haberimdesin2.Models.ApplicationUser")
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

                    b.HasOne("Haberimdesin2.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
