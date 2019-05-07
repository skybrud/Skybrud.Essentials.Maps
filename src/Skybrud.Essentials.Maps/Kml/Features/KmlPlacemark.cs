using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Geometry;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features {

    public class KmlPlacemark : KmlFeature {

        #region Properties

        public KmlExtendedData ExtendedData { get; set; }

        public KmlGeometry Geometry { get; set; }

        #endregion

        #region Constructors

        public KmlPlacemark() { }

        public KmlPlacemark(KmlGeometry geometry) {
            Geometry = geometry;
        }

        protected KmlPlacemark(XElement xml) : this(xml, Namespaces) { }

        protected KmlPlacemark(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            ExtendedData = xml.GetElement("kml:ExtendedData", namespaces, KmlExtendedData.Parse);
            Geometry = KmlUtils.ParseGeometryChildren(xml, namespaces).FirstOrDefault();
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            
            XElement xml = base.ToXElement();

            if (Geometry != null) xml.Add(Geometry.ToXElement());

            return xml;

        }

        #endregion

        public static KmlPlacemark Parse(XElement xml) {
            return xml == null ? null : new KmlPlacemark(xml, Namespaces);
        }

        public static KmlPlacemark Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlPlacemark(xml, namespaces);
        }

    }

}