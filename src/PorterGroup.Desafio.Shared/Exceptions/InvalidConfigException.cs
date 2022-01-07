using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace PorterGroup.Desafio.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidConfigException : CustomException
    {
        private const string ExMessage = "Invalid values for configuration {0}, errors: {1}";

        public InvalidConfigException(string message)
            : base(message)
        {
        }

        public InvalidConfigException(IEnumerable<ValidationResult> validationResults, string configName)
            : base(FormatExMessage(validationResults, configName))
        {
        }

        protected InvalidConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected InvalidConfigException()
        {
        }

        protected InvalidConfigException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }

        private static string FormatExMessage(IEnumerable<ValidationResult> validationResults, string configName) => string.Format(
            ExMessage,
            string.Join(" | ", validationResults.Select(vr => vr.ErrorMessage)),
            configName);
    }
}
