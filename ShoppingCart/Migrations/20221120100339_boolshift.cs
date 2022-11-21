using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Migrations
{
    public partial class boolshift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LocationIsDhaka",
                table: "OrderCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LocationOutsideDhaka",
                table: "OrderCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationIsDhaka",
                table: "OrderCarts");

            migrationBuilder.DropColumn(
                name: "LocationOutsideDhaka",
                table: "OrderCarts");
        }
    }
}
