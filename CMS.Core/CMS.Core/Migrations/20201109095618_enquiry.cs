using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class enquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enquiries",
                columns: table => new
                {
                    enquiry_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(maxLength: 100, nullable: true),
                    contact_number = table.Column<string>(maxLength: 50, nullable: true),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    email = table.Column<string>(maxLength: 300, nullable: false),
                    description = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enquiries", x => x.enquiry_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enquiries");
        }
    }
}
