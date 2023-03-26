using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutores.Filtros
{
    public class FiltrosExcepcion:ExceptionFilterAttribute
    {
        private readonly ILogger<FiltrosExcepcion> logger;

        public FiltrosExcepcion(ILogger<FiltrosExcepcion> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }

    }
}
