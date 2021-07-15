using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturacion.Migrations
{
    public partial class AddClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Precio_Unitario",
                table: "Articulos",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Comercial = table.Column<string>(nullable: true),
                    RNC = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    Cuenta_Contable = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ID_Estado = table.Column<int>(nullable: false),
                    EstadoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Estados_EstadoID",
                        column: x => x.EstadoID,
                        principalTable: "Estados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EstadoID",
                table: "Clientes",
                column: "EstadoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.AlterColumn<double>(
                name: "Precio_Unitario",
                table: "Articulos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
