using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    blog_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    slug = table.Column<string>(maxLength: 120, nullable: false),
                    title = table.Column<string>(maxLength: 150, nullable: false),
                    posted_on = table.Column<DateTime>(nullable: false),
                    artical_by = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(maxLength: 50000, nullable: false),
                    image_name = table.Column<string>(maxLength: 100, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.blog_id);
                });

            migrationBuilder.CreateTable(
                name: "careers",
                columns: table => new
                {
                    career_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    opening_date = table.Column<DateTime>(nullable: false),
                    closing_date = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(maxLength: 2000, nullable: false),
                    image_name = table.Column<string>(maxLength: 70, nullable: true),
                    is_closed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_careers", x => x.career_id);
                });

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    class_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.class_id);
                });

            migrationBuilder.CreateTable(
                name: "designations",
                columns: table => new
                {
                    Designation_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    position = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designations", x => x.Designation_id);
                });

            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    details_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    value1 = table.Column<long>(nullable: false),
                    value2 = table.Column<long>(nullable: false),
                    value3 = table.Column<long>(nullable: false),
                    value4 = table.Column<long>(nullable: false),
                    value0 = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details", x => x.details_id);
                });

            migrationBuilder.CreateTable(
                name: "email_setup",
                columns: table => new
                {
                    email_setup_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    password = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    port = table.Column<string>(nullable: true),
                    host = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_setup", x => x.email_setup_id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    event_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 250, nullable: false),
                    slug = table.Column<string>(maxLength: 120, nullable: false),
                    event_from_date = table.Column<DateTime>(nullable: false),
                    event_to_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    image_name = table.Column<string>(maxLength: 70, nullable: true),
                    venue = table.Column<string>(maxLength: 250, nullable: false),
                    file_name = table.Column<string>(maxLength: 70, nullable: true),
                    time = table.Column<string>(maxLength: 200, nullable: true),
                    is_closed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.event_id);
                });

            migrationBuilder.CreateTable(
                name: "exam_terms",
                columns: table => new
                {
                    exam_term_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_terms", x => x.exam_term_id);
                });

            migrationBuilder.CreateTable(
                name: "faculty",
                columns: table => new
                {
                    faculty_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faculty", x => x.faculty_id);
                });

            migrationBuilder.CreateTable(
                name: "file_uploads",
                columns: table => new
                {
                    file_upload_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(nullable: false),
                    file_name = table.Column<string>(maxLength: 70, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_uploads", x => x.file_upload_id);
                });

            migrationBuilder.CreateTable(
                name: "fiscalYears",
                columns: table => new
                {
                    fiscal_year_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    is_current = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiscalYears", x => x.fiscal_year_id);
                });

            migrationBuilder.CreateTable(
                name: "gallery",
                columns: table => new
                {
                    gallery_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    is_active = table.Column<bool>(nullable: false),
                    _description = table.Column<string>(nullable: true),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gallery", x => x.gallery_id);
                });

            migrationBuilder.CreateTable(
                name: "item_categories",
                columns: table => new
                {
                    item_category_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    slug = table.Column<string>(maxLength: 55, nullable: false),
                    description = table.Column<string>(maxLength: 2000, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_categories", x => x.item_category_id);
                });

            migrationBuilder.CreateTable(
                name: "notices",
                columns: table => new
                {
                    notice_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    notice_date = table.Column<DateTime>(nullable: false),
                    notice_expiry_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    slug = table.Column<string>(maxLength: 120, nullable: false),
                    image_name = table.Column<string>(maxLength: 70, nullable: true),
                    title = table.Column<string>(maxLength: 250, nullable: false),
                    is_closed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notices", x => x.notice_id);
                });

            migrationBuilder.CreateTable(
                name: "page_categories",
                columns: table => new
                {
                    page_category_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 60, nullable: false),
                    slug = table.Column<string>(maxLength: 70, nullable: false),
                    is_enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_page_categories", x => x.page_category_id);
                });

            migrationBuilder.CreateTable(
                name: "received_emails",
                columns: table => new
                {
                    received_email_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sender_email = table.Column<string>(maxLength: 50, nullable: false),
                    first_name = table.Column<string>(maxLength: 30, nullable: false),
                    last_name = table.Column<string>(maxLength: 30, nullable: false),
                    subject = table.Column<string>(maxLength: 150, nullable: false),
                    message = table.Column<string>(maxLength: 1000, nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_received_emails", x => x.received_email_id);
                });

            migrationBuilder.CreateTable(
                name: "setup",
                columns: table => new
                {
                    setup_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    key = table.Column<string>(maxLength: 70, nullable: false),
                    value = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setup", x => x.setup_id);
                });

            migrationBuilder.CreateTable(
                name: "testimonials",
                columns: table => new
                {
                    testimonial_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    person_name = table.Column<string>(maxLength: 50, nullable: false),
                    statement = table.Column<string>(maxLength: 500, nullable: false),
                    designation = table.Column<string>(maxLength: 50, nullable: false),
                    associated_company_name = table.Column<string>(maxLength: 200, nullable: false),
                    image_name = table.Column<string>(maxLength: 100, nullable: true),
                    is_visible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testimonials", x => x.testimonial_id);
                });

            migrationBuilder.CreateTable(
                name: "blog_comment",
                columns: table => new
                {
                    blog_comment_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    blog_id = table.Column<long>(nullable: false),
                    comment_by = table.Column<string>(maxLength: 200, nullable: false),
                    comment_date = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(maxLength: 300, nullable: false),
                    comments = table.Column<string>(maxLength: 5000, nullable: false),
                    blogComment = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_comment", x => x.blog_comment_id);
                    table.ForeignKey(
                        name: "FK_blog_comment_blog_blogComment",
                        column: x => x.blogComment,
                        principalTable: "blog",
                        principalColumn: "blog_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_blog_comment_blog_blog_id",
                        column: x => x.blog_id,
                        principalTable: "blog",
                        principalColumn: "blog_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    member_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Designation_id = table.Column<long>(nullable: false),
                    fiscal_year_id = table.Column<long>(nullable: false),
                    full_name = table.Column<string>(maxLength: 50, nullable: false),
                    address = table.Column<string>(maxLength: 50, nullable: true),
                    contact_number = table.Column<string>(maxLength: 50, nullable: true),
                    image_url = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.member_id);
                    table.ForeignKey(
                        name: "FK_members_designations_Designation_id",
                        column: x => x.Designation_id,
                        principalTable: "designations",
                        principalColumn: "Designation_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_members_fiscalYears_fiscal_year_id",
                        column: x => x.fiscal_year_id,
                        principalTable: "fiscalYears",
                        principalColumn: "fiscal_year_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "routine",
                columns: table => new
                {
                    routine_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fiscal_year_id = table.Column<long>(nullable: false),
                    class_id = table.Column<long>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false),
                    image = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routine", x => x.routine_id);
                    table.ForeignKey(
                        name: "FK_routine_classes_class_id",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "class_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_routine_fiscalYears_fiscal_year_id",
                        column: x => x.fiscal_year_id,
                        principalTable: "fiscalYears",
                        principalColumn: "fiscal_year_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GalleryImage",
                columns: table => new
                {
                    gallery_image_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gallery_id = table.Column<long>(nullable: false),
                    image_name = table.Column<string>(maxLength: 70, nullable: false),
                    title = table.Column<string>(maxLength: 70, nullable: false),
                    description = table.Column<string>(nullable: false),
                    is_enabled = table.Column<bool>(nullable: false),
                    is_slider_image = table.Column<bool>(nullable: false),
                    is_default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImage", x => x.gallery_image_id);
                    table.ForeignKey(
                        name: "FK_GalleryImage_gallery_gallery_id",
                        column: x => x.gallery_id,
                        principalTable: "gallery",
                        principalColumn: "gallery_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    item_category_id = table.Column<long>(nullable: false),
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
                    table.PrimaryKey("PK_products", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_products_item_categories_item_category_id",
                        column: x => x.item_category_id,
                        principalTable: "item_categories",
                        principalColumn: "item_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    page_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    page_category_id = table.Column<long>(nullable: false),
                    title = table.Column<string>(maxLength: 50, nullable: false),
                    slug = table.Column<string>(maxLength: 60, nullable: false),
                    description = table.Column<string>(nullable: true),
                    image_name = table.Column<string>(maxLength: 70, nullable: true),
                    is_enabled = table.Column<bool>(nullable: false),
                    is_home_page = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.page_id);
                    table.ForeignKey(
                        name: "FK_pages_page_categories_page_category_id",
                        column: x => x.page_category_id,
                        principalTable: "page_categories",
                        principalColumn: "page_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blog_comment_blogComment",
                table: "blog_comment",
                column: "blogComment");

            migrationBuilder.CreateIndex(
                name: "IX_blog_comment_blog_id",
                table: "blog_comment",
                column: "blog_id");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImage_gallery_id",
                table: "GalleryImage",
                column: "gallery_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_Designation_id",
                table: "members",
                column: "Designation_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_fiscal_year_id",
                table: "members",
                column: "fiscal_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_pages_page_category_id",
                table: "pages",
                column: "page_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_item_category_id",
                table: "products",
                column: "item_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_routine_class_id",
                table: "routine",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_routine_fiscal_year_id",
                table: "routine",
                column: "fiscal_year_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_comment");

            migrationBuilder.DropTable(
                name: "careers");

            migrationBuilder.DropTable(
                name: "details");

            migrationBuilder.DropTable(
                name: "email_setup");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "exam_terms");

            migrationBuilder.DropTable(
                name: "faculty");

            migrationBuilder.DropTable(
                name: "file_uploads");

            migrationBuilder.DropTable(
                name: "GalleryImage");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "notices");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "received_emails");

            migrationBuilder.DropTable(
                name: "routine");

            migrationBuilder.DropTable(
                name: "setup");

            migrationBuilder.DropTable(
                name: "testimonials");

            migrationBuilder.DropTable(
                name: "blog");

            migrationBuilder.DropTable(
                name: "gallery");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropTable(
                name: "page_categories");

            migrationBuilder.DropTable(
                name: "item_categories");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "fiscalYears");
        }
    }
}
