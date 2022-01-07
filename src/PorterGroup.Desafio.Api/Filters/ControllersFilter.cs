using Microsoft.AspNetCore.Mvc.Filters;
using PorterGroup.Desafio.Infra.Logger.Logging;

namespace PorterGroup.Desafio.Api.Filters
{
    internal class ControllersFilter : IActionFilter
    {
        private readonly ILogWriter _logWriter;

        public ControllersFilter(
            ILogWriter logWriter) =>
            _logWriter = logWriter;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controllerName = context.Controller.GetType().FullName;
            _logWriter.Info($"Executed {controllerName}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.Controller.GetType().FullName;
            _logWriter.Info($"Executing {controllerName}");
        }
    }
}
