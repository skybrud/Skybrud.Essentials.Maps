using System;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public class KmlIcon : KmlObject {

        #region Properties

        public string Href { get; set; }

        #endregion

        #region Constructors

        public KmlIcon() { }

        public KmlIcon(string href) {
            Href = href;
        }

        protected KmlIcon(XElement xml, XmlNamespaceManager namespaces) {
            Href = xml.GetElementValue("kml:href", namespaces);
        }

        #endregion

        #region Mmember methods

        public override XElement ToXElement() {
            XElement xml = NewXElement("Icon");
            xml.Add(NewXElement("href", Href ?? String.Empty));
            return xml;
        }

        #endregion

        #region Static methods

        public static KmlIcon Parse(XElement xml) {
            return new KmlIcon(xml, Namespaces);

        }

        public static KmlIcon Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlIcon(xml, namespaces);
        }

        #endregion

    }

}