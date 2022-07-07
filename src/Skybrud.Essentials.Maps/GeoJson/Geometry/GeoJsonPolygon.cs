using System;
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
    /// Class representing a GeoJSON <strong>Polygon</strong> geometry.
    /// </summary>
    [JsonConverter(typeof(GeoJsonConverter))]
    public class GeoJsonPolygon : GeoJsonGeometry {
        
        #region Properties

        /// <summary>
        /// Gets a list representing the outer coordinates of the polygon.
        /// </summary>
        public List<GeoJsonCoordinates> Outer { get; }

        /// <summary>
        /// Gets a list representing the inner coordinates of the polygon.
        /// </summary>
        public List<List<GeoJsonCoordinates>> Inner { get; }

        /// <summary>
        /// Returns a three-dimensional array representing the outer and inner coordinates of the <strong>Polygon</strong>.
        /// </summary>
        public double[][][] Coordinates {

            get {

                List<double[][]> list = new() {
                    Outer.Select(x => x.ToArray()).ToArray()
                };
                
                list.AddRange(Inner.Select(inner => inner.Select(x => x.ToArray()).ToArray()));

                return list.ToArray();

            }

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty <strong>Polygon</strong> geometry.
        /// </summary>
        public GeoJsonPolygon() : base(GeoJsonType.Polygon)  {
            Outer = new List<GeoJsonCoordinates>();
            Inner = new List<List<GeoJsonCoordinates>>();
        }

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="outer"/> coordinates.
        /// </summary>
        /// <param name="outer">A two-dimensional array representing the outer polygon.</param>
        public GeoJsonPolygon(double[][] outer) : base(GeoJsonType.Polygon) {

            if (outer == null) throw new ArgumentNullException(nameof(outer));
            
            Outer = outer
                .Select(x => new GeoJsonCoordinates(x))
                .ToList();

            Inner = new List<List<GeoJsonCoordinates>>();

        }

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">A three-dimensional array representing the outer polygon as well as any inner polygons.</param>
        public GeoJsonPolygon(double[][][] coordinates) : base(GeoJsonType.Polygon) {

            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));

            if (coordinates.Length == 0) {
                Outer = new List<GeoJsonCoordinates>();
                Inner = new List<List<GeoJsonCoordinates>>();
                return;
            }

            Outer = coordinates
                .ElementAt(0)
                .Select(x => new GeoJsonCoordinates(x))
                .ToList();

            Inner = coordinates
                .Skip(1)
                .Select(x => x.Select(y => new GeoJsonCoordinates(y)).ToList())
                .ToList();

        }

        /// <summary>
        /// Initializes a new instance from the specified array of <paramref name="outer"/> coordinates.
        /// </summary>
        /// <param name="outer">The outer coordinates.</param>
        public GeoJsonPolygon(IPoint[] outer) : base(GeoJsonType.Polygon) {
            
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            
            Outer = outer
                .Select(x => new GeoJsonCoordinates(x))
                .ToList();

            Inner = new List<List<GeoJsonCoordinates>>();

        }
        
        /// <summary>
        /// Initializes a new instance from the specified array of <see cref="IPoint"/> <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public GeoJsonPolygon(IPoint[][] coordinates) : base(GeoJsonType.Polygon) {
            
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));

            if (coordinates.Length == 0) {
                Outer = new List<GeoJsonCoordinates>();
                Inner = new List<List<GeoJsonCoordinates>>();
                return;
            }

            Outer = coordinates
                .ElementAt(0)
                .Select(x => new GeoJsonCoordinates(x))
                .ToList();

            Inner = coordinates
                .Skip(1)
                .Select(x => x.Select(y => new GeoJsonCoordinates(y)).ToList())
                .ToList();

        }

        /// <summary>
        /// Initializes a new <strong>Polygon</strong> geometry based on the specified <paramref name="outer"/> coordinates.
        /// </summary>
        /// <param name="outer">The outher coordinates of the polygon.</param>
        public GeoJsonPolygon(params GeoJsonCoordinates[] outer) : base(GeoJsonType.Polygon) {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            Outer = outer.ToList();
            Inner = new List<List<GeoJsonCoordinates>>();
        }

        /// <summary>
        /// Initializes a new <strong>Polygon</strong> geometry based on the specified <paramref name="outer"/> coordinates.
        /// </summary>
        /// <param name="outer">The outher coordinates of the polygon.</param>
        public GeoJsonPolygon(IEnumerable<GeoJsonCoordinates> outer) : base(GeoJsonType.Polygon) {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            Outer = outer.ToList();
            Inner = new List<List<GeoJsonCoordinates>>();
        }
        
        /// <summary>
        /// Initializes a new instance from the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public GeoJsonPolygon(IPolygon polygon) : base(GeoJsonType.Polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            Outer = polygon.Outer.Select(x => new GeoJsonCoordinates(x)).ToList();
            Inner = polygon.Inner.Select(x => x.Select(y => new GeoJsonCoordinates(y)).ToList()).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        public GeoJsonPolygon(JObject json) : base(GeoJsonType.Polygon) {

            JArray coordinates = json.GetValue("coordinates") as JArray;
            if (coordinates == null) throw new GeoJsonParseException("Unable to parse Polygon. \"coordinates\" is not an instance of JArray.", json);

            try {

                // Convert the JArray to a three dimensional array
                double[][][] array = coordinates.ToObject<double[][][]>();
                
                if (array.Length == 0) {
                    Outer = new List<GeoJsonCoordinates>();
                    Inner = new List<List<GeoJsonCoordinates>>();
                    return;
                }

                Outer = array
                    .ElementAt(0)
                    .Select(x => new GeoJsonCoordinates(x))
                    .ToList();

                Inner = array
                    .Skip(1)
                    .Select(x => x.Select(y => new GeoJsonCoordinates(y)).ToList())
                    .ToList();

            } catch (Exception ex)  {
                
                throw new GeoJsonParseException("Unable to parse \"coordinates\" of MultiLineString.", json, ex);

            }

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns an instance of <see cref="IPolygon"/> representing this GeoJSON polygon.
        /// </summary>
        /// <returns>An instance of <see cref="IPolygon"/>.</returns>
        public IPolygon ToPolygon() {

            IEnumerable<IPoint> outer = Outer.Select(x => x.ToPoint());
            IEnumerable<IEnumerable<IPoint>> inner = Inner.Select(x => x.Select(y => y.ToPoint()));

            return new Polygon(outer, inner);

        }

        /// <inheritdoc />
        public override IGeometry ToGeometry() {
            return ToPolygon();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public static new GeoJsonPolygon Parse(string json) {
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public static new GeoJsonPolygon Parse(JObject json) {
            return json == null ? null : new GeoJsonPolygon(json);
        }

        /// <summary>
        /// Loads and parses the polygon at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public static new GeoJsonPolygon Load(string path) {
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}