using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class Teachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    teacher_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    item_category_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    file_name = table.Column<string>(maxLength: 110, nullable: true),
                    slug = table.Column<string>(maxLength: 120, nullable: false),
                    description = table.Column<string>(nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.teacher_id);
                    table.ForeignKey(
                        name: "FK_teachers_itemCategories_item_category_id",
                        column: x => x.item_category_id,
                        principalTable: "itemCategories",
                        principalColumn: "item_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teachers_item_category_id",
                table: "teachers",
                column: "item_category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teachers");
        }
    }
}
