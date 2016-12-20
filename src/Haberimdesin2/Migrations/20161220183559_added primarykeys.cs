using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Haberimdesin2.Migrations
{
    public partial class addedprimarykeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DislikeComment",
                columns: table => new
                {
                    dislikeCommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentID = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikeComment", x => x.dislikeCommentID);
                    table.ForeignKey(
                        name: "FK_DislikeComment_Comment_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DislikeComment_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DislikeHaber",
                columns: table => new
                {
                    dislikeHaberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HaberID = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikeHaber", x => x.dislikeHaberID);
                    table.ForeignKey(
                        name: "FK_DislikeHaber_Haber_HaberID",
                        column: x => x.HaberID,
                        principalTable: "Haber",
                        principalColumn: "HaberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DislikeHaber_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeComment",
                columns: table => new
                {
                    likeCommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentID = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeComment", x => x.likeCommentID);
                    table.ForeignKey(
                        name: "FK_LikeComment_Comment_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeComment_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeHaber",
                columns: table => new
                {
                    likeHaberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HaberID = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeHaber", x => x.likeHaberID);
                    table.ForeignKey(
                        name: "FK_LikeHaber_Haber_HaberID",
                        column: x => x.HaberID,
                        principalTable: "Haber",
                        principalColumn: "HaberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeHaber_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DislikeComment_CommentID",
                table: "DislikeComment",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_DislikeComment_Id",
                table: "DislikeComment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DislikeHaber_HaberID",
                table: "DislikeHaber",
                column: "HaberID");

            migrationBuilder.CreateIndex(
                name: "IX_DislikeHaber_Id",
                table: "DislikeHaber",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComment_CommentID",
                table: "LikeComment",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComment_Id",
                table: "LikeComment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LikeHaber_HaberID",
                table: "LikeHaber",
                column: "HaberID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeHaber_Id",
                table: "LikeHaber",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DislikeComment");

            migrationBuilder.DropTable(
                name: "DislikeHaber");

            migrationBuilder.DropTable(
                name: "LikeComment");

            migrationBuilder.DropTable(
                name: "LikeHaber");
        }
    }
}
