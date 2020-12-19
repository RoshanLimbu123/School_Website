using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class careerslug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "careers",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slug",
                table: "careers");
        }
    }
}
