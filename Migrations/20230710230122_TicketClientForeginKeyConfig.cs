using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalHPMS.Migrations
{
    /// <inheritdoc />
    public partial class TicketClientForeginKeyConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "Ticket",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ClientId1",
                table: "Ticket",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Client_ClientId1",
                table: "Ticket",
                column: "ClientId1",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Client_ClientId1",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_ClientId1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Ticket");
        }
    }
}
