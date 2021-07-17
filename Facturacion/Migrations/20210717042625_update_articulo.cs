using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Migrations
{
    public partial class update_articulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Precio_Unitario",
                table: "Articulos",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Articulos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Articulos");

            migrationBuilder.AlterColumn<double>(
                name: "Precio_Unitario",
                table: "Articulos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
