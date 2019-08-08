using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Enums;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
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

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">A type inheriting from <see cref="GeoJsonObject"/>.</typeparam>
        /// <param name="json">The JSON string to be parsed.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        public static T Parse<T>(string json) where T : GeoJsonObject {
            return Parse(json) as T;
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance inheriting from <see cref="GeoJsonObject"/>.
        ///
        /// As the type is specified in the JSON, the returned instance for a GeoJSON <strong>Feature</strong> will be
        /// <see cref="GeoJsonFeature"/>, a <strong>Point</strong> will be <see cref="GeoJsonPoint"/> and so accordingly.
        /// </summary>
        /// <param name="json">The JSON string to be parsed.</param>
        /// <returns>An instance of <see cref="GeoJsonObject"/>.</returns>
        public static GeoJsonObject Parse(string json) {
            if (string.IsNullOrWhiteSpace(json)) throw new ArgumentNullException(nameof(json));
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Loads the JSON string at the specified <paramref name="path"/> and parses it into an instance of <typeparamref name="T"/>.
        /// 
        /// As the type is specified in the JSON, the returned instance for a GeoJSON <strong>Feature</strong> will be
        /// <see cref="GeoJsonFeature"/>, a <strong>Point</strong> will be <see cref="GeoJsonPoint"/> and so accordingly.
        ///
        /// If the type specified in the JSON doesn't match that of <typeparamref name="T"/>, this method will return <c>null</c>.
        /// </summary>
        /// <typeparam name="T">A type inheriting from <see cref="GeoJsonObject"/>.</typeparam>
        /// <param name="path">The path to the JSON file on disk.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        public static T Load<T>(string path) where T : GeoJsonObject {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            return Load(path) as T;
        }

        /// <summary>
        /// Loads the JSON string at the specified <paramref name="path"/> and parses it into an instance of <see cref="GeoJsonObject"/>.
        /// 
        /// As the type is specified in the JSON, the returned instance for a GeoJSON <strong>Feature</strong> will be
        /// <see cref="GeoJsonFeature"/>, a <strong>Point</strong> will be <see cref="GeoJsonPoint"/> and so accordingly.
        /// </summary>
        /// <param name="path">The path to the JSON file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonObject"/>.</returns>
        public static GeoJsonObject Load(string path) {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        private static GeoJsonObject Parse(JObject obj) {

            // Get the value of the "type" property
            string type = obj.GetString("type");
            if (string.IsNullOrWhiteSpace(type)) throw new Exception("The JSON object doesn't specify a type");

            // Parse the type into an enum
            if (EnumUtils.TryParseEnum(type, out GeoJsonType result) == false) throw new Exception("Unknown type " + type);

            switch (result) {

                case GeoJsonType.Feature:
                    return GeoJsonFeature.Parse(obj);

                case GeoJsonType.FeatureCollection:
                    return GeoJsonFeatureCollection.Parse(obj);

                case GeoJsonType.Point:
                    return GeoJsonPoint.Parse(obj);

                case GeoJsonType.LineString:
                    return GeoJsonLineString.Parse(obj);

                case GeoJsonType.Polygon:
                    return GeoJsonPolygon.Parse(obj);

                case GeoJsonType.MultiPoint:
                    return GeoJsonMultiPoint.Parse(obj);

                //case GeoJsonType.MultiLineString:
                //    return GeoJsonMultiLineString.Parse(obj);

                case GeoJsonType.MultiPolygon:
                    return GeoJsonMultiPolygon.Parse(obj);

                case GeoJsonType.GeometryCollection:
                    return GeoJsonGeometryCollection.Parse(obj);

                default:
                    throw new Exception("Unknown type " + type);

            }

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

            return points.Count == 0 ? null : MapsUtils.GetBoundingBox(points);

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

            return points.Count == 0 ? null : MapsUtils.GetBoundingBox(points);

        }

        public static IRectangle GetBoundingBox(GeoJsonFeature feature) {

            IGeometry geometry = Convert(feature.Geometry);

            switch (geometry) {

                case IPoint point:
                    return new Rectangle(point, point);

                case IShape shape:
                    return shape.GetBoundingBox();

                default:
                    throw new Exception("Unsupported geometry " + geometry.GetType());
                
            }

        }

    }

}