using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutores.Filtros
{
    public class MiFiltroDeAccion : IActionFilter
    {
        public ILogger<MiFiltroDeAccion> Logger { get; }

        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger)
        {
            Logger = logger;
        }

        //Se Ejecuta primero
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.LogInformation("Antes de ejecutar la accion");
        }
        //Se Ejecuta despues -se ejecuta cuando la accion ya se ha ejecutado
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogInformation("Despues de Ejecutar la accion");

        }


    }
}
