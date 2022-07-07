using System;
using Skybrud.Essentials.Maps.Extensions;

namespace Skybrud.Essentials.Maps.Geometry.Shapes {
    
    /// <summary>
    /// Class representing a circle on a spheroid.
    /// </summary>
    public class Circle : ICircle {

        private IPolygon _polygon;

        private IPoint _center;
        private double _radius;

        #region Properties

        /// <summary>
        /// Gets or sets the center of the circle.
        /// </summary>
        public IPoint Center {
            get => _center;
            set { _polygon = null; _center = value ?? new Point(0, 0); }
        }

        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        public double Radius {
            get => _radius;
            set { _polygon = null; _radius = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new circle from the specified <paramref name="center"/> and <paramref name="radius"/>.
        /// </summary>
        /// <param name="center">The center point.</param>
        /// <param name="radius">The radius in metres.</param>
        public Circle(IPoint center, double radius) {
            Center = center ?? new Point(0, 0);
            Radius = radius;
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public bool Contains(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            return PointUtils.GetDistance(Center, point) <= Radius;
        }

        /// <inheritdoc />
        public IPoint GetCenter() {
            return new Point(Center.Latitude, Center.Longitude);
        }

        /// <inheritdoc />
        public double GetArea() {
            return (_polygon = _polygon ?? this.ToPolygon()).GetArea();
        }

        /// <inheritdoc />
        public virtual double GetCircumference() {
            return (_polygon = _polygon ?? this.ToPolygon()).GetCircumference();
        }

        /// <inheritdoc />
        public virtual IRectangle GetBoundingBox() {

            IPoint north = MapsUtils.ComputeOffset(Center, Radius, 0);
            IPoint east = MapsUtils.ComputeOffset(Center, Radius, 90);
            IPoint south = MapsUtils.ComputeOffset(Center, Radius, 180);
            IPoint west = MapsUtils.ComputeOffset(Center, Radius, 270);

            return MapsUtils.GetBoundingBox(new []{ north, east, south, west });

        }

        #endregion

    }

}