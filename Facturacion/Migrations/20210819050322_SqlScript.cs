using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Facturacion.Migrations
{
    public partial class SqlScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID ('DEBITAR_STOCK', 'TR') IS NOT NULL
                DROP TRIGGER DEBITAR_STOCK
                GO
                CREATE TRIGGER DEBITAR_STOCK
                ON Facturas
                FOR INSERT
                AS
                BEGIN
	                UPDATE A SET A.Stock = A.Stock - I.Cantidad
	                FROM Articulos as A INNER JOIN
	                INSERTED as I ON I.ID_Articulo = A.ID;
                END

                IF OBJECT_ID ('REGRESAR_STOCK', 'TR') IS NOT NULL
                DROP TRIGGER REGRESAR_STOCK
                GO
                CREATE TRIGGER REGRESAR_STOCK
                ON Facturas
                INSTEAD OF DELETE
                AS
                BEGIN
	                SELECT Cantidad FROM DELETED
	                SELECT ID_Articulo FROM DELETED
	                UPDATE Articulos SET Stock = Stock + (SELECT Cantidad FROM DELETED)
	                WHERE ID = (SELECT ID_Articulo FROM DELETED)
	                DELETE Facturas WHERE ID = (SELECT ID FROM DELETED)
                END

                IF OBJECT_ID ('ACTUALIZAR_STOCK', 'TR') IS NOT NULL
                DROP TRIGGER ACTUALIZAR_STOCK
                GO
                CREATE TRIGGER ACTUALIZAR_STOCK
                ON Facturas
                FOR UPDATE
                AS
                BEGIN
	                UPDATE a SET a.Stock = a.Stock + d.Cantidad
	                FROM Articulos as a INNER JOIN
	                DELETED as d ON d.ID_Articulo = a.ID;

	                UPDATE a SET a.Stock = a.Stock - i.Cantidad
	                FROM Articulos as a INNER JOIN
	                INSERTED as i ON i.ID_Articulo = a.ID;
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
