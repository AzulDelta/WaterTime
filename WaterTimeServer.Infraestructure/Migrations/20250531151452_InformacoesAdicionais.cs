using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterTimeServer.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InformacoesAdicionais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Altura",
                table: "Usuarios",
                newName: "CapacidadeGarrafa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CapacidadeGarrafa",
                table: "Usuarios",
                newName: "Altura");
        }
    }
}
