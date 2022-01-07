#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace PorterGroup.Desafio.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class CustomException : Exception
    {
        protected CustomException()
        {
        }

        protected CustomException(string? message)
            : base(message)
        {
        }

        protected CustomException(string? message, Exception? innerEx)
            : base(message, innerEx)
        {
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
