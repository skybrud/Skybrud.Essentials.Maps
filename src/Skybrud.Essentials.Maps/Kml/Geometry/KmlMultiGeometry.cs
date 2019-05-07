using System;
using System.Xml;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public class KmlMultiGeometry : KmlGeometry {

        public KmlGeometry[] Children { get; }

        protected KmlMultiGeometry(XElement xml, XmlNamespaceManager namespaces) {
            Children = KmlUtils.ParseGeometryChildren(xml, namespaces);
        }

        public override XElement ToXElement() {
            throw new NotImplementedException();
        }

        public static KmlMultiGeometry Parse(XElement xml) {

            XmlNameTable table = new NameTable();
            XmlNamespaceManager namespaces = new XmlNamespaceManager(table);
            namespaces.AddNamespace("kml", "http://www.opengis.net/kml/2.2");

            return new KmlMultiGeometry(xml, namespaces);

        }

        public static KmlMultiGeometry Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlMultiGeometry(xml, namespaces);
        }

    }

}