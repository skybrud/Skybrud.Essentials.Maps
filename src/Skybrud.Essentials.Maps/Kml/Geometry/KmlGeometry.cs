using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public abstract class KmlGeometry : KmlObject {

        #region Properties

        public string Id { get; set; }

        #endregion

        #region Constructors

        protected KmlGeometry() { }

        protected KmlGeometry(XElement xml, XmlNamespaceManager namespaces) {
            Id = xml.GetAttributeValue("id");
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (Id.HasValue()) xml.Add(new XAttribute("id", Id));

            return xml;

        }

        #endregion

    }

}