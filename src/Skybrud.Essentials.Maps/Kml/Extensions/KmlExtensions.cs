using System.Diagnostics.CodeAnalysis;

namespace Skybrud.Essentials.Maps.Kml.Extensions;

internal static class KmlExtensions {

    internal static bool HasValue([NotNullWhen(true)] this object? obj) {
        return obj switch {
            string str => string.IsNullOrWhiteSpace(str) == false,
            _ => obj != null
        };
    }

    internal static bool IsDefault(KmlLinkRefreshMode value) {
        return value == default;
    }

    internal static bool IsNotDefault(KmlLinkRefreshMode value) {
        return value != default;
    }

}

//public class Enum<T> where T : struct, IConvertible
//{
//    public static int Count
//    {
//        get
//        {
//            if (!typeof(T).IsEnum)
//                throw new ArgumentException("T must be an enumerated type");

//            return Enum.GetNames(typeof(T)).Length;
//        }
//    }

//    public static bool IsDefault(T value)
//    {
//        return value == default(T);
//    }

//}