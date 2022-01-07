using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PorterGroup.Desafio.Infra.Logger.Logging;

namespace PorterGroup.Desafio.Api.Interceptors
{
    [ExcludeFromCodeCoverage]
    internal class FluentValidatorInterceptor : IValidatorInterceptor
    {
        private const string ValidationFailedMessage = "Request validation failed";

        private readonly ILogWriter _logWriter;

        public FluentValidatorInterceptor(ILogWriter logWriter) =>
            _logWriter = logWriter;

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext) => commonContext;

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(err => new
                    {
                        err.PropertyName,
                        err.ErrorMessage,
                        err.ErrorCode,
                        err.AttemptedValue,
                    });

                _logWriter.Error(ValidationFailedMessage, errors);
                actionContext.HttpContext.Items.Add(nameof(ValidationResult), result);
            }

            return result;
        }
    }
}
