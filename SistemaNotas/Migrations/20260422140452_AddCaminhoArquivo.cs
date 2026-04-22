using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaNotas.Migrations
{
    /// <inheritdoc />
    public partial class AddCaminhoArquivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaminhoArquivo",
                table: "Notas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoArquivo",
                table: "Notas");
        }
    }
}
