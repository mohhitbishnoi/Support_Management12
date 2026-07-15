using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "tickets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_CompanyId",
                table: "tickets",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_Companys_CompanyId",
                table: "tickets",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_Companys_CompanyId",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_tickets_CompanyId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "tickets");
        }
    }
}
