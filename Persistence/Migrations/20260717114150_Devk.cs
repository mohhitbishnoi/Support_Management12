using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Devk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketsHistroys_Users_UserId",
                table: "ticketsHistroys");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketsHistroys_tickets_TicketId",
                table: "ticketsHistroys");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketsReplys_Users_UserId",
                table: "ticketsReplys");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketsReplys_tickets_TicketId",
                table: "ticketsReplys");

            migrationBuilder.AddColumn<int>(
                name: "AssignedEmployeeId",
                table: "tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "SlASettings");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "SlASettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_AssignedEmployeeId",
                table: "tickets",
                column: "AssignedEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_Users_AssignedEmployeeId",
                table: "tickets",
                column: "AssignedEmployeeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsHistroys_Users_UserId",
                table: "ticketsHistroys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsHistroys_tickets_TicketId",
                table: "ticketsHistroys",
                column: "TicketId",
                principalTable: "tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsReplys_Users_UserId",
                table: "ticketsReplys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsReplys_tickets_TicketId",
                table: "ticketsReplys",
                column: "TicketId",
                principalTable: "tickets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tickets_Users_AssignedEmployeeId",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketsHistroys_Users_UserId",
                table: "ticketsHistroys");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketsHistroys_tickets_TicketId",
                table: "ticketsHistroys");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketsReplys_Users_UserId",
                table: "ticketsReplys");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketsReplys_tickets_TicketId",
                table: "ticketsReplys");

            migrationBuilder.DropIndex(
                name: "IX_tickets_AssignedEmployeeId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "AssignedEmployeeId",
                table: "tickets");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "SlASettings",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsHistroys_Users_UserId",
                table: "ticketsHistroys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsHistroys_tickets_TicketId",
                table: "ticketsHistroys",
                column: "TicketId",
                principalTable: "tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsReplys_Users_UserId",
                table: "ticketsReplys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticketsReplys_tickets_TicketId",
                table: "ticketsReplys",
                column: "TicketId",
                principalTable: "tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
