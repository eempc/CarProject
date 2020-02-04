using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarProject.Migrations
{
    public partial class userreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserReview",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<string>(nullable: false),
                    ReviewTitle = table.Column<string>(maxLength: 48, nullable: false),
                    ReviewDescription = table.Column<string>(maxLength: 500, nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReview", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_UserReview_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReview_OwnerId",
                table: "UserReview",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReview");
        }
    }
}
