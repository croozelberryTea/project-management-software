using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PM_API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreTicketRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ticket_attachment",
                schema: "project",
                columns: table => new
                {
                    ticket_attachment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    file_content = table.Column<byte[]>(type: "bytea", nullable: false),
                    content_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ticket_attachment_pkey", x => x.ticket_attachment_id);
                    table.ForeignKey(
                        name: "FK_ticket_attachment_ticket_ticket_id",
                        column: x => x.ticket_id,
                        principalSchema: "project",
                        principalTable: "ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_attachment_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "identity",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ticket_comment",
                schema: "project",
                columns: table => new
                {
                    ticket_comment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    created_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ticket_comment_pkey", x => x.ticket_comment_id);
                    table.ForeignKey(
                        name: "FK_ticket_comment_ticket_ticket_id",
                        column: x => x.ticket_id,
                        principalSchema: "project",
                        principalTable: "ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_comment_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "identity",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticket_history",
                schema: "project",
                columns: table => new
                {
                    ticket_history_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    details = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: true),
                    created_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ticket_history_pkey", x => x.ticket_history_id);
                    table.ForeignKey(
                        name: "FK_ticket_history_ticket_ticket_id",
                        column: x => x.ticket_id,
                        principalSchema: "project",
                        principalTable: "ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_history_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "identity",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticket_linked_ticket",
                schema: "project",
                columns: table => new
                {
                    ticket_linked_ticket_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    relation = table.Column<int>(type: "integer", nullable: false),
                    parent_ticket_id = table.Column<long>(type: "bigint", nullable: false),
                    child_ticket_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ticket_linked_ticket_pkey", x => x.ticket_linked_ticket_id);
                    table.ForeignKey(
                        name: "FK_ticket_linked_ticket_ticket_child_ticket_id",
                        column: x => x.child_ticket_id,
                        principalSchema: "project",
                        principalTable: "ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_linked_ticket_ticket_parent_ticket_id",
                        column: x => x.parent_ticket_id,
                        principalSchema: "project",
                        principalTable: "ticket",
                        principalColumn: "ticket_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ticket_attachment_ticket_id",
                schema: "project",
                table: "ticket_attachment",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_attachment_user_id",
                schema: "project",
                table: "ticket_attachment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_comment_ticket_id",
                schema: "project",
                table: "ticket_comment",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_comment_user_id",
                schema: "project",
                table: "ticket_comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_history_ticket_id",
                schema: "project",
                table: "ticket_history",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_history_user_id",
                schema: "project",
                table: "ticket_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_linked_ticket_child_ticket_id",
                schema: "project",
                table: "ticket_linked_ticket",
                column: "child_ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_linked_ticket_parent_ticket_id",
                schema: "project",
                table: "ticket_linked_ticket",
                column: "parent_ticket_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_attachment",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ticket_comment",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ticket_history",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ticket_linked_ticket",
                schema: "project");
        }
    }
}
