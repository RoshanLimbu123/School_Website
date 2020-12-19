using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class outletrequiredremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "file_name",
                table: "outlets",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 70,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "file_name",
                table: "outlets",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
