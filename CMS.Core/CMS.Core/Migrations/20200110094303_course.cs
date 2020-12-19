using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "item_categories");

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    course_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    faculty_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    slug = table.Column<string>(maxLength: 120, nullable: false),
                    description = table.Column<string>(nullable: true),
                    specification = table.Column<string>(nullable: true),
                    features = table.Column<string>(nullable: true),
                    is_enabled = table.Column<bool>(nullable: false),
                    file_name = table.Column<string>(maxLength: 110, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.course_id);
                    table.ForeignKey(
                        name: "FK_courses_faculty_faculty_id",
                        column: x => x.faculty_id,
                        principalTable: "faculty",
                        principalColumn: "faculty_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courses_faculty_id",
                table: "courses",
                column: "faculty_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.CreateTable(
                name: "item_categories",
                columns: table => new
                {
                    item_category_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(maxLength: 2000, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    slug = table.Column<string>(maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_categories", x => x.item_category_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(nullable: true),
                    features = table.Column<string>(nullable: true),
                    file_name = table.Column<string>(maxLength: 110, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false),
                    item_category_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    slug = table.Column<string>(maxLength: 120, nullable: false),
                    specification = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_products_item_categories_item_category_id",
                        column: x => x.item_category_id,
                        principalTable: "item_categories",
                        principalColumn: "item_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_item_category_id",
                table: "products",
                column: "item_category_id");
        }
    }
}
