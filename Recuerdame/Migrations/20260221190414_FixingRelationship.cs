using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recuerdame.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaMedicamentoId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_CategoriaMedicamentoId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "CategoriaMedicamentoId",
                table: "Medicamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaMedicamentoId",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_CategoriaMedicamentoId",
                table: "Medicamentos",
                column: "CategoriaMedicamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaMedicamentoId",
                table: "Medicamentos",
                column: "CategoriaMedicamentoId",
                principalTable: "CategoriaMedicamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
