using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.GeoJson.Json;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>MultiLineString</strong> geometry.
    /// </summary>
    [JsonConverter(typeof(GeoJsonConverter))]
    public class GeoJsonMultiLineString : GeoJsonGeometry, IGeoJsonLine, IEnumerable<GeoJsonLineString> {

        private readonly List<GeoJsonLineString> _lineStrings;

        #region Properties

        /// <summary>
        /// Gets the amount of <strong>LineString</strong> geometries in the this <strong>MultiLineString</strong>.
        /// </summary>
        public int Count => _lineStrings.Count;

        /// <summary>
        /// Returns the <strong>LineString</strong> geometry at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the point to retrieve.</param>
        /// <returns>The <see cref="GeoJsonLineString"/> at the specified <paramref name="index"/>.</returns>
        public GeoJsonLineString this[int index] => _lineStrings.ElementAt(index);

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
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            _lineStrings = new List<GeoJsonLineString>(coordinates.Select(x => new GeoJsonLineString(x)));
        }

        /// <summary>
        /// Initializes a instance based on the specified collection of <paramref name="lineStrings"/>.
        /// </summary>
        /// <param name="lineStrings">A collection of <see cref="LineString"/>.</param>
        public GeoJsonMultiLineString(IEnumerable<LineString> lineStrings) : base(GeoJsonType.MultiLineString) {
            if (lineStrings == null) throw new ArgumentNullException(nameof(lineStrings));
            _lineStrings = new List<GeoJsonLineString>(lineStrings.Select(x => new GeoJsonLineString(x)));
        }

        /// <summary>
        /// Initializes a instance based on the specified array of <paramref name="lineStrings"/>.
        /// </summary>
        /// <param name="lineStrings">An array of <strong>LineString</strong> geometries.</param>
        public GeoJsonMultiLineString(params GeoJsonLineString[] lineStrings) : base(GeoJsonType.MultiLineString) {
            if (lineStrings == null) throw new ArgumentNullException(nameof(lineStrings));
            _lineStrings = new List<GeoJsonLineString>(lineStrings);
        }

        /// <summary>
        /// Initializes a instance based on the specified collection of <paramref name="lineStrings"/>.
        /// </summary>
        /// <param name="lineStrings">A collection of <strong>LineString</strong> geometries.</param>
        public GeoJsonMultiLineString(IEnumerable<GeoJsonLineString> lineStrings) : base(GeoJsonType.MultiLineString) {
            if (lineStrings == null) throw new ArgumentNullException(nameof(lineStrings));
            _lineStrings = new List<GeoJsonLineString>(lineStrings);
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
        /// Adds the specified <paramref name="lineString"/>. 
        /// </summary>
        /// <param name="lineString">The line string to be added.</param>
        public void Add(ILineString lineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            _lineStrings.Add(new GeoJsonLineString(lineString));
        }

        /// <summary>
        /// Adds the specified <paramref name="lineString"/>. 
        /// </summary>
        /// <param name="lineString">The line string to be added.</param>
        public void Add(GeoJsonLineString lineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            _lineStrings.Add(lineString);
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="lineStrings"/> to the end of this <strong>MultiLineString</strong> geometry.
        /// </summary>
        /// <param name="lineStrings">A collection of line strings to be added.</param>
        public void AddRange(IEnumerable<ILineString> lineStrings) {
            if (lineStrings == null) throw new ArgumentNullException(nameof(lineStrings));
            _lineStrings.AddRange(lineStrings.Select(x => new GeoJsonLineString(x)));
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="lineStrings"/> to the end of this <strong>MultiLineString</strong> geometry.
        /// </summary>
        /// <param name="lineStrings">A collection of line strings to be added.</param>
        public void AddRange(IEnumerable<GeoJsonLineString> lineStrings) {
            if (lineStrings == null) throw new ArgumentNullException(nameof(lineStrings));
            _lineStrings.AddRange(lineStrings);
        }

        /// <summary>
        /// Removes the specified <paramref name="lineString"/>.
        /// </summary>
        /// <param name="lineString">The line string to be removed.</param>
        public void Remove(GeoJsonLineString lineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            _lineStrings.Remove(lineString);
        }

        /// <summary>
        /// Removes all line strings.
        /// </summary>
        public void Clear() {
            _lineStrings.Clear();
        }

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

        /// <summary>
        /// Returns an enumerator that iterates through the underlying <see cref="List{GeoJsonLineString}"/>.
        /// </summary>
        /// <returns>A <see cref="List{GeoJsonLineString}.Enumerator"/> for the interal <see cref="List{GeoJsonLineString}"/>.</returns>
        public IEnumerator<GeoJsonLineString> GetEnumerator() {
            return _lineStrings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
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