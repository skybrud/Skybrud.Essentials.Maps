using Skybrud.Essentials.Common;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Geometry.Lines {
    
    /// <summary>
    /// Class representing a line between two instances of <see cref="Point"/>.
    /// </summary>
    public class Line : ILine {

        #region Properties

        /// <summary>
        /// Gets the start point of the line.
        /// </summary>
        public IPoint A { get; }

        /// <summary>
        /// Gets the end point of the line.
        /// </summary>
        public IPoint B { get; }

        #endregion

        #region Constructors
        
        public Line(double lat1, double lng1, double lat2, double lng2) {
            A = new Point(lat1, lng1);
            B = new Point(lat2, lng2);
        }

        public Line(IPoint point1, IPoint point2) {
            A = point1 ?? throw new PropertyNotSetException(nameof(A));
            B = point2 ?? throw new PropertyNotSetException(nameof(B));
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the total length of line.
        /// </summary>
        /// <returns>The total length in metres.</returns>
        public double GetLength() {
            return DistanceUtils.GetDistance(A, B);
        }
        
        /// <summary>
        /// Gets the center of the line.
        /// </summary>
        /// <returns></returns>
        public IPoint GetCenter() {
            // TODO: It shouldn't be necessary to calculate the bounding box first
            return GetBoundingBox().GetCenter();
        }

        public IRectangle GetBoundingBox() {
            return new Rectangle(A, B);
        }

        #endregion

    }

}