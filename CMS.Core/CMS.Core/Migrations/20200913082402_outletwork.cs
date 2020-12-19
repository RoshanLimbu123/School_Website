using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class outletwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_uploads");

            migrationBuilder.CreateTable(
                name: "outlets",
                columns: table => new
                {
                    outlet_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    address = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: false),
                    file_name = table.Column<string>(maxLength: 70, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outlets", x => x.outlet_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outlets");

            migrationBuilder.CreateTable(
                name: "file_uploads",
                columns: table => new
                {
                    file_upload_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(nullable: false),
                    file_name = table.Column<string>(maxLength: 70, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_uploads", x => x.file_upload_id);
                });
        }
    }
}
