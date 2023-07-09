using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public abstract class KmlStyleSelector : KmlObject {

        #region Properties

        public string? Id { get; set; }

        [MemberNotNullWhen(true, "Id")]
        public bool HasId => Id.HasValue();

        #endregion

        #region Constructors

        protected KmlStyleSelector() { }

        protected KmlStyleSelector(XElement xml, IXmlNamespaceResolver namespaces) {
            Id = xml.GetAttributeValue("id", namespaces);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (!string.IsNullOrWhiteSpace(Id)) xml.Add(new XAttribute("id", Id!));

            return xml;

        }

        #endregion

        #region Static methods

        public static KmlStyleSelector Parse(XElement xml) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            return Parse(xml, Namespaces);
        }

        public static KmlStyleSelector Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            namespaces ??= Namespaces;
            return xml.Name.LocalName switch {
                "Style" => KmlStyle.Parse(xml, namespaces),
                "StyleMap" => KmlStyleMap.Parse(xml, namespaces),
                _ => throw new Exception("Unknown style selector " + xml.Name.LocalName)
            };
        }

        #endregion

    }

}