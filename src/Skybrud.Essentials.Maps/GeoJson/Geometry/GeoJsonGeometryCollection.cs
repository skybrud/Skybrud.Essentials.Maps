using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a collection of other GeoJSON geometries.
    /// </summary>
    public class GeoJsonGeometryCollection : GeoJsonGeometry, IReadOnlyList<GeoJsonGeometry> {

        #region Properties

        /// <summary>
        /// Gets the number of geometries in this line string.
        /// </summary>
        public int Count => Geometries.Count;

        /// <summary>
        /// Gets the <see cref="GeoJsonGeometry"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="GeoJsonGeometry"/> at the specified index.</returns>
        public GeoJsonGeometry this[int index] => Geometries[index];

        /// <summary>
        /// Gets or sets the list of geometries in this collection.
        /// </summary>
        [JsonProperty("geometries")]
        public List<GeoJsonGeometry> Geometries { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty instance.
        /// </summary>
        public GeoJsonGeometryCollection() : base(GeoJsonType.GeometryCollection) {
            Geometries = new List<GeoJsonGeometry>();
        }

        /// <summary>
        /// Initializes a new instance with the specified <paramref name="geometries"/>.
        /// </summary>
        /// <param name="geometries">An array with the geometries the collection should initially be based on.</param>
        public GeoJsonGeometryCollection(IEnumerable<GeoJsonGeometry> geometries) : base(GeoJsonType.GeometryCollection) {
            Geometries = geometries?.ToList() ?? new List<GeoJsonGeometry>();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/>.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the geometry collection.</param>
        protected GeoJsonGeometryCollection(JObject json) : base(GeoJsonType.GeometryCollection) {
            Geometries = json.GetArrayItems("geometries", GeoJsonGeometry.Parse).ToList();
        }

        #endregion

        #region Member methods

        public IEnumerator<GeoJsonGeometry> GetEnumerator() {
            return Geometries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public override IGeometry ToGeometry() {
            throw new GeoJsonException($"The Geometry namespace does not have an equivalent for {nameof(GeoJsonGeometryCollection)}.");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonGeometryCollection"/>.
        /// </summary>
        /// <param name="json">The JSON string to be parsed.</param>
        /// <returns>An instance of <see cref="GeoJsonGeometryCollection"/>.</returns>
        public static new GeoJsonGeometryCollection Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonGeometryCollection"/>.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GeoJsonGeometryCollection"/>.</returns>
        public static new GeoJsonGeometryCollection Parse(JObject json) {
            return json == null ? null : new GeoJsonGeometryCollection(json);
        }

        #endregion

    }

}