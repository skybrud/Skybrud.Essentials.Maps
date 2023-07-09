using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;
using Skybrud.Essentials.Maps.Kml.Exceptions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#polygon</cref>
    /// </see>
    public class KmlPolygon : KmlGeometry {

        public KmlPolygonOuterBoundaries OuterBoundaries { get; set; }

        public List<KmlPolygonInnerBoundaries> InnerBoundaries { get; set; }

        public KmlPolygon() {
            OuterBoundaries = new KmlPolygonOuterBoundaries();
            InnerBoundaries = new List<KmlPolygonInnerBoundaries>();
        }

        public KmlPolygon(params KmlPointCoordinates[] outer) {
            OuterBoundaries = new KmlPolygonOuterBoundaries(outer);
            InnerBoundaries = new List<KmlPolygonInnerBoundaries>();
        }

        public KmlPolygon(IEnumerable<KmlPointCoordinates> outer) {
            OuterBoundaries = new KmlPolygonOuterBoundaries(outer);
            InnerBoundaries = new List<KmlPolygonInnerBoundaries>();
        }

        public KmlPolygon(KmlPolygonOuterBoundaries outer) {
            OuterBoundaries = outer;
            InnerBoundaries = new List<KmlPolygonInnerBoundaries>();
        }

        public KmlPolygon(KmlPolygonOuterBoundaries outer, KmlPolygonInnerBoundaries[]? inner) {
            OuterBoundaries = outer;
            InnerBoundaries = inner?.ToList() ?? new List<KmlPolygonInnerBoundaries>();
        }

        protected KmlPolygon(XElement xml, IXmlNamespaceResolver namespaces) : base(xml) {
            OuterBoundaries = xml.GetElement("kml:outerBoundaryIs", namespaces, x => KmlPolygonOuterBoundaries.Parse(x, namespaces))!;
            InnerBoundaries = xml.GetElements("kml:innerBoundaryIs", namespaces, KmlPolygonInnerBoundaries.Parse).ToList();
            if (OuterBoundaries is null) throw new KmlParseException($"Failed parsing 'kml:outerBoundaryIs' from '{xml.Name}'...");
        }

        public override XElement ToXElement() {

            if (OuterBoundaries == null) throw new PropertyNotSetException(nameof(OuterBoundaries));

            XElement xml = base.ToXElement();

            xml.Add(OuterBoundaries.ToXElement());

            foreach (KmlPolygonInnerBoundaries inner in InnerBoundaries) {
                xml.Add(inner.ToXElement());
            }

            return xml;

        }

        public static KmlPolygon Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPolygon(xml, Namespaces);
        }

        public static KmlPolygon Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPolygon(xml, namespaces ?? Namespaces);
        }

    }

}