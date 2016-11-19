using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Haberimdesin2.Data.Migrations
{
    public partial class HaberEntityUserIdtoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Haber",
                columns: table => new
                {
                    HaberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    Dislike = table.Column<int>(nullable: false),
                    HeadLine = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true),
                    Latitude = table.Column<float>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    PrimaryImgURL = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Haber", x => x.HaberID);
                    table.ForeignKey(
                        name: "FK_Haber_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Haber_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    Dislike = table.Column<int>(nullable: false),
                    HaberID = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    Like = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_Haber_HaberID",
                        column: x => x.HaberID,
                        principalTable: "Haber",
                        principalColumn: "HaberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HaberID = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Image_Haber_HaberID",
                        column: x => x.HaberID,
                        principalTable: "Haber",
                        principalColumn: "HaberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Image_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_HaberID",
                table: "Comment",
                column: "HaberID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Id",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Haber_CategoryID",
                table: "Haber",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Haber_Id",
                table: "Haber",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Image_HaberID",
                table: "Image",
                column: "HaberID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Id",
                table: "Image",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Haber");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
