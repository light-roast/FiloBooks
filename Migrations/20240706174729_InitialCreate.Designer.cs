﻿// <auto-generated />
using System;
using ControlboxLibreriaAPI.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControlboxLibreriaAPI.Migrations
{
    [DbContext(typeof(FiloBookContext))]
    [Migration("20240706174729_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreCategoria")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Libro", b =>
                {
                    b.Property<int>("LibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Resumen")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlImagen")
                        .HasColumnType("TEXT");

                    b.HasKey("LibroId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Libro");
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Resena", b =>
                {
                    b.Property<int>("ReseñaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Calificacion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaReseña")
                        .HasColumnType("TEXT");

                    b.Property<int>("LibroId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReseñaId");

                    b.HasIndex("LibroId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Resena");
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            UsuarioId = 1,
                            Contraseña = "default_diffindb",
                            CorreoElectronico = "echeverri121@gmail.com",
                            NombreUsuario = "Daniel Echeverri LLano"
                        });
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Libro", b =>
                {
                    b.HasOne("ControlboxLibreriaAPI.Entities.Categoria", "Categoria")
                        .WithMany("Libros")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Resena", b =>
                {
                    b.HasOne("ControlboxLibreriaAPI.Entities.Libro", "Libro")
                        .WithMany("Reseñas")
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlboxLibreriaAPI.Entities.Usuario", "Usuario")
                        .WithMany("Reseñas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Libro");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Categoria", b =>
                {
                    b.Navigation("Libros");
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Libro", b =>
                {
                    b.Navigation("Reseñas");
                });

            modelBuilder.Entity("ControlboxLibreriaAPI.Entities.Usuario", b =>
                {
                    b.Navigation("Reseñas");
                });
#pragma warning restore 612, 618
        }
    }
}
