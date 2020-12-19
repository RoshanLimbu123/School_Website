using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class specialitiesandcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "specialitiesCategories",
                columns: table => new
                {
                    specialities_category_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 60, nullable: false),
                    slug = table.Column<string>(maxLength: 70, nullable: false),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialitiesCategories", x => x.specialities_category_id);
                });

            migrationBuilder.CreateTable(
                name: "specialities",
                columns: table => new
                {
                    specialities_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    specialities_category_id = table.Column<long>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    slug = table.Column<string>(maxLength: 60, nullable: false),
                    description = table.Column<string>(nullable: true),
                    image_name = table.Column<string>(maxLength: 70, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialities", x => x.specialities_id);
                    table.ForeignKey(
                        name: "FK_specialities_specialitiesCategories_specialities_category_id",
                        column: x => x.specialities_category_id,
                        principalTable: "specialitiesCategories",
                        principalColumn: "specialities_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_specialities_specialities_category_id",
                table: "specialities",
                column: "specialities_category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "specialities");

            migrationBuilder.DropTable(
                name: "specialitiesCategories");
        }
    }
}
