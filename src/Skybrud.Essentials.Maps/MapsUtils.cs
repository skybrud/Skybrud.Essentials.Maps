using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

using static System.Math;

namespace Skybrud.Essentials.Maps {

    public static class MapsUtils {

        /// <summary>
        /// Converts the specified <paramref name="radians"/> to degrees.
        /// </summary>
        /// <param name="radians">The angle specified in radians.</param>
        /// <returns>The angle in degrees.</returns>
        public static double RadiansToDegrees(double radians) {
            return radians * (180.0 / PI);
        }

        /// <summary>
        /// Converts the specified <paramref name="degrees"/> to radians.
        /// </summary>
        /// <param name="degrees">The angle specified in degrees.</param>
        /// <returns>The angle in radians.</returns>
        public static double DegreesToRadians(double degrees) {
            return degrees * PI / 180.0;
        }

    }

}