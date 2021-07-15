using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Migrations
{
    public partial class ArreglosTablaClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Estados_EstadoID",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_Estados_ID_Estado",
                table: "Vendedores");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EstadoID",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EstadoID",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "Porc_Comision",
                table: "Vendedores",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Estado",
                table: "Vendedores",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Precio_Unitario",
                table: "Articulos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ID_Estado",
                table: "Clientes",
                column: "ID_Estado");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Estados_ID_Estado",
                table: "Clientes",
                column: "ID_Estado",
                principalTable: "Estados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_Estados_ID_Estado",
                table: "Vendedores",
                column: "ID_Estado",
                principalTable: "Estados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Estados_ID_Estado",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_Estados_ID_Estado",
                table: "Vendedores");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ID_Estado",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "Porc_Comision",
                table: "Vendedores",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID_Estado",
                table: "Vendedores",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoID",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio_Unitario",
                table: "Articulos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EstadoID",
                table: "Clientes",
                column: "EstadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Estados_EstadoID",
                table: "Clientes",
                column: "EstadoID",
                principalTable: "Estados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_Estados_ID_Estado",
                table: "Vendedores",
                column: "ID_Estado",
                principalTable: "Estados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
