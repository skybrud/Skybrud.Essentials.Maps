using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public class KmlPolygonInnerBoundaries : KmlPolygonBoundaries {

        #region Constructors

        public KmlPolygonInnerBoundaries() { }

        public KmlPolygonInnerBoundaries(params KmlPointCoordinates[] coordinates) : base(coordinates) { }

        public KmlPolygonInnerBoundaries(double[][] array) : base(array) { }

        protected KmlPolygonInnerBoundaries(XElement xml, XmlNamespaceManager manager) : base(xml, manager) { }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            if (LinearRing == null) throw new PropertyNotSetException(nameof(LinearRing));
            return NewXElement("innerBoundaryIs", LinearRing.ToXElement());
        }

        #endregion

        #region Static methods

        public static KmlPolygonInnerBoundaries Parse(XElement xml) {
            return new KmlPolygonInnerBoundaries(xml, Namespaces);
        }

        public static KmlPolygonInnerBoundaries Parse(XElement xml, XmlNamespaceManager manager) {
            return new KmlPolygonInnerBoundaries(xml, manager);
        }

        #endregion

    }

}