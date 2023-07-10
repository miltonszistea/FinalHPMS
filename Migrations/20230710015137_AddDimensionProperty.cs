using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalHPMS.Migrations
{
    /// <inheritdoc />
    public partial class AddDimensionProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "Product",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "Product");
        }
    }
}
