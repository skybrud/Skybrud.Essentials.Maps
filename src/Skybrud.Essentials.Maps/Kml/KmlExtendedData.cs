using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml {

    public class KmlExtendedData {

        public Dictionary<string, string> SchemaData { get; }

        protected KmlExtendedData(XElement xml, XmlNamespaceManager namespaces) {
            
            Dictionary<string, string> temp = new();

            foreach (XElement sd in xml.GetElements("kml:SchemaData/kml:SimpleData", namespaces)) {
                string name = sd.GetAttributeValue("name");
                string value = sd.Value;
                temp.Add(name, value);
            }

            SchemaData = temp;

        }

        public static KmlExtendedData Parse(XElement xml) {

            XmlNameTable table = new NameTable();
            XmlNamespaceManager namespaces = new(table);
            namespaces.AddNamespace("kml", "http://www.opengis.net/kml/2.2");

            return new KmlExtendedData(xml, namespaces);

        }

        public static KmlExtendedData Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlExtendedData(xml, namespaces);
        }

    }

}