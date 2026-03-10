namespace Recuerdame.Services
{
    public class TomasCancelacionWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger _logger;

        // Intervalo de revisión: cada 1 hora
        private readonly TimeSpan _intervalo = TimeSpan.FromHours(1);

        public TomasCancelacionWorker(
            ILogger<TomasCancelacionWorker> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("✅ TomasCancelacionWorker iniciado en: {time}", DateTimeOffset.Now);

            // Ejecutar inmediatamente al arrancar
            await CancelarTomasVencidasAsync();

            // Luego repetir cada hora
            using PeriodicTimer timer = new(_intervalo);

            while (!stoppingToken.IsCancellationRequested &&
                   await timer.WaitForNextTickAsync(stoppingToken))
            {
                await CancelarTomasVencidasAsync();
            }
        }

        private async Task CancelarTomasVencidasAsync()
        {

            try
            {
                // Crear scope para obtener servicios con Scoped lifetime (ej: DbContext)
                using var scope = _scopeFactory.CreateScope();
                var tomasService = scope.ServiceProvider.GetRequiredService<ServicioAutomatizacion>();

                int canceladas = await tomasService.CancelarTomasVencidas();

                if (canceladas > 0)
                    _logger.LogWarning("⚠️  Se cancelaron {cantidad} toma(s) vencida(s).", canceladas);
                else
                    _logger.LogInformation("✔️  No hay tomas vencidas pendientes.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error al intentar cancelar tomas vencidas.");
            }
        }
    }
}
