using Recuerdame.Dtos.Medicamento;
using Recuerdame.Dtos.TomaProgramada;
using Recuerdame.Interfaces;
using Recuerdame.Model;

namespace Recuerdame.Services
{
    public class ServicioTomaProgramada
    {
        private readonly IRepositorioTomaProgramada _repositorioTomaProgramda;
        public ServicioTomaProgramada(IRepositorioTomaProgramada repositorioTomaProgramada)
        {
            _repositorioTomaProgramda = repositorioTomaProgramada; 
        }
    }
}
