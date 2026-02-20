using Microsoft.EntityFrameworkCore;
using Recuerdame.Common;
using Recuerdame.Dtos.Medicamento;
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
                .AsQueryable();

            if (filtros.FechaConfirmacion.HasValue)
                query = query.Where(m => m.FechaConfirmacion == filtros.FechaConfirmacion.Value);

            if (filtros.FechaHoraProgramada.HasValue)
                query = query.Where(m => m.FechaHoraProgramada == filtros.FechaHoraProgramada.Value);

            if (filtros.MedicamentoId.HasValue)
                query = query.Where(m => m.MedicamentoId == filtros.MedicamentoId.Value);

            query = filtros.OrdenarPor.ToLower() switch
            {
                "fechaHoraProgramada" => filtros.OrdenAscendente
                    ? query.OrderBy(m => m.FechaHoraProgramada)
                    : query.OrderByDescending(m => m.FechaConfirmacion),
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
