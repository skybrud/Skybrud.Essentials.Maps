using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Exceptions;
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

        protected KmlPolygonBoundaries(XElement xml, IXmlNamespaceResolver namespaces) {
            LinearRing = xml.GetElement("kml:LinearRing", namespaces, x => KmlLinearRing.Parse(x, namespaces))!;
            if (LinearRing is null) throw new KmlParseException($"Failed parsing 'kml:LinearRing' from '{xml.Name}'...");
        }

        #endregion

    }

}