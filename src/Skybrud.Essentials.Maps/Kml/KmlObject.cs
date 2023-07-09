using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Constants;
using Skybrud.Essentials.Maps.Kml.Parsing;
using Skybrud.Essentials.Strings;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml {

    public abstract class KmlObject {

        protected static XNamespace Namespace => Kml;

        protected static readonly XNamespace Kml = KmlConstants.DefaultNamespace;

        protected static IXmlNamespaceResolver Namespaces => KmlNamespaceResolver.Instance;

        public virtual XElement ToXElement() {
            return NewXElement();
        }

        protected virtual XElement NewXElement() {
            return new XElement(Kml + GetType().Name.Substring(3));
        }

        protected virtual XElement NewXElement(string name) {
            return new XElement(Kml + name);
        }

        protected XElement NewXElement(string name, string value) {
            return new XElement(Kml + name, value);
        }

        protected XElement NewXElement(string name, object value) {
            return new XElement(Kml + name, string.Format(CultureInfo.InvariantCulture, "{0}", value));
        }

        protected XElement NewXElement(string name, XCData value) {
            return new XElement(Kml + name, value);
        }

        protected XElement NewXElement(string name, XText value) {
            return new XElement(Kml + name, value);
        }

        protected XElement NewXElement(string name, XElement child) {
            return new XElement(Kml + name, child);
        }

        protected int GetInt32(XElement element, string name, IXmlNamespaceResolver namespaces, int fallback) {
            XElement? child = element.GetElement(name, namespaces);
            return StringUtils.ParseInt32(child?.Value, fallback);
        }

        protected float GetFloat(XElement element, string name, IXmlNamespaceResolver namespaces, float fallback) {
            XElement? child = element.GetElement(name, namespaces);
            return StringUtils.ParseFloat(child?.Value, fallback);
        }

        protected bool GetBoolean(XElement element, string name, IXmlNamespaceResolver namespaces, bool fallback) {
            XElement? child = element.GetElement(name, namespaces);
            return StringUtils.ParseBoolean(child?.Value, fallback);
        }

    }

}