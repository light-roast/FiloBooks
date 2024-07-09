using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlboxLibreriaAPI.Migrations
{
    /// <inheritdoc />
    public partial class NReseña : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resena_Usuario_UsuarioFirebaseUserId",
                table: "Resena");

            migrationBuilder.DropColumn(
                name: "FirebaseUserId",
                table: "Resena");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioFirebaseUserId",
                table: "Resena",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resena_Usuario_UsuarioFirebaseUserId",
                table: "Resena",
                column: "UsuarioFirebaseUserId",
                principalTable: "Usuario",
                principalColumn: "FirebaseUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resena_Usuario_UsuarioFirebaseUserId",
                table: "Resena");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioFirebaseUserId",
                table: "Resena",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "FirebaseUserId",
                table: "Resena",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Resena_Usuario_UsuarioFirebaseUserId",
                table: "Resena",
                column: "UsuarioFirebaseUserId",
                principalTable: "Usuario",
                principalColumn: "FirebaseUserId");
        }
    }
}
