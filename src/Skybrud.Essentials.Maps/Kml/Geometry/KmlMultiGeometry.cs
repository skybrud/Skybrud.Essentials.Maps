using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    /// <summary>
    /// Class representing a KML <c>&lt;MultiGeometry&gt;</c> element.
    /// </summary>
    public class KmlMultiGeometry : KmlGeometry {

        #region Properties

        /// <summary>
        /// Gets a reference to the list of geometries within this <c>&lt;MultiGeometry&gt;</c> element.
        /// </summary>
        public List<KmlGeometry> Children { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;MultiGeometry&gt;</c> element.
        /// </summary>
        public KmlMultiGeometry() {
            Children = new List<KmlGeometry>();
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;MultiGeometry&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlMultiGeometry(XElement xml, XmlNamespaceManager namespaces) {
            Children = KmlUtils.ParseGeometryChildren(xml, namespaces).ToList();
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override XElement ToXElement() {
            throw new NotImplementedException();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlMultiGeometry"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <returns>An instance of <see cref="KmlMultiGeometry"/>.</returns>
        public static KmlMultiGeometry Parse(XElement xml) {

            XmlNameTable table = new NameTable();
            XmlNamespaceManager namespaces = new(table);
            namespaces.AddNamespace("kml", "http://www.opengis.net/kml/2.2");

            return new KmlMultiGeometry(xml, namespaces);

        }

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlMultiGeometry"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the document.</param>
        /// <param name="namespaces">The XML namespace.</param>
        /// <returns>An instance of <see cref="KmlMultiGeometry"/>.</returns>
        public static KmlMultiGeometry Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlMultiGeometry(xml, namespaces);
        }

        #endregion

    }

}