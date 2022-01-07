using PorterGroup.Desafio.Business.Constants;

namespace PorterGroup.Desafio.Business.Extensions
{
    public static class ResponseCodeExtensions
    {
        public static string ToResponseCodeString(this int responseCodeInt) =>
            responseCodeInt.ToString().PadLeft(2, '0');

        public static int ToResponseCodeInt(this string responseCodeString)
        {
            var responseCodeInt = ResponseCodes.NotApplicable;
            _ = int.TryParse(responseCodeString, out responseCodeInt);

            return responseCodeInt;
        }
    }
}
