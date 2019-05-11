using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.GeoJson.Features;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson {

    public static class GeoJsonUtils {

        public static IGeometry Convert(GeoJsonGeometry geometry) {

            if (geometry == null) throw new ArgumentNullException(nameof(geometry));

            switch (geometry) {

                case GeoJsonPoint point:
                    return Convert(point);

                case GeoJsonLineString lineString:
                    return Convert(lineString);

                case GeoJsonPolygon polygon:
                    return Convert(polygon);

                default:
                    throw new Exception("Unsupported type " + geometry);

            }

        }

        public static IPoint Convert(GeoJsonPoint point) {

            if (point == null) throw new ArgumentNullException(nameof(point));

            return new Point(point.Y, point.X);

        }

        public static ILineString Convert(GeoJsonLineString lineString) {

            if (lineString == null) throw new ArgumentNullException(nameof(lineString));

            var points = lineString.Coordinates.Select(x => new Point(x[1], x[0]));

            return new LineString(points);

        }

        public static IPolygon Convert(GeoJsonPolygon polygon) {

            if (polygon == null) throw new ArgumentNullException(nameof(polygon));

            IPoint[][] coordinates = new IPoint[polygon.Coordinates.Length][];

            for (int i = 0; i < coordinates.Length; i++) {

                coordinates[i] = new IPoint[polygon.Coordinates[i].Length];

                for (int j = 0; j < coordinates[i].Length; j++) {

                    double x = polygon.Coordinates[i][j][0];
                    double y = polygon.Coordinates[i][j][1];

                    coordinates[i][j] = new Point(y, x);
                }

            }

            return new Polygon(coordinates);

        }











        public static IRectangle GetBoundingBox(GeoJsonFeatureCollection[] collections) {

            List<IPoint> points = new List<IPoint>();

            foreach (GeoJsonFeatureCollection collection in collections) {

                foreach (GeoJsonFeature feature in collection.Features) {

                    IRectangle bbox = GetBoundingBox(feature);

                    if (bbox != null) {
                        points.Add(bbox.SouthWest);
                        points.Add(bbox.NorthEast);
                    }

                }

            }

            return points.Count == 0 ? null : new Rectangle(points);

        }

        public static IRectangle GetBoundingBox(GeoJsonFeatureCollection collection) {

            List<IPoint> points = new List<IPoint>();

            foreach (GeoJsonFeature feature in collection.Features) {

                IRectangle bbox = GetBoundingBox(feature);

                if (bbox != null) {
                    points.Add(bbox.SouthWest);
                    points.Add(bbox.NorthEast);
                }

            }

            return points.Count == 0 ? null : new Rectangle(points);

        }

        public static IRectangle GetBoundingBox(GeoJsonFeature feature) {

            IGeometry geometry = Convert(feature.Geometry);

            switch (geometry) {

                case IPoint point:
                    return new Rectangle(point);

                case IShape shape:
                    return shape.GetBoundingBox();

                default:
                    throw new Exception("Unsupported geometry " + geometry.GetType());
                
            }

        }

    }

}