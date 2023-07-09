using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Common;
using Skybrud.Essentials.Maps.Kml.Exceptions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public class KmlStyleMapPair : KmlObject {

        #region Properties

        /// <summary>
        /// Gets or sets the key that identifies the pair.
        /// </summary>
#if NET7_0_OR_GREATER
        public required string Key { get; set; }
#else
        public string Key { get; set; }
#endif

        /// <summary>
        /// Gets or sets the style URL.
        /// </summary>
#if NET7_0_OR_GREATER
        public required string StyleUrl { get; set; }
#else
        public string StyleUrl { get; set; }
#endif

        #endregion

        #region Constructors

#if NET7_0_OR_GREATER
        public KmlStyleMapPair() { }
#endif

#if NET7_0_OR_GREATER
        [SetsRequiredMembers]
#endif
        protected KmlStyleMapPair(XElement xml, IXmlNamespaceResolver namespaces) {
            Key = xml.GetElementValue("kml:key", namespaces);
            StyleUrl = xml.GetElementValue("kml:styleUrl", namespaces);
            if (Key is null) throw new KmlParseException($"Failed parsing 'kml:key' from '{xml.Name}'...");
            if (StyleUrl is null) throw new KmlParseException($"Failed parsing 'kml:styleUrl' from '{xml.Name}'...");
        }

        #endregion

        #region Properties

        public override XElement ToXElement() {

            if (string.IsNullOrWhiteSpace(Key)) throw new PropertyNotSetException(nameof(Key), "The Key property of a <Pair> must have a value");
            if (string.IsNullOrWhiteSpace(StyleUrl)) throw new PropertyNotSetException(nameof(StyleUrl), "The StyleUrl property of a <Pair> must have a value");

            XElement xml = base.NewXElement("Pair");

            xml.Add(NewXElement("key", Key));
            xml.Add(NewXElement("styleUrl", StyleUrl));

            return xml;

        }

        #endregion

        #region Static methods

        public static KmlStyleMapPair Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlStyleMapPair(xml, Namespaces);
        }

        public static KmlStyleMapPair Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlStyleMapPair(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}