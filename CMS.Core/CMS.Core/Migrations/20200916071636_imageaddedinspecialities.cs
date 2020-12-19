using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class imageaddedinspecialities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "specialitiesCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_name",
                table: "specialitiesCategories",
                maxLength: 70,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "specialitiesCategories");

            migrationBuilder.DropColumn(
                name: "image_name",
                table: "specialitiesCategories");
        }
    }
}
