using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace PorterGroup.Desafio.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ValidationException : CustomException
    {
        protected ValidationException(string message)
            : base(message)
        {
        }

        protected ValidationException(string message, Exception ex)
            : base(message, ex)
        {
        }

        protected ValidationException()
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
