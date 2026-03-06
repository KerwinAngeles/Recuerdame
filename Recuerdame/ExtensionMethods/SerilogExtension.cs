using System.Data;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Recuerdame.ExtensionMethods
{
    public static class SerilogExtension
    {
        public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console();

                try
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    var sinkOptions = new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        SchemaName = "dbo",
                        AutoCreateSqlTable = true
                    };
                    var columnOptions = new ColumnOptions();
                    columnOptions.Store.Remove(StandardColumn.MessageTemplate);
                    columnOptions.Store.Add(StandardColumn.LogEvent);
                    columnOptions.TimeStamp.ConvertToUtc = true;

                    configuration.WriteTo.MSSqlServer(
                        connectionString: connectionString,
                        sinkOptions: sinkOptions,
                        columnOptions: columnOptions,
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Serilog] No se pudo configurar el sink de SQL Server: {ex.Message}");
                }
            });
            return builder;
        }
    }
}
