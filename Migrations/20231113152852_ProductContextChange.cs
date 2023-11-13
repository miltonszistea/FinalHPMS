using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalHPMS.Migrations
{
    /// <inheritdoc />
    public partial class ProductContextChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Ticket_TicketId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCommunity_Community_CommunitiesId",
                table: "ProductCommunity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCommunity_Product_ProductsId",
                table: "ProductCommunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Client_ClientId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Community_CommunityId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCommunity",
                table: "ProductCommunity");

            migrationBuilder.DropIndex(
                name: "IX_ProductCommunity_ProductsId",
                table: "ProductCommunity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_TicketId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Community",
                table: "Community");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Community");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Community");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Community",
                newName: "Communities");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductCommunity",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CommunitiesId",
                table: "ProductCommunity",
                newName: "CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_CommunityId",
                table: "Tickets",
                newName: "IX_Tickets_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_ClientId",
                table: "Tickets",
                newName: "IX_Tickets_ClientId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductCommunity",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Tickets",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tickets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategory",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCommunity",
                table: "ProductCommunity",
                columns: new[] { "ProductId", "CommunityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Communities",
                table: "Communities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductTicket",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTicket", x => new { x.ProductId, x.TicketId });
                    table.ForeignKey(
                        name: "FK_ProductTicket_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTicket_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommunity_CommunityId",
                table: "ProductCommunity",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTicket_TicketId",
                table: "ProductTicket",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCommunity_Communities_CommunityId",
                table: "ProductCommunity",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCommunity_Products_ProductId",
                table: "ProductCommunity",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Communities_CommunityId",
                table: "Tickets",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCommunity_Communities_CommunityId",
                table: "ProductCommunity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCommunity_Products_ProductId",
                table: "ProductCommunity");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Communities_CommunityId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "ProductTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCommunity",
                table: "ProductCommunity");

            migrationBuilder.DropIndex(
                name: "IX_ProductCommunity_CommunityId",
                table: "ProductCommunity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Communities",
                table: "Communities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductCommunity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Communities",
                newName: "Community");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductCommunity",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "CommunityId",
                table: "ProductCommunity",
                newName: "CommunitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CommunityId",
                table: "Ticket",
                newName: "IX_Ticket_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ClientId",
                table: "Ticket",
                newName: "IX_Ticket_ClientId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Product",
                newName: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "Total",
                table: "Ticket",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Ticket",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Product",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "Product",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Product",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Community",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Community",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Client",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCommunity",
                table: "ProductCommunity",
                columns: new[] { "CommunitiesId", "ProductsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Community",
                table: "Community",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCommunity_ProductsId",
                table: "ProductCommunity",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TicketId",
                table: "Product",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Ticket_TicketId",
                table: "Product",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCommunity_Community_CommunitiesId",
                table: "ProductCommunity",
                column: "CommunitiesId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCommunity_Product_ProductsId",
                table: "ProductCommunity",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Client_ClientId",
                table: "Ticket",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Community_CommunityId",
                table: "Ticket",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
