using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PM_API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTicketHistoryActionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("action", "ticket_history", "project");
            migrationBuilder.AddColumn<int>(
                name: "action",
                schema: "project",
                table: "ticket_history",
                type: "integer",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("action", "ticket_history", "project");
            migrationBuilder.AddColumn<string>(
                name: "action",
                schema: "project",
                table: "ticket_history",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false);
        }
    }
}
