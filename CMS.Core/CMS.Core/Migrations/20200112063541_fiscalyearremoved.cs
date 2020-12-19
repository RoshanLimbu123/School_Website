using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class fiscalyearremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_routine_fiscalYears_fiscal_year_id",
                table: "routine");

            migrationBuilder.DropIndex(
                name: "IX_routine_fiscal_year_id",
                table: "routine");

            migrationBuilder.DropColumn(
                name: "fiscal_year_id",
                table: "routine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "fiscal_year_id",
                table: "routine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_routine_fiscal_year_id",
                table: "routine",
                column: "fiscal_year_id");

            migrationBuilder.AddForeignKey(
                name: "FK_routine_fiscalYears_fiscal_year_id",
                table: "routine",
                column: "fiscal_year_id",
                principalTable: "fiscalYears",
                principalColumn: "fiscal_year_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
