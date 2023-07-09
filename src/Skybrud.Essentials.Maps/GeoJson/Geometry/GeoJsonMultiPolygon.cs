using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.GeoJson.Json;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>MultiPolygon</strong> geometry.
    /// </summary>
    [JsonConverter(typeof(GeoJsonConverter))]
    public class GeoJsonMultiPolygon : GeoJsonGeometry, IGeoJsonShape, IEnumerable<GeoJsonPolygon> {

        private readonly List<GeoJsonPolygon> _polygons;

        #region Properties

        /// <summary>
        /// Gets the amount of <strong>Polygon</strong> geometries in the this <strong>MultiPolygon</strong>.
        /// </summary>
        public int Count => _polygons.Count;

        /// <summary>
        /// Returns the <strong>Polygon</strong> geometry at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the point to retrieve.</param>
        /// <returns>The <see cref="GeoJsonLineString"/> at the specified <paramref name="index"/>.</returns>
        public GeoJsonPolygon this[int index] => _polygons.ElementAt(index);

        /// <summary>
        /// Returns a four-dimensional array representing the coordinates of the <strong>MultiPolygon</strong>.
        /// </summary>
        public double[][][][] Coordinates => _polygons.Select(x => x.Coordinates).ToArray();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty <strong>MultiPolygon</strong> geometry.
        /// </summary>
        public GeoJsonMultiPolygon() : base(GeoJsonType.MultiPolygon) {
            _polygons = new List<GeoJsonPolygon>();
        }

        /// <summary>
        /// Initializes a new <strong>MultiPolygon</strong> geometry with the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates of the <strong>MultiPolygon</strong> geometry.</param>
        public GeoJsonMultiPolygon(double[][][][] coordinates) : base(GeoJsonType.MultiPolygon) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            _polygons = coordinates.Select(x => new GeoJsonPolygon(x)).ToList();
        }

        /// <summary>
        /// Initializes a new <strong>MultiPolygon</strong> geometry based on the specified collection of <paramref name="polygons"/>.
        /// </summary>
        /// <param name="polygons">A collection of <see cref="IPolygon"/>.</param>
        public GeoJsonMultiPolygon(IEnumerable<IPolygon> polygons) : base(GeoJsonType.MultiPolygon) {
            if (polygons == null) throw new ArgumentNullException(nameof(polygons));
            _polygons = new List<GeoJsonPolygon>(polygons.Select(x => new GeoJsonPolygon(x)));
        }

        /// <summary>
        /// Initializes a new <strong>MultiPolygon</strong> geometry based on the specified array of <paramref name="polygons"/>.
        /// </summary>
        /// <param name="polygons">An array of <strong>Polygon</strong> geometries.</param>
        public GeoJsonMultiPolygon(params GeoJsonPolygon[] polygons) : base(GeoJsonType.MultiPolygon) {
            if (polygons == null) throw new ArgumentNullException(nameof(polygons));
            _polygons = new List<GeoJsonPolygon>(polygons);
        }

        /// <summary>
        /// Initializes a new <strong>MultiPolygon</strong> geometry based on the specified collection of <paramref name="polygons"/>.
        /// </summary>
        /// <param name="polygons">A collection of <strong>GeoJsonPolygon</strong> geometries.</param>
        public GeoJsonMultiPolygon(IEnumerable<GeoJsonPolygon> polygons) : base(GeoJsonType.MultiPolygon) {
            if (polygons == null) throw new ArgumentNullException(nameof(polygons));
            _polygons = new List<GeoJsonPolygon>(polygons);
        }

        /// <summary>
        /// Initializes a new <strong>MultiPolygon</strong> geometry based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the <strong>MultiPolygon</strong> geometry.</param>
        public GeoJsonMultiPolygon(JObject json) : base(GeoJsonType.MultiPolygon) {

            if (json.GetValue("coordinates") is not JArray coordinates) throw new GeoJsonParseException("Unable to parse MultiPolygon. \"coordinates\" is not an instance of JArray.", json);

            try {

                // Convert the JArray to a four dimensional array
                double[][][][] array = coordinates.ToObject<double[][][][]>();

                // Parse the individual line strings
                _polygons = array.Select(x => new GeoJsonPolygon(x)).ToList();

            } catch (Exception ex)  {

                throw new GeoJsonParseException("Unable to parse \"coordinates\" of MultiLineString.", json, ex);

            }

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon to be added.</param>
        public void Add(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            _polygons.Add(new GeoJsonPolygon(polygon));
        }

        /// <summary>
        /// Adds the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon to be added.</param>
        public void Add(GeoJsonPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            _polygons.Add(polygon);
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="polygons"/> to the end of this <strong>MultiPolygon</strong> geometry.
        /// </summary>
        /// <param name="polygons">A collection of polygons to be added.</param>
        public void AddRange(IEnumerable<IPolygon> polygons) {
            if (polygons == null) throw new ArgumentNullException(nameof(polygons));
            _polygons.AddRange(polygons.Select(x => new GeoJsonPolygon(x)));
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="polygons"/> to the end of this <strong>MultiPolygon</strong> geometry.
        /// </summary>
        /// <param name="polygons">A collection of polygons to be added.</param>
        public void AddRange(IEnumerable<GeoJsonPolygon> polygons) {
            if (polygons == null) throw new ArgumentNullException(nameof(polygons));
            _polygons.AddRange(polygons);
        }

        /// <summary>
        /// Removes the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon to be removed.</param>
        public void Remove(GeoJsonPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            _polygons.Remove(polygon);
        }

        /// <summary>
        /// Removes all polygons.
        /// </summary>
        public void Clear() {
            _polygons.Clear();
        }

        /// <summary>
        /// Returns an instance of <see cref="IMultiPolygon"/> representing this GeoJSON multi polygon.
        /// </summary>
        /// <returns>An instance of <see cref="IMultiPolygon"/>.</returns>
        public IMultiPolygon ToMultiPolygon() {
            return new MultiPolygon(_polygons.Select(x => x.ToPolygon()));
        }

        /// <inheritdoc />
        public IShape ToShape() {
            return ToMultiPolygon();
        }

        /// <inheritdoc />
        public override IGeometry ToGeometry() {
            return ToMultiPolygon();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the underlying <see cref="List{GeoJsonPolygon}"/>.
        /// </summary>
        /// <returns>A <see cref="List{GeoJsonPolygon}.Enumerator"/> for the interal <see cref="List{GeoJsonPolygon}"/>.</returns>
        public IEnumerator<GeoJsonPolygon> GetEnumerator() {
            return _polygons.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonMultiPolygon"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public static new GeoJsonMultiPolygon Parse(string json) {
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonMultiPolygon"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public static new GeoJsonMultiPolygon Parse(JObject json) {
            return json == null ? null : new GeoJsonMultiPolygon(json);
        }

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonMultiPolygon"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiPolygon"/>.</returns>
        public static new GeoJsonMultiPolygon Load(string path) {
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}