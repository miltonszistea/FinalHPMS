using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalHPMS.Migrations
{
    /// <inheritdoc />
    public partial class CommunityIdInProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "Product",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Product");
        }
    }
}
