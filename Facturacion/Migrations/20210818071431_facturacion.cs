using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Migrations
{
    public partial class facturacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Vendedor = table.Column<int>(nullable: true),
                    ID_Cliente = table.Column<int>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    ID_Articulo = table.Column<int>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ID_Asiento = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Facturas_Articulos_ID_Articulo",
                        column: x => x.ID_Articulo,
                        principalTable: "Articulos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_ID_Cliente",
                        column: x => x.ID_Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Vendedores_ID_Vendedor",
                        column: x => x.ID_Vendedor,
                        principalTable: "Vendedores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ID_Articulo",
                table: "Facturas",
                column: "ID_Articulo");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ID_Cliente",
                table: "Facturas",
                column: "ID_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ID_Vendedor",
                table: "Facturas",
                column: "ID_Vendedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas");
        }
    }
}
