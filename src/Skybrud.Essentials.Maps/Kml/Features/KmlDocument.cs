using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Extensions;
using Skybrud.Essentials.Maps.Kml.Geometry;
using Skybrud.Essentials.Maps.Kml.Styles;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Features {

    public class KmlDocument : KmlContainer {

        #region Properties

        public KmlNetworkLink NetworkLink { get; set; }

        public bool HasNetworkLink => NetworkLink != null;

        public KmlStyleSelectorCollection StyleSelectors { get; }

        public KmlFeatureCollection Features { get; }

        #endregion

        #region Constructors

        public KmlDocument() {
            StyleSelectors = new KmlStyleSelectorCollection();
            Features = new KmlFeatureCollection();
        }

        public KmlDocument(params KmlFeature[] features) {
            StyleSelectors = new KmlStyleSelectorCollection();
            Features = features ?? new KmlFeatureCollection();
        }

        protected KmlDocument(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {

            NetworkLink = xml.GetElement("kml:NetworkLink", namespaces, KmlNetworkLink.Parse);

            List<KmlStyleSelector> selectors = new List<KmlStyleSelector>();

            List<KmlFeature> features = new List<KmlFeature>();

            foreach (XElement child in xml.Elements()) {

                switch (child.Name.LocalName) {

                    case "Style":
                        selectors.Add(KmlStyle.Parse(child, namespaces));
                        break;

                    case "StyleMap":
                        selectors.Add(KmlStyleMap.Parse(child, namespaces));
                        break;

                    case "Document":
                        features.Add(KmlDocument.Parse(child, namespaces));
                        break;

                    case "Folder":
                        features.Add(KmlFolder.Parse(child, namespaces));
                        break;

                    case "NetworkLink":
                        features.Add(KmlNetworkLink.Parse(child, namespaces));
                        break;

                    case "Placemark":
                        features.Add(KmlPlacemark.Parse(child, namespaces));
                        break;

                    case "GroundOverlay":
                    case "PhotoOverlay":
                    case "ScreenOverlay":
                        // currently not supported
                        break;

                }

            }

            StyleSelectors = new KmlStyleSelectorCollection(selectors);
            Features = new KmlFeatureCollection(features);

        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            
            XElement xml = base.ToXElement();

            if (NetworkLink.HasValue()) {
                xml.Add(NetworkLink.ToXElement());
            }

            if (StyleSelectors != null) {
                foreach (KmlStyleSelector selector in StyleSelectors) {
                    xml.Add(selector.ToXElement());
                }
            }

            if (Features != null) {
                foreach (KmlFeature feature in Features) {
                    xml.Add(feature.ToXElement());
                }
            }

            return xml;

        }

        #endregion

        #region Static methods
        
        public static KmlDocument Parse(XElement xml) {
            return new KmlDocument(xml, Namespaces);
        }

        public static KmlDocument Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlDocument(xml, namespaces);
        }

        #endregion

    }

}