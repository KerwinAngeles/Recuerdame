using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recuerdame.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaMedicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaMedicamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosis = table.Column<int>(type: "int", nullable: false),
                    FrecuenciaHora = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaMedicamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriaMedicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicamentos_CategoriaMedicamentos_CategoriaMedicamentoId",
                        column: x => x.CategoriaMedicamentoId,
                        principalTable: "CategoriaMedicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TomaProgramada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicamentoId = table.Column<int>(type: "int", nullable: false),
                    FechaHoraProgramada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaConfirmacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoToma = table.Column<int>(type: "int", nullable: false),
                    MedicamentoId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TomaProgramada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TomaProgramada_Medicamentos_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "Medicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TomaProgramada_Medicamentos_MedicamentoId1",
                        column: x => x.MedicamentoId1,
                        principalTable: "Medicamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_CategoriaId",
                table: "Medicamentos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_CategoriaMedicamentoId",
                table: "Medicamentos",
                column: "CategoriaMedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TomaProgramada_MedicamentoId",
                table: "TomaProgramada",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TomaProgramada_MedicamentoId1",
                table: "TomaProgramada",
                column: "MedicamentoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TomaProgramada");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "CategoriaMedicamentos");
        }
    }
}
