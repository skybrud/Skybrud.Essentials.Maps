using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;
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

        public KmlPolygon(KmlPolygonOuterBoundaries outer, KmlPolygonInnerBoundaries[] inner) {
            OuterBoundaries = outer;
            InnerBoundaries = inner?.ToList() ?? new List<KmlPolygonInnerBoundaries>();
        }

        protected KmlPolygon(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            OuterBoundaries = xml.GetElement("kml:outerBoundaryIs", namespaces, KmlPolygonOuterBoundaries.Parse);
            InnerBoundaries = xml.GetElements("kml:innerBoundaryIs", namespaces, KmlPolygonInnerBoundaries.Parse).ToList();
        }

        public override XElement ToXElement() {

            if (OuterBoundaries == null) throw new PropertyNotSetException(nameof(OuterBoundaries));

            XElement xml = base.ToXElement();

            xml.Add(OuterBoundaries.ToXElement());

            if (InnerBoundaries != null) {
                foreach (var inner in InnerBoundaries) {
                    xml.Add(inner.ToXElement());
                }
            }

            return xml;

        }

        public static KmlPolygon Parse(XElement xml) {
            return xml == null ? null : new KmlPolygon(xml, Namespaces);

        }

        public static KmlPolygon Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlPolygon(xml, namespaces);
        }

    }

}