using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recuerdame.Migrations
{
    /// <inheritdoc />
    public partial class fixingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_TomaProgramada_Medicamentos_MedicamentoId",
                table: "TomaProgramada");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaId",
                table: "Medicamentos",
                column: "CategoriaId",
                principalTable: "CategoriaMedicamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TomaProgramada_Medicamentos_MedicamentoId",
                table: "TomaProgramada",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_TomaProgramada_Medicamentos_MedicamentoId",
                table: "TomaProgramada");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaId",
                table: "Medicamentos",
                column: "CategoriaId",
                principalTable: "CategoriaMedicamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TomaProgramada_Medicamentos_MedicamentoId",
                table: "TomaProgramada",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
