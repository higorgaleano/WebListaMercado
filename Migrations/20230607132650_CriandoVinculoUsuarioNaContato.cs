using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebListaMercado.Migrations
{
    /// <inheritdoc />
    public partial class CriandoVinculoUsuarioNaContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Compras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Compras");
        }
    }
}
