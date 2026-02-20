using Microsoft.EntityFrameworkCore;
using Recuerdame.Common;
using Recuerdame.Dtos.CategoriaMedicamento;
using Recuerdame.Excepciones;
using Recuerdame.Interfaces;
using Recuerdame.Model;
using Recuerdame.Persistence;

namespace Recuerdame.Repositories
{
    public class RepositorioCategoriaMedicamento : RepositorioGenerico<CategoriaMedicamento>, IRepositorioCategoriaMedicamento
    {
        private readonly ApplicationDbContext _context;
        public RepositorioCategoriaMedicamento(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<ResultadoPaginado<CategoriaMedicamento>> GetAllFiltrado(CategoriaMedicamentoFiltros filtros)
        {
            var query = _context.CategoriaMedicamentos
                .Include(c => c.Medicamentos)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.Nombre))
                query = query.Where(c => c.Nombre.Contains(filtros.Nombre));

            query = filtros.OrdenarPor.ToLower() switch
            {
                "descripcion" => filtros.OrdenAscendente
                    ? query.OrderBy(c => c.Descripcion)
                    : query.OrderByDescending(c => c.Descripcion),
                _ => filtros.OrdenAscendente
                    ? query.OrderBy(c => c.Nombre)
                    : query.OrderByDescending(c => c.Nombre)
            };

            var total = await query.CountAsync();
            var items = await query
                .Skip((filtros.Pagina - 1) * filtros.TamanoPagina)
                .Take(filtros.TamanoPagina)
                .ToListAsync();

            return new ResultadoPaginado<CategoriaMedicamento>
            {
                Items = items,
                TotalRegistros = total,
                PaginaActual = filtros.Pagina,
                TamanoPagina = filtros.TamanoPagina
            };
        }

        public async Task InactivarCategoriaMedicamento(int id)
        {
            var categoria = await _context.CategoriaMedicamentos.FindAsync(id)
                ?? throw new NotFoundException("CategoriaMedicamento", id);
            categoria.Estado = false;
            await _context.SaveChangesAsync();
        }

        public async Task<int> CantidadDeMedicamentosAsignadoAunaCategoria()
        {
            var categoria = _context.CategoriaMedicamentos
                .Include(c => c.Medicamentos).Count();
            return categoria;
        }
    }
}
