using Microsoft.EntityFrameworkCore;
using Recuerdame.Common;
using Recuerdame.Dtos.Medicamento;
using Recuerdame.Excepciones;
using Recuerdame.Interfaces;
using Recuerdame.Model;
using Recuerdame.Persistence;

namespace Recuerdame.Repositories
{
    public class RepositorioMedicamento : RepositorioGenerico<Medicamento>, IRepositorioMedicamento
    {
        private readonly ApplicationDbContext _context;

        public RepositorioMedicamento(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task InactivarMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id)
                ?? throw new NotFoundException("Medicamento", id);

            medicamento.EstaActivo = false;
            await _context.SaveChangesAsync();
        }

        public async Task<ResultadoPaginado<Medicamento>> GetAllFiltrado(MedicamentoFiltros filtros)
        {
            var query = _context.Medicamentos
                .Include(m => m.CategoriaMedicamento)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.Nombre))
                query = query.Where(m => m.Nombre.Contains(filtros.Nombre));

            if (filtros.CategoriaId.HasValue)
                query = query.Where(m => m.CategoriaId == filtros.CategoriaId.Value);

            if (filtros.EstaActivo.HasValue)
                query = query.Where(m => m.EstaActivo == filtros.EstaActivo.Value);

            if (filtros.FechaInicio.HasValue)
                query = query.Where(m => m.FechaInicio >= filtros.FechaInicio.Value);

            if (filtros.FechaFinal.HasValue)
                query = query.Where(m => m.FechaFinal <= filtros.FechaFinal.Value);

            query = filtros.OrdenarPor.ToLower() switch
            {
                "fechainicio" => filtros.OrdenAscendente
                    ? query.OrderBy(m => m.FechaInicio)
                    : query.OrderByDescending(m => m.FechaInicio),
                "dosis" => filtros.OrdenAscendente
                    ? query.OrderBy(m => m.Dosis)
                    : query.OrderByDescending(m => m.Dosis),
                _ => filtros.OrdenAscendente
                    ? query.OrderBy(m => m.Nombre)
                    : query.OrderByDescending(m => m.Nombre)
            };

            var total = await query.CountAsync();
            var items = await query
                .Skip((filtros.Pagina - 1) * filtros.TamanoPagina)
                .Take(filtros.TamanoPagina)
                .ToListAsync();

            return new ResultadoPaginado<Medicamento>
            {
                Items = items,
                TotalRegistros = total,
                PaginaActual = filtros.Pagina,
                TamanoPagina = filtros.TamanoPagina
            };
        }
    }
}
