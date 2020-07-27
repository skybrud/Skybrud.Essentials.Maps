using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Json;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Base class with common logic for the classes representing the geometries defined by the GeoJSON specification.
    /// </summary>
    [JsonConverter(typeof(GeoJsonReadConverter))]
    public abstract class GeoJsonGeometry : GeoJsonObject, IGeoJsonGeometry {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the shape.</param>
        protected GeoJsonGeometry(GeoJsonType type) : base(type) { }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public abstract IGeometry ToGeometry();

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonGeometry"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonGeometry"/>.</returns>
        public static GeoJsonGeometry Parse(string json) {
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonGeometry"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonGeometry"/>.</returns>
        public static GeoJsonGeometry Parse(JObject json) {

            if (json == null) return null;

            string type = json.GetString("type");

            switch (type) {

                case "Point":
                    return GeoJsonPoint.Parse(json);

                case "LineString":
                    return GeoJsonLineString.Parse(json);

                case "Polygon":
                    return GeoJsonPolygon.Parse(json);

                case "MultiPoint":
                    return GeoJsonMultiPoint.Parse(json);

                case "MultiLineString":
                    throw new NotImplementedException();
                //return GeoJsonMultiLineString.Parse(obj);

                case "MultiPolygon":
                    return GeoJsonMultiPolygon.Parse(json);

                case "GeometryCollection":
                    return GeoJsonGeometryCollection.Parse(json);

                default:
                    throw new Exception("Unsupported type " + type);

            }

        }

        /// <summary>
        /// Loads and parses the geometry at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonGeometry"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonGeometry"/>.</returns>
        public static GeoJsonGeometry Load(string path) {
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}