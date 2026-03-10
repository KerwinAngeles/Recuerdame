using Recuerdame.Interfaces;

namespace Recuerdame.Services
{
    public class ServicioAutomatizacion
    {
        private readonly IRepositorioTomaProgramada _repositorioTomaProgramada;

        public ServicioAutomatizacion(IRepositorioTomaProgramada repositorioTomaProgramada)
        {
            _repositorioTomaProgramada = repositorioTomaProgramada;
        }

        public async Task<int> CancelarTomasVencidas()
        {
            var ahora = DateTime.UtcNow;

            var tomasVencidas = await _repositorioTomaProgramada.TomasVencidas(ahora);

            foreach(var toma in tomasVencidas)
            {
                toma.EstadoToma = Enums.EstadoToma.Cancelada;
                toma.FechaModificacion = ahora;
                await _repositorioTomaProgramada.UpdateAsync(toma, toma.Id);
            }
            
            return tomasVencidas.Count;
        }
    }
}
    