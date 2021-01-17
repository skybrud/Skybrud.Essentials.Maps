using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>MultiLineString</strong> geometry.
    /// </summary>
    public class GeoJsonMultiLineString : GeoJsonGeometry, IGeoJsonLine {

        private readonly List<GeoJsonLineString> _lineStrings;

        #region Properties

        /// <summary>
        /// Gets a three dimensional array representing the coordinates of this <strong>MultiLineString</strong>.
        /// </summary>
        [JsonProperty("coordinates")]
        public double[][][] Coordinates => (
            from line in _lineStrings
            select line.Coordinates.Select(x => x.ToArray()).ToArray()
        ).ToArray();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty instance.
        /// </summary>
        public GeoJsonMultiLineString() : base(GeoJsonType.MultiLineString) {
            _lineStrings = new List<GeoJsonLineString>();
        }

        /// <summary>
        /// Initializes a instance with the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates of the <strong>MultiLineString</strong> geometry.</param>
        public GeoJsonMultiLineString(double[][][] coordinates) : base(GeoJsonType.MultiLineString) {
            _lineStrings = new List<GeoJsonLineString>(coordinates.Select(x => new GeoJsonLineString(x)));
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the <strong>MultiLineString</strong> geometry.</param>
        public GeoJsonMultiLineString(JObject json) : base(GeoJsonType.MultiLineString) {
            
            JArray coordinates = json.GetValue("coordinates") as JArray;
            if (coordinates == null) throw new GeoJsonParseException("Unable to parse MultiLineString. \"coordinates\" is not an instance of JArray.", json);

            try {

                // Convert the JArray to a three dimensional array
                double [][][] array = coordinates.ToObject<double[][][]>();

                // Parse the individual line strings
                _lineStrings = array.Select(x => new GeoJsonLineString(x)).ToList();

            } catch (Exception ex)  {
                
                throw new GeoJsonParseException("Unable to parse \"coordinates\" of MultiLineString.", json, ex);

            }


        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns an instance of <see cref="IMultiLineString"/> representing this GeoJSON <strong>MultiLineString</strong>.
        /// </summary>
        /// <returns>An instance of <see cref="IMultiPolygon"/>.</returns>
        public IMultiLineString ToMultiLineString() {
            return new MultiLineString(_lineStrings.Select(x => x.ToLineString()));
        }

        /// <inheritdoc />
        public ILineBase ToLine()  {
            return ToMultiLineString();
        }

        /// <inheritdoc />
        public override IGeometry ToGeometry() {
            return ToMultiLineString();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonMultiLineString"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiLineString"/>.</returns>
        public new static GeoJsonMultiLineString Parse(string json) {
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonMultiLineString"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiLineString"/>.</returns>
        public new static GeoJsonMultiLineString Parse(JObject json) {
            return json == null ? null : new GeoJsonMultiLineString(json);
        }

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonMultiLineString"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiLineString"/>.</returns>
        public new static GeoJsonMultiLineString Load(string path) {
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}