using Skybrud.Essentials.Common;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Geometry.Lines {
    
    /// <summary>
    /// Class representing a line between two instances of <see cref="Point"/>.
    /// </summary>
    public class Line : ILine {

        #region Properties

        public IPoint A { get; }

        public IPoint B { get; }

        #endregion

        #region Constructors
        
        public Line(double lat1, double lng1, double lat2, double lng2) {
            A = new Point(lat1, lng1);
            B = new Point(lat2, lng2);
        }

        public Line(IPoint point1, IPoint point2) {
            A = point1;
            B = point2;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the total length of line.
        /// </summary>
        /// <returns>The total length in metres.</returns>
        public double GetLength() {
            if (A == null) throw new PropertyNotSetException(nameof(A));
            if (B == null) throw new PropertyNotSetException(nameof(B));
            return DistanceUtils.GetDistance(A, B);
        }
        
        public IPoint GetCenter() {
            return GetBoundingBox().GetCenter();
        }

        public IRectangle GetBoundingBox() {
            return new Rectangle(new []{A, B});
        }

        #endregion

    }

}