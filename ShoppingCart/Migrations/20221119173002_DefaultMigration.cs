using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Migrations
{
    public partial class DefaultMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsideDhaka",
                table: "OrderCarts");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryLocation",
                table: "OrderCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryLocation",
                table: "OrderCarts");

            migrationBuilder.AddColumn<bool>(
                name: "InsideDhaka",
                table: "OrderCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
