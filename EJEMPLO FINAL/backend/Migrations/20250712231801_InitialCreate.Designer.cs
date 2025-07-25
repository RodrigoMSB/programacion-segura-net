﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeguridadBancoFinal.Data;

#nullable disable

namespace SeguridadBancoFinal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250712231801_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("SeguridadBancoFinal.Models.CuentaBancaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NumeroCuenta")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("CuentasBancarias");
                });

            modelBuilder.Entity("SeguridadBancoFinal.Models.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CuentaDestinoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CuentaOrigenId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Monto")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CuentaDestinoId");

                    b.HasIndex("CuentaOrigenId");

                    b.ToTable("Movimientos");
                });

            modelBuilder.Entity("SeguridadBancoFinal.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Cliente");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SeguridadBancoFinal.Models.CuentaBancaria", b =>
                {
                    b.HasOne("SeguridadBancoFinal.Models.Usuario", "Usuario")
                        .WithMany("Cuentas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SeguridadBancoFinal.Models.Movimiento", b =>
                {
                    b.HasOne("SeguridadBancoFinal.Models.CuentaBancaria", "CuentaDestino")
                        .WithMany("MovimientosDestino")
                        .HasForeignKey("CuentaDestinoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SeguridadBancoFinal.Models.CuentaBancaria", "CuentaOrigen")
                        .WithMany("MovimientosOrigen")
                        .HasForeignKey("CuentaOrigenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CuentaDestino");

                    b.Navigation("CuentaOrigen");
                });

            modelBuilder.Entity("SeguridadBancoFinal.Models.CuentaBancaria", b =>
                {
                    b.Navigation("MovimientosDestino");

                    b.Navigation("MovimientosOrigen");
                });

            modelBuilder.Entity("SeguridadBancoFinal.Models.Usuario", b =>
                {
                    b.Navigation("Cuentas");
                });
#pragma warning restore 612, 618
        }
    }
}
