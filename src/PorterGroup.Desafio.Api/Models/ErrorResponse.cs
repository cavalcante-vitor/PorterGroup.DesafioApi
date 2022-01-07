using System;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using PorterGroup.Desafio.Business.Constants;
using PorterGroup.Desafio.Business.Extensions;
using PorterGroup.Desafio.Business.Models.Responses;

namespace PorterGroup.Desafio.Api.Models
{
    internal record ErrorResponse : BaseResponse
    {
        private static readonly Random _random = new();

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public static int StatusCode { get; private set; }

        public static ErrorResponse FromException() => new()
        {
            ResponseDate = DateTime.Now,
            ResponseCode = ResponseCodes.TechnicalFailure,
        };

        public static ErrorResponse FromValidation(ValidationResult validationResult)
        {
            var errorCode = validationResult.Errors
                .Select(err => err.ErrorCode)
                .Distinct()
                .Max();

            var hasCustomState = validationResult.Errors.Any(err => err.CustomState is not null);
            if (!hasCustomState)
            {
                StatusCode = StatusCodes.Status400BadRequest;
                return new()
                {
                    ResponseDate = DateTime.Now,
                    ResponseCode = 0,
                };
            }

            StatusCode = StatusCodes.Status422UnprocessableEntity;
            return new()
            {
                ResponseDate = DateTime.Now,
                ResponseCode = 0,
            };
        }

        public static ErrorResponse FromBadAuthorization() => new()
        {
            ResponseDate = DateTime.Now,
            ResponseCode = ResponseCodes.TechnicalFailure,
        };
    }
}
