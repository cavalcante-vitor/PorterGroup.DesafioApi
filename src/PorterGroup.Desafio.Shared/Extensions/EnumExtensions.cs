using System;

namespace PorterGroup.Desafio.Shared.Extensions
{
    public static class EnumExtensions
    {
        private const string EnumConvertError = "Can not convert {0} into {1}.";

        public static T AsEnum<T>(this int value)
            where T : struct
        {
            bool valid = Enum.IsDefined(typeof(T), value);
            if (!valid)
            {
                throw new InvalidCastException(string.Format(EnumConvertError, value, typeof(T).Name));
            }

            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T AsEnum<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            var success = Enum.IsDefined(typeof(T), value);
            if (!success)
            {
                throw new InvalidCastException(EnumConvertError.Format(value, typeof(T).Name));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

        public static string AsString(this Enum value, string defaultValue = "") =>
            Enum.GetName(value.GetType(), value) ?? defaultValue;

        public static int ToInteger(this Enum value) => Convert.ToInt32(value);
    }
}
