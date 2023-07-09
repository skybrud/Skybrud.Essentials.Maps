using System;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public abstract class KmlStyleSelector : KmlObject {

        #region Properties

        public string Id { get; set; }

        public bool HasId => Id.HasValue();

        #endregion

        #region Constructors

        protected KmlStyleSelector() { }

        protected KmlStyleSelector(XElement xml, XmlNamespaceManager namespaces) {
            Id = xml.GetAttributeValue("id", namespaces);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (Id.HasValue()) xml.Add(new XAttribute("id", Id));

            return xml;

        }

        #endregion

        #region Static methods

        public static KmlStyleSelector Parse(XElement xml) {
            return Parse(xml, Namespaces);
        }

        public static KmlStyleSelector Parse(XElement xml, XmlNamespaceManager namespaces) {
            if (xml == null) return null;
            return xml.Name.LocalName switch {
                "Style" => KmlStyle.Parse(xml, namespaces),
                "StyleMap" => KmlStyleMap.Parse(xml, namespaces),
                _ => throw new Exception("Unknown style selector " + xml.Name.LocalName)
            };
        }

        #endregion

    }

}