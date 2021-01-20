﻿using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Geometry;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features {

    /// <summary>
    /// Class representing a KML <c>&lt;Placemark&gt;</c> element.
    /// </summary>
    public class KmlPlacemark : KmlFeature {

        #region Properties

        /// <summary>
        /// Gets or sets the extended data of the placemark.
        /// </summary>
        public KmlExtendedData ExtendedData { get; set; }

        /// <summary>
        /// Gets or sets the geometry of the placemark.
        /// </summary>
        public KmlGeometry Geometry { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;Placemark&gt;</c> element.
        /// </summary>
        public KmlPlacemark() { }

        /// <summary>
        /// Initializes a new KML <c>&lt;Placemark&gt;</c> element containing the specified <paramref name="geometry"/>..
        /// </summary>
        /// <param name="geometry">The geometry to be added.</param>
        public KmlPlacemark(KmlGeometry geometry) {
            Geometry = geometry;
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;Document&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        protected KmlPlacemark(XElement xml) : this(xml, Namespaces) { }

        /// <summary>
        /// Initializes a new KML <c>&lt;Document&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        /// <param name="namespaces">The XML namespace.</param>
        protected KmlPlacemark(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            ExtendedData = xml.GetElement("kml:ExtendedData", namespaces, KmlExtendedData.Parse);
            Geometry = KmlUtils.ParseGeometryChildren(xml, namespaces).FirstOrDefault();
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override XElement ToXElement() {
            
            XElement xml = base.ToXElement();

            if (Geometry != null) xml.Add(Geometry.ToXElement());

            return xml;

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlPlacemark"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the placemark.</param>
        /// <returns>An instance of <see cref="KmlPlacemark"/>.</returns>
        public static KmlPlacemark Parse(XElement xml) {
            return xml == null ? null : new KmlPlacemark(xml, Namespaces);
        }

        /// <summary>
        /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlPlacemark"/>.
        /// </summary>
        /// <param name="xml">The XML element representing the placemark.</param>
        /// <param name="namespaces">The XML namespace.</param>
        /// <returns>An instance of <see cref="KmlPlacemark"/>.</returns>
        public static KmlPlacemark Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlPlacemark(xml, namespaces);
        }

        #endregion

    }

}