using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    /// <summary>
    /// Class representing a KML <c>&lt;LineString&gt;</c> element.
    /// </summary>
    public class KmlLineString : KmlGeometry, IEnumerable<KmlPointCoordinates> {

        private readonly List<KmlPointCoordinates> _list;

        #region Properties

        /// <summary>
        /// Gets the amount of coordinates within the element.
        /// </summary>
        public int Count => _list.Count;

        /// <summary>
        /// Gets the coordinates at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>An instance of <see cref="KmlPointCoordinates"/>.</returns>
        public KmlPointCoordinates this[int index] => _list.ElementAt(index);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;LineString&gt;</c> element.
        /// </summary>
        public KmlLineString() {
            _list = new List<KmlPointCoordinates>();
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;LineString&gt;</c> element based on the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">An array of coordinates that make up the element.</param>
        public KmlLineString(IEnumerable<KmlPointCoordinates>? coordinates) {
            _list = coordinates?.ToList() ?? new List<KmlPointCoordinates>();
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;LineString&gt;</c> element based on the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">An array of coordinates that make up the element.</param>
        public KmlLineString(params KmlPointCoordinates[]? coordinates) {
            _list = coordinates?.ToList() ?? new List<KmlPointCoordinates>();
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;LineString&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlLineString(XElement xml, IXmlNamespaceResolver namespaces) : base(xml) {
            string[] coordinates = xml.GetElementValue("kml:coordinates", namespaces).Split(new[] { " ", "\n", "\r", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            _list = coordinates.Select(x => new KmlPointCoordinates(StringUtils.ParseDoubleArray(x))).ToList();
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds a new point with the specified <paramref name="latitude"/> and <paramref name="longitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        public void Add(double latitude, double longitude) {
            _list.Add(new KmlPointCoordinates(latitude, longitude));
        }

        /// <summary>
        /// Adds a new point with the specified <paramref name="latitude"/>, <paramref name="longitude"/> and <paramref name="altitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <param name="altitude">The altitude of the point.</param>
        public void Add(double latitude, double longitude, double altitude) {
            _list.Add(new KmlPointCoordinates(latitude, longitude, altitude));
        }

        /// <summary>
        /// Adds the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates to be added.</param>
        public void Add(KmlPointCoordinates coordinates) {
            _list.Add(coordinates);
        }

        /// <inheritdoc />
        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            string coordinates = string.Join(" ", from p in _list select p.ToString());
            xml.Add(NewXElement("coordinates", coordinates));

            return xml;

        }

        /// <inheritdoc />
        public IEnumerator<KmlPointCoordinates> GetEnumerator() {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlLineString"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <returns>An instance of <see cref="KmlLineString"/>.</returns>
        public static KmlLineString Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlLineString(xml, Namespaces);

        }

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlLineString"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <param name="namespaces">The XML namespace.</param>
        /// <returns>An instance of <see cref="KmlLineString"/>.</returns>
        public static KmlLineString Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlLineString(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}