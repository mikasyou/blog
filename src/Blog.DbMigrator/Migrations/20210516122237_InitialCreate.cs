using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blog.DbMigrator.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "varchar(256)", nullable: false),
                    title = table.Column<string>(type: "varchar(512)", nullable: false),
                    sub_title = table.Column<string>(type: "varchar(1024)", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    summary = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    access_counts = table.Column<int>(type: "integer", nullable: false),
                    comment_counts = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ip = table.Column<string>(type: "varchar(128)", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audits", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "article_access_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    article_id = table.Column<int>(type: "integer", nullable: true),
                    create_data = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ip = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_access_log", x => x.id);
                    table.ForeignKey(
                        name: "fk_article_access_log_article_article_id",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(48)", nullable: false),
                    email = table.Column<string>(type: "varchar(256)", nullable: false),
                    web_site = table.Column<string>(type: "varchar(512)", nullable: false),
                    content = table.Column<string>(type: "varchar(1024)", nullable: false),
                    article_id = table.Column<int>(type: "integer", nullable: false),
                    target_id = table.Column<int>(type: "integer", nullable: false),
                    root_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_article_article_id",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_comment_comment_record_id",
                        column: x => x.root_id,
                        principalTable: "comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_comment_comment_target_id",
                        column: x => x.target_id,
                        principalTable: "comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "article_tags",
                columns: table => new
                {
                    articles_id = table.Column<int>(type: "integer", nullable: false),
                    tags_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_tags", x => new { x.articles_id, x.tags_id });
                    table.ForeignKey(
                        name: "fk_article_tags_article_articles_id",
                        column: x => x.articles_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_tags_tags_tags_id",
                        column: x => x.tags_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_access_log_article_id",
                table: "article_access_log",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_article_tags_tags_id",
                table: "article_tags",
                column: "tags_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_article_id",
                table: "comment",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_root_id",
                table: "comment",
                column: "root_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_comment_target_id",
                table: "comment",
                column: "target_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_access_log");

            migrationBuilder.DropTable(
                name: "article_tags");

            migrationBuilder.DropTable(
                name: "audits");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "article");
        }
    }
}
