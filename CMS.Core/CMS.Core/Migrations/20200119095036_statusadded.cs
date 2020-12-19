using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class statusadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "videos",
                newName: "is_enabled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_enabled",
                table: "videos",
                newName: "is_active");
        }
    }
}
