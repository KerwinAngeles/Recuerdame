using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recuerdame.Common;
using Recuerdame.Excepciones;

namespace Recuerdame.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(
                context.Exception,
                "Error no controlado en {Controller}.{Action}: {Message}",
                context.RouteData.Values["controller"],
                context.RouteData.Values["action"],
                context.Exception.Message);

            var (statusCode, response) = context.Exception switch
            {
                NotFoundException ex => (
                    StatusCodes.Status404NotFound,
                    ApiResponse<object>.Fallido(ex.Message)
                ),
                BusinessException ex => (
                    StatusCodes.Status400BadRequest,
                    ApiResponse<object>.Fallido(ex.Message)
                ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.Fallido("Ocurrió un error interno. Por favor, contacte al administrador.")
                )
            };

            context.Result = new ObjectResult(response) { StatusCode = statusCode };
            context.ExceptionHandled = true;
        }
    }
}
