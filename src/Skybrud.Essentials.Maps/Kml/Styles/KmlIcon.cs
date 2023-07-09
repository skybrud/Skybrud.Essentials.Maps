using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Exceptions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public class KmlIcon : KmlObject {

        #region Properties

#if NET7_0_OR_GREATER
        public required string Href { get; set; }
#else
        public string Href { get; set; }
#endif

        #endregion

        #region Constructors

#if NET7_0_OR_GREATER
        public KmlIcon() { }
#endif

#if NET7_0_OR_GREATER
        [SetsRequiredMembers]
#endif
        public KmlIcon(string href) {
            Href = href;
        }

#if NET7_0_OR_GREATER
        [SetsRequiredMembers]
#endif
        protected KmlIcon(XElement xml, IXmlNamespaceResolver namespaces) {
            Href = xml.GetElementValue("kml:href", namespaces);
            if (string.IsNullOrWhiteSpace(Href)) throw new KmlException($"Failed parsing 'kml:href' value from '{xml.Name}'.");
        }

        #endregion

        #region Mmember methods

        public override XElement ToXElement() {
            XElement xml = NewXElement("Icon");
            xml.Add(NewXElement("href", Href));
            return xml;
        }

        #endregion

        #region Static methods

        public static KmlIcon Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlIcon(xml, Namespaces);

        }

        public static KmlIcon Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return new KmlIcon(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}