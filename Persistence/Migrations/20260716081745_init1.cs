using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_CustomerId",
                table: "tickets",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_Users_CustomerId",
                table: "tickets",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_Users_CustomerId",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_CustomerId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "tickets");
        }
    }
}
