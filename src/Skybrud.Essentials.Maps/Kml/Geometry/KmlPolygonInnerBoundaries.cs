using System;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;

namespace Skybrud.Essentials.Maps.Kml.Geometry;

public class KmlPolygonInnerBoundaries : KmlPolygonBoundaries {

    #region Constructors

    public KmlPolygonInnerBoundaries() { }

    public KmlPolygonInnerBoundaries(params KmlPointCoordinates[] coordinates) : base(coordinates) { }

    public KmlPolygonInnerBoundaries(double[][] array) : base(array) { }

    protected KmlPolygonInnerBoundaries(XElement xml, IXmlNamespaceResolver namespaces) : base(xml, namespaces) { }

    #endregion

    #region Member methods

    public override XElement ToXElement() {
        if (LinearRing == null) throw new PropertyNotSetException(nameof(LinearRing));
        return NewXElement("innerBoundaryIs", LinearRing.ToXElement());
    }

    #endregion

    #region Static methods

    public static KmlPolygonInnerBoundaries Parse(XElement xml) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlPolygonInnerBoundaries(xml, Namespaces);
    }

    public static KmlPolygonInnerBoundaries Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
        if (xml is null) throw new ArgumentNullException(nameof(xml));
        return new KmlPolygonInnerBoundaries(xml, namespaces ?? Namespaces);
    }

    #endregion

}