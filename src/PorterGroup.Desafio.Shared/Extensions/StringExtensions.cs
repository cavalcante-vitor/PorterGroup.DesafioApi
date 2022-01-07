using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PorterGroup.Desafio.Shared.Extensions
{
    public static class StringExtensions
    {
        public static T To<T>(this string str)
        {
            try
            {
                return (T)Convert.ChangeType(str, typeof(T));
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Could not convert string '{str}' to {typeof(T).FullName}.", ex);
            }
        }

        public static string Format(this string format, params object[] args) =>
            string.Format(format, args);

        public static string EmptyCoalesce(this string value, string coalesce) =>
            string.IsNullOrWhiteSpace(value) ? coalesce : value;

        public static byte[] GetBytes(this string value) =>
            Encoding.UTF8.GetBytes(value);

        public static string GetString(this byte[] value) =>
            Encoding.UTF8.GetString(value);

        public static bool IsAllNumerics(this string value) =>
            value?.All(char.IsDigit) == true;

        public static DateTimeOffset ToDateTimeOffset(this ReadOnlySpan<char> value, string format) => DateTimeOffset
            .ParseExact(value, format, new DateTimeFormat(format).FormatProvider, DateTimeStyles.AssumeUniversal);
    }
}
