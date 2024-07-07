﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlboxLibreriaAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdateOnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Usuario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Rol",
                value: "admin");
        }
    }
}
