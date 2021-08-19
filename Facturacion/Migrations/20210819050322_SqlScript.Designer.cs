﻿// <auto-generated />
using System;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Facturacion.Migrations
{
    [DbContext(typeof(FacturacionDbContext))]
    [Migration("20210819050322_SqlScript")]
    partial class SqlScript
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Facturacion.Models.Articulo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Estado")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio_Unitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ID_Estado");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("Facturacion.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cuenta_Contable")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Estado")
                        .HasColumnType("int");

                    b.Property<string>("Nombre_Comercial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RNC")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ID_Estado");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Facturacion.Models.Estado", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("Facturacion.Models.Factura", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ID_Articulo")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Asiento")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Cliente")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Vendedor")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("ID_Articulo");

                    b.HasIndex("ID_Cliente");

                    b.HasIndex("ID_Vendedor");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Facturacion.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contraseña")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Vendedor")
                        .HasColumnType("int");

                    b.Property<string>("Nombre_Usuario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ID_Vendedor")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Facturacion.Models.Vendedor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ID_Estado")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Porc_Comision")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ID_Estado");

                    b.ToTable("Vendedores");
                });

            modelBuilder.Entity("Facturacion.Models.Articulo", b =>
                {
                    b.HasOne("Facturacion.Models.Estado", "Estado")
                        .WithMany("Articulos")
                        .HasForeignKey("ID_Estado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Models.Cliente", b =>
                {
                    b.HasOne("Facturacion.Models.Estado", "Estado")
                        .WithMany("Clientes")
                        .HasForeignKey("ID_Estado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Models.Factura", b =>
                {
                    b.HasOne("Facturacion.Models.Articulo", "Articulo")
                        .WithMany("Facturas")
                        .HasForeignKey("ID_Articulo");

                    b.HasOne("Facturacion.Models.Cliente", "Cliente")
                        .WithMany("Facturas")
                        .HasForeignKey("ID_Cliente");

                    b.HasOne("Facturacion.Models.Vendedor", "Vendedor")
                        .WithMany("Facturas")
                        .HasForeignKey("ID_Vendedor");
                });

            modelBuilder.Entity("Facturacion.Models.Usuario", b =>
                {
                    b.HasOne("Facturacion.Models.Vendedor", "Vendedor")
                        .WithOne("Usuario")
                        .HasForeignKey("Facturacion.Models.Usuario", "ID_Vendedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Facturacion.Models.Vendedor", b =>
                {
                    b.HasOne("Facturacion.Models.Estado", "Estado")
                        .WithMany("Vendedores")
                        .HasForeignKey("ID_Estado");
                });
#pragma warning restore 612, 618
        }
    }
}
