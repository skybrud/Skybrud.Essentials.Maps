using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    public class GeoJsonPoint : GeoJsonGeometry {

        #region Properties

        [JsonIgnore]
        public double X { get; set; }

        [JsonIgnore]
        public double Y { get; set; }

        [JsonIgnore]
        public double Altitude { get; set; }

        [JsonProperty("coordinates", Order = 100)]
        public double[] Coordinates => Math.Abs(Altitude) < Double.Epsilon ? new[] {X, Y} : new[] {X, Y, Altitude};

        #endregion

        #region Constructors
        
        public GeoJsonPoint() : base(GeoJsonType.Point) { }
        
        public GeoJsonPoint(double x, double y) : base(GeoJsonType.Point) {
            X = x;
            Y = y;
        }
        
        public GeoJsonPoint(double x, double y, double altitude) : base(GeoJsonType.Point) {
            X = x;
            Y = y;
            Altitude = altitude;
        }
        
        public GeoJsonPoint(double[] coordinates) : base(GeoJsonType.Point) {
            X = coordinates[0];
            Y = coordinates[1];
            Altitude = coordinates.Length == 3 ? coordinates[2] : 0;
        }

        private GeoJsonPoint(JObject obj) : base(GeoJsonType.Point) {

            JArray array = obj.GetValue("coordinates") as JArray;
            if (array == null) return;

            double[] coordinates = array.ToObject<double[]>();

            X = coordinates[0];
            Y = coordinates[1];
            Altitude = coordinates.Length == 3 ? coordinates[2] : 0;

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonPoint"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonPoint"/>.</returns>
        public new static GeoJsonPoint Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonPoint"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPoint"/>.</returns>
        public new static GeoJsonPoint Parse(JObject json) {
            return json == null ? null : new GeoJsonPoint(json);
        }

        #endregion

    }

}