using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public abstract class KmlPolygonBoundaries : KmlObject {

        #region Properties

        public KmlLinearRing LinearRing { get; set; }

        #endregion

        #region Constructors

        protected KmlPolygonBoundaries() {
            LinearRing = new KmlLinearRing();
        }

        protected KmlPolygonBoundaries(params KmlPointCoordinates[] coordinates) {
            LinearRing = new KmlLinearRing(coordinates);
        }

        protected KmlPolygonBoundaries(IEnumerable<KmlPointCoordinates> coordinates) {
            LinearRing = new KmlLinearRing(coordinates);
        }

        protected KmlPolygonBoundaries(double[][] array) {
            LinearRing = new KmlLinearRing(array);
        }

        protected KmlPolygonBoundaries(XElement xml, XmlNamespaceManager namespaces) {
            LinearRing = xml.GetElement("kml:LinearRing", namespaces, KmlLinearRing.Parse);
        }

        #endregion

    }

}