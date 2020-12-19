using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class commerce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "commerces",
                columns: table => new
                {
                    commerce_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(maxLength: 250, nullable: false),
                    slug = table.Column<string>(maxLength: 120, nullable: false),
                    image_name = table.Column<string>(maxLength: 70, nullable: true),
                    title = table.Column<string>(nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commerces", x => x.commerce_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commerces");
        }
    }
}
