using System.Collections.Generic;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Features {

    public class KmlFeatureCollection : KmlCollectionBase<KmlFeature> {

        public KmlFeatureCollection() { }

        public KmlFeatureCollection(IEnumerable<KmlFeature> features) {
            AddRange(features);
        }
        
        #region Operator overloading

        public static implicit operator KmlFeatureCollection(KmlFeature[] features) {
            return new KmlFeatureCollection(features);
        }

        public static implicit operator KmlFeatureCollection(KmlPlacemark[] placemarks) {
            return new KmlFeatureCollection(placemarks);
        }

        #endregion

        public static KmlFeatureCollection GetFromChildren(XElement xml) {
            
            List<KmlFeature> temp = new List<KmlFeature>();

            foreach (XElement element in xml.Elements()) {

                switch (element.Name.LocalName) {

                    case "Placemark":
                        temp.Add(KmlPlacemark.Parse(element));
                        break;

                    case "Folder":
                        temp.Add(KmlFolder.Parse(element));
                        break;

                    case "Document":
                        temp.Add(KmlDocument.Parse(element));
                        break;

                }

            }

            return temp.ToArray();

        }

    }

}