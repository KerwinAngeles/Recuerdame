using Microsoft.EntityFrameworkCore;
using Recuerdame.Common;
using Recuerdame.Dtos.TomaProgramada;
using Recuerdame.Interfaces;
using Recuerdame.Model;
using Recuerdame.Persistence;

namespace Recuerdame.Repositories
{
    public class RepositorioTomaProgramada : RepositorioGenerico<TomaProgramada>, IRepositorioTomaProgramada
    {
        private readonly ApplicationDbContext _context;
        public RepositorioTomaProgramada(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ResultadoPaginado<TomaProgramada>> GetAllFiltrado(TomaProgramadaFiltro filtros)
        {
            var query = _context.TomaProgramada
                .Include(m => m.Medicamento)
                    .ThenInclude(med => med.CategoriaMedicamento)
                .AsQueryable();

            if (filtros.FechaConfirmacion.HasValue)
                query = query.Where(m => m.FechaConfirmacion == filtros.FechaConfirmacion.Value);

            if (filtros.FechaHoraProgramada.HasValue)
                query = query.Where(m => m.FechaHoraProgramada == filtros.FechaHoraProgramada.Value);

            if (filtros.MedicamentoId.HasValue)
                query = query.Where(m => m.MedicamentoId == filtros.MedicamentoId.Value);

            if (filtros.EstadoToma.HasValue)
                query = query.Where(m => m.EstadoToma == filtros.EstadoToma.Value);

            if (filtros.FechaDesde.HasValue)
                query = query.Where(m => m.FechaHoraProgramada >= filtros.FechaDesde.Value);

            if (filtros.FechaHasta.HasValue)
                query = query.Where(m => m.FechaHoraProgramada <= filtros.FechaHasta.Value);

            query = filtros.OrdenarPor?.ToLower() switch
            {
                "fechahoraprogramada" => filtros.OrdenAscendente
                    ? query.OrderBy(m => m.FechaHoraProgramada)
                    : query.OrderByDescending(m => m.FechaHoraProgramada),
                _ => query.OrderByDescending(m => m.FechaHoraProgramada),
            };

            var total = await query.CountAsync();
            var items = await query
                .Skip((filtros.Pagina - 1) * filtros.TamanoPagina)
                .Take(filtros.TamanoPagina)
                .ToListAsync();

            return new ResultadoPaginado<TomaProgramada>
            {
                Items = items,
                TotalRegistros = total,
                PaginaActual = filtros.Pagina,
                TamanoPagina = filtros.TamanoPagina
            };
        }
    }
}
