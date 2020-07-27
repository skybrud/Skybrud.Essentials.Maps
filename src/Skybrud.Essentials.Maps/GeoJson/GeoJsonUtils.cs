using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Enums;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Features;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson {

    /// <summary>
    /// Class with with various statuc methods for working with <strong>GeoJSON</strong>.
    /// </summary>
    public static class GeoJsonUtils {

        /// <summary>
        /// Converts the specified GeoJSON <paramref name="geometry"/> into an instance of <see cref="IGeometry"/>.
        /// </summary>
        /// <param name="geometry">The GeoJSON geometry to be converted.</param>
        /// <returns>An instance of <see cref="IGeometry"/>.</returns>
        public static IGeometry Convert(IGeoJsonGeometry geometry) {
            if (geometry == null) throw new ArgumentNullException(nameof(geometry));
            return geometry.ToGeometry();
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

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance deriving from <see cref="GeoJsonObject"/>.
        ///
        /// As the GeoJSON format specified the type of a
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance that derives from <see cref="GeoJsonObject"/>.</returns>
        private static GeoJsonObject Parse(JObject json) {

            if (json == null) return null;

            // Get the value of the "type" property
            string type = json.GetString("type");
            if (string.IsNullOrWhiteSpace(type)) throw new Exception("The JSON object doesn't specify a type");

            // Parse the type into an enum
            if (EnumUtils.TryParseEnum(type, out GeoJsonType result) == false) throw new Exception("Unknown type " + type);

            switch (result) {

                case GeoJsonType.Feature:
                    return GeoJsonFeature.Parse(json);

                case GeoJsonType.FeatureCollection:
                    return GeoJsonFeatureCollection.Parse(json);

                case GeoJsonType.Point:
                    return GeoJsonPoint.Parse(json);

                case GeoJsonType.LineString:
                    return GeoJsonLineString.Parse(json);

                case GeoJsonType.Polygon:
                    return GeoJsonPolygon.Parse(json);

                case GeoJsonType.MultiPoint:
                    return GeoJsonMultiPoint.Parse(json);

                //case GeoJsonType.MultiLineString:
                //    return GeoJsonMultiLineString.Parse(obj);

                case GeoJsonType.MultiPolygon:
                    return GeoJsonMultiPolygon.Parse(json);

                case GeoJsonType.GeometryCollection:
                    return GeoJsonGeometryCollection.Parse(json);

                default:
                    throw new Exception("Unknown type " + type);

            }

        }

        /// <summary>
        /// Returns an instance of <see cref="IRectangle"/> representing the bounding box of the specified GeoJSON <paramref name="collections"/>.
        /// </summary>
        /// <param name="collections">An array of feature collections.</param>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collections"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="collections"/> is empty.</exception>
        public static IRectangle GetBoundingBox(GeoJsonFeatureCollection[] collections) {

            if (collections == null) throw new ArgumentNullException(nameof(collections));
            if (collections.Length == 0) throw new InvalidOperationException(nameof(collections));

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

        /// <summary>
        /// Returns an instance of <see cref="IRectangle"/> representing the bounding box of the specified GeoJSON <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">The feature collection.</param>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="collection"/> is empty.</exception>
        public static IRectangle GetBoundingBox(GeoJsonFeatureCollection collection) {

            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (collection.Features.Count == 0) throw new InvalidOperationException(nameof(collection));
            
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

        /// <summary>
        /// Returns an instance of <see cref="IRectangle"/> representing the bounding box of the GeoJSON specified <paramref name="feature."/>
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="feature"/> is <c>null</c>.</exception>
        public static IRectangle GetBoundingBox(GeoJsonFeature feature) {

            if (feature == null) throw new ArgumentNullException(nameof(feature));

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