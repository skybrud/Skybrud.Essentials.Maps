using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Maps.GeoJson.Json;
using Skybrud.Essentials.Maps.Geometry.Lines;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>LineString</strong> geometry.
    /// </summary>
    public class GeoJsonLineString : GeoJsonGeometry {

        #region Properties

        /// <summary>
        /// Gets or sets the coordinates making up the line string.
        /// </summary>
        [JsonProperty("coordinates", Order = 100)]
        [JsonConverter(typeof(GeoJsonConverter))]
        public List<GeoJsonCoordinates> Coordinates { get; set; }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance with no initial coordinates.
        /// </summary>
        public GeoJsonLineString() : base(GeoJsonType.LineString) {
            Coordinates = new List<GeoJsonCoordinates>();
        }
        
        /// <summary>
        /// Initializes a new instance based on the specified array of <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">An array of coordinates.</param>
        public GeoJsonLineString(double[][] coordinates) : base(GeoJsonType.LineString) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            Coordinates = coordinates.Select(x => new GeoJsonCoordinates(x)).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line the new instance should be based on.</param>
        public GeoJsonLineString(ILine line) : base(GeoJsonType.LineString) {
            if (line == null) throw new ArgumentNullException(nameof(line));
            Coordinates = new List<GeoJsonCoordinates> {new GeoJsonCoordinates(line.A), new GeoJsonCoordinates(line.B)};
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="lineString"/>.
        /// </summary>
        /// <param name="lineString">The line string the new instance should be based on.</param>
        public GeoJsonLineString(ILineString lineString) : base(GeoJsonType.LineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            Coordinates = lineString.Points.Select(x => new GeoJsonCoordinates(x)).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the line string.</param>
        protected GeoJsonLineString(JObject obj) : base(GeoJsonType.LineString) {

            Coordinates = new List<GeoJsonCoordinates>();

            if (!(obj.GetValue("coordinates") is JArray array)) {
                return;
            }

            foreach (JToken token in array) {
                Coordinates.Add(new GeoJsonCoordinates(token.Select(x => x.Value<double>()).ToArray()));
            }

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns an instance of <see cref="ILineString"/> representing this GeoJSON line string.
        /// </summary>
        /// <returns>An instance of <see cref="ILineString"/>.</returns>
        public ILineString ToLineString() {
            return new LineString(Coordinates.Select(x => x.ToPoint()));
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public new static GeoJsonLineString Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public new static GeoJsonLineString Parse(JObject json) {
            return json == null ? null : new GeoJsonLineString(json);
        }

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public static GeoJsonLineString Load(string path) {
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        #endregion

    }

}