using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Json;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    [JsonConverter(typeof(GeoJsonShapeJsonConverter))]
    public abstract class GeoJsonGeometry : GeoJsonObject {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the shape.</param>
        protected GeoJsonGeometry(GeoJsonType type) : base(type) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonGeometry"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonGeometry"/>.</returns>
        public static GeoJsonGeometry Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="obj"/> object into an instance of <see cref="GeoJsonGeometry"/>.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonGeometry"/>.</returns>
        public static GeoJsonGeometry Parse(JObject obj) {

            if (obj == null) return null;

            string type = obj.GetString("type");

            switch (type) {

                case "Point":
                    return GeoJsonPoint.Parse(obj);

                case "LineString":
                    return GeoJsonLineString.Parse(obj);

                case "Polygon":
                    return GeoJsonPolygon.Parse(obj);

                case "MultiPoint":
                    throw new NotImplementedException();
                    //return GeoJsonMultiPoint.Parse(obj);

                case "MultiLineString":
                    throw new NotImplementedException();
                //return GeoJsonMultiLineString.Parse(obj);

                case "MultiPolygon":
                    return GeoJsonMultiPolygon.Parse(obj);

                case "GeometryCollection":
                    return GeoJsonGeometryCollection.Parse(obj);

                default:
                    throw new Exception("Unsupported type " + type);


            }

        }

        #endregion

    }

}