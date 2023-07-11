using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalHPMS.Migrations
{
    /// <inheritdoc />
    public partial class TicketCommunityFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Client_ClientId1",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "CommunityProduct");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_ClientId1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Ticket");

            migrationBuilder.CreateTable(
                name: "ProductCommunity",
                columns: table => new
                {
                    CommunitiesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCommunity", x => new { x.CommunitiesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductCommunity_Community_CommunitiesId",
                        column: x => x.CommunitiesId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCommunity_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommunity_ProductsId",
                table: "ProductCommunity",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCommunity");

            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "Ticket",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CommunityProduct",
                columns: table => new
                {
                    CommunitiesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityProduct", x => new { x.CommunitiesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CommunityProduct_Community_CommunitiesId",
                        column: x => x.CommunitiesId,
                        principalTable: "Community",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityProduct_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ClientId1",
                table: "Ticket",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityProduct_ProductsId",
                table: "CommunityProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Client_ClientId1",
                table: "Ticket",
                column: "ClientId1",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
