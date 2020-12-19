using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.User.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authentications",
                columns: table => new
                {
                    authentication_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    type_id = table.Column<long>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    username = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authentications", x => x.authentication_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    full_name = table.Column<string>(nullable: false),
                    address_line_1 = table.Column<string>(nullable: false),
                    address_line_2 = table.Column<string>(nullable: true),
                    primary_contact = table.Column<string>(nullable: false),
                    secondary_contact = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    created_by = table.Column<long>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    image_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "login_sessions",
                columns: table => new
                {
                    login_session_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    authentication_id = table.Column<long>(nullable: false),
                    date_time = table.Column<DateTime>(nullable: false),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_sessions", x => x.login_session_id);
                    table.ForeignKey(
                        name: "FK_login_sessions_authentications_authentication_id",
                        column: x => x.authentication_id,
                        principalTable: "authentications",
                        principalColumn: "authentication_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission_maps",
                columns: table => new
                {
                    role_permission_map_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<long>(nullable: false),
                    permission_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission_maps", x => x.role_permission_map_id);
                    table.ForeignKey(
                        name: "FK_role_permission_maps_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_role_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    type = table.Column<int>(nullable: false),
                    type_id = table.Column<long>(nullable: false),
                    role_id = table.Column<long>(nullable: false),
                    user_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_login_sessions_authentication_id",
                table: "login_sessions",
                column: "authentication_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_maps_role_id",
                table: "role_permission_maps",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.InsertData(
           table: "roles",
           columns: new[] { "name", "is_enabled" },
           values: new object[] { "admin", true });

            migrationBuilder.InsertData(
         table: "role_permission_maps",
         columns: new[] { "role_id", "permission_name" },
         values: new object[] { 1, "billing" });

            migrationBuilder.InsertData(
      table: "users",
      columns: new[] { "full_name", "address_line_1", "primary_contact", "email", "is_active", "created_by", "created_date" },
      values: new object[] { "Leading Edge", "birtamode-5", "023-540171", "contact@leadingedgesoft.com", true, 0, DateTime.Now });


            migrationBuilder.InsertData(
   table: "authentications",
   columns: new[] { "type_id", "type", "username", "password", "is_enabled" },
   values: new object[] { 1, 1, "admin", "1000:2g+LR0YZGnowMxRiGsFW72dpPQ6h+HAL:PgxZSf2ETjNUTZfIcPOZ1Lk9Wj2+x1PS", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login_sessions");

            migrationBuilder.DropTable(
                name: "role_permission_maps");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "authentications");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
