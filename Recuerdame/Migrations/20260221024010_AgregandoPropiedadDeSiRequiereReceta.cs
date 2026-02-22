using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recuerdame.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoPropiedadDeSiRequiereReceta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequiereReceta",
                table: "CategoriaMedicamentos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiereReceta",
                table: "CategoriaMedicamentos");
        }
    }
}
