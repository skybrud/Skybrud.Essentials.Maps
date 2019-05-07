using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features {

    public class KmlFolder : KmlFeature {
        
        #region Properties

        public KmlFeatureCollection Features { get; }

        #endregion

        #region Constructors

        public KmlFolder(IEnumerable<KmlPlacemark> placemarks) {
            Features = new KmlFeatureCollection(placemarks);
        }

        public KmlFolder(IEnumerable<KmlFeature> features) {
            Features = new KmlFeatureCollection(features);
        }

        protected KmlFolder(XElement xml, XmlNamespaceManager namespaces) {
            Id = xml.GetAttributeValue("id");
            Name = xml.GetElementValue("kml:name", namespaces);
            Features = xml.GetElements("kml:Placemark", namespaces, KmlPlacemark.Parse);
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xFolder = base.ToXElement();

            foreach (KmlFeature feature in Features) {
                xFolder.Add(feature.ToXElement());
            }

            return xFolder;

        }

        #endregion

        public static KmlFolder Parse(XElement xml) {
            return new KmlFolder(xml, Namespaces);

        }

        public static KmlFolder Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlFolder(xml, namespaces);
        }

    }

}