using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace PorterGroup.Desafio.Business.Constants
{
    [ExcludeFromCodeCoverage]
    public static class ResponseCodes
    {
        public const int ErrorTryAgain = 06;
        public const int TechnicalFailure = 07;
        public const int InvalidCardType = 14;
        public const int InvalidValue = 18;
        public const int CancelledAccount = 22;
        public const int InvalidData = 30;
        public const int BlockedAccount = 34;
        public const int InconsistentData = 40;
        public const int Nonexistent = 46;
        public const int InactivatedAccount = 60;
        public const int ExceedsDailyLimitValue = 61;
        public const int ServiceUnavailable = 96;
        public const int DuplicateTransaction = 97;
        public const int Success = 00;
        public const int NotApplicable = -1;

        public static string GetDescriptorTo(int responseCode) => responseCode switch
        {
            ErrorTryAgain => "Error Try Again",
            TechnicalFailure => "Technical Failure.",
            InvalidCardType => "Invalid card type",
            InvalidValue => "Invalid value.",
            InvalidData => "Invalid data.",
            InconsistentData => "Inconsistent data.",
            Nonexistent => "Invalid Code",
            ServiceUnavailable => "Service Unavailable",
            CancelledAccount => "Cancelled account",
            BlockedAccount => "Blocked account",
            InactivatedAccount => "Inactivated account",
            ExceedsDailyLimitValue => "Exceeds daily limit value",
            DuplicateTransaction => "Duplicate transaction",
            _ => responseCode.ToString(),
        };

        public static HttpStatusCode GetHttpStatusCodeTo(int responseCode) => responseCode switch
        {
            ErrorTryAgain => HttpStatusCode.InternalServerError,
            TechnicalFailure => HttpStatusCode.InternalServerError,
            InvalidValue => HttpStatusCode.BadRequest,
            InvalidData => HttpStatusCode.BadRequest,
            Nonexistent => HttpStatusCode.NotFound,
            ServiceUnavailable => HttpStatusCode.ServiceUnavailable,
            CancelledAccount => HttpStatusCode.UnprocessableEntity,
            BlockedAccount => HttpStatusCode.UnprocessableEntity,
            InactivatedAccount => HttpStatusCode.UnprocessableEntity,
            DuplicateTransaction => HttpStatusCode.UnprocessableEntity,
            InvalidCardType => HttpStatusCode.UnprocessableEntity,
            ExceedsDailyLimitValue => HttpStatusCode.UnprocessableEntity,
            _ => HttpStatusCode.BadRequest,
        };
    }
}
