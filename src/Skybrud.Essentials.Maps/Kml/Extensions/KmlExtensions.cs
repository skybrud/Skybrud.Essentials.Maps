using System;

namespace Skybrud.Essentials.Maps.Kml.Extensions {

    internal static class KmlExtensions {

        internal static bool HasValue(this object obj) {

            switch (obj) {

                case string str:
                    return String.IsNullOrWhiteSpace(str) == false;

                default:
                    return obj != null;
                    
            }

        }

        internal static bool IsDefault(KmlLinkRefreshMode value) {
            return value == default(KmlLinkRefreshMode);
        }

        internal static bool IsNotDefault(KmlLinkRefreshMode value) {
            return value != default(KmlLinkRefreshMode);
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

}