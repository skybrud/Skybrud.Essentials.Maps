using System.Collections.Generic;
using System.Xml.Linq;

namespace Skybrud.Essentials.Maps.Kml.Features {

    /// <summary>
    /// Class representing a KML <c>&lt;FeatureCollection&gt;</c> element.
    /// </summary>
    public class KmlFeatureCollection : KmlCollectionBase<KmlFeature> {

        /// <summary>
        /// Initializes a new empty KML <c>&lt;FeatureCollection&gt;</c> element.
        /// </summary>
        public KmlFeatureCollection() { }

        /// <summary>
        /// Initializes a new empty KML <c>&lt;FeatureCollection&gt;</c> element containing the specified <paramref name="features"/>.
        /// </summary>
        /// <param name="features">An array of features to add to the document.</param>
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

            List<KmlFeature> temp = new();

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