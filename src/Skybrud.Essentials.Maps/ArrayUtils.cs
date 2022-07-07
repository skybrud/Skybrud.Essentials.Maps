// ReSharper disable UseArrayEmptyMethod

namespace Skybrud.Essentials.Maps {
    
    internal static class ArrayUtils {

        // TODO: Move to Skybrud.Essentials
        
        private static class EmptyArray<T> {
            internal static readonly T[] Value = new T[0];
        }

        public static T[] Empty<T>() {
            return EmptyArray<T>.Value;
        }

    }

}