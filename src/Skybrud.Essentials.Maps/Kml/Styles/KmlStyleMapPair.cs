using System;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public class KmlStyleMapPair : KmlObject {

        #region Properties

        /// <summary>
        /// Gets or sets the key that identifies the pair.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the style URL.
        /// </summary>
        public string StyleUrl { get; set; }

        #endregion

        #region Constructors

        public KmlStyleMapPair() { }

        protected KmlStyleMapPair(XElement xml, XmlNamespaceManager namespaces) {
            Key = xml.GetElementValue("kml:key", namespaces);
            StyleUrl = xml.GetElementValue("kml:styleUrl", namespaces);
        }

        #endregion

        #region Properties

        public override XElement ToXElement() {

            if (String.IsNullOrWhiteSpace(Key)) throw new PropertyNotSetException(nameof(Key), "The Key property of a <Pair> must have a value");
            if (String.IsNullOrWhiteSpace(StyleUrl)) throw new PropertyNotSetException(nameof(StyleUrl), "The StyleUrl property of a <Pair> must have a value");

            XElement xml = base.NewXElement("Pair");

            xml.Add(NewXElement("key", Key));
            xml.Add(NewXElement("styleUrl", StyleUrl));

            return xml;

        }

        #endregion

        #region Static methods
        
        public static KmlStyleMapPair Parse(XElement xml) {
            return xml == null ? null : new KmlStyleMapPair(xml, Namespaces);
        }

        public static KmlStyleMapPair Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlStyleMapPair(xml, namespaces);
        }

        #endregion

    }

}