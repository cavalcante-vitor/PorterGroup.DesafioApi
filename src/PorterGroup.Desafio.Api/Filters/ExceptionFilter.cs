using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PorterGroup.Desafio.Api.Models;
using PorterGroup.Desafio.Infra.Logger.Logging;

namespace PorterGroup.Desafio.Api.Filters
{
    internal class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogWriter _logWriter;

        public ExceptionFilter(
            ILogWriter logWriter) =>
            _logWriter = logWriter;

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            _logWriter.Error(
                message: ex.Message,
                ex: ex,
                source: ex.TargetSite?.Name);

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(ErrorResponse.FromException());
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
