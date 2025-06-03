using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterTimeServer.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class VolumeGarrafaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolumeAtualGarrafaML",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeAtualGarrafaML",
                table: "Usuarios");
        }
    }
}
