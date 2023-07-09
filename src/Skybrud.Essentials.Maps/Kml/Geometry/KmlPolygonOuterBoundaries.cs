using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public class KmlPolygonOuterBoundaries : KmlPolygonBoundaries {

        #region Constructors

        public KmlPolygonOuterBoundaries() { }

        public KmlPolygonOuterBoundaries(params KmlPointCoordinates[] coordinates) : base(coordinates) { }

        public KmlPolygonOuterBoundaries(IEnumerable<KmlPointCoordinates> coordinates) : base(coordinates) { }

        public KmlPolygonOuterBoundaries(double[][] array) : base(array) { }

        protected KmlPolygonOuterBoundaries(XElement xml, IXmlNamespaceResolver manager) : base(xml, manager) { }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            if (LinearRing == null) throw new PropertyNotSetException(nameof(KmlPolygonBoundaries.LinearRing));
            return NewXElement("outerBoundaryIs", LinearRing.ToXElement());
        }

        #endregion

        #region Static methods

        public static KmlPolygonOuterBoundaries Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPolygonOuterBoundaries(xml, Namespaces);
        }

        public static KmlPolygonOuterBoundaries Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlPolygonOuterBoundaries(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}