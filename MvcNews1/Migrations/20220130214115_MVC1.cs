using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcNews1.Migrations
{
    public partial class MVC1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Categories");
        }
    }
}
