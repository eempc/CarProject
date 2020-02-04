using Microsoft.EntityFrameworkCore.Migrations;

namespace CarProject.Migrations
{
    public partial class UserReviewBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "UserReview",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "UserReview");
        }
    }
}
