using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public class KmlStyleMap : KmlStyleSelector, IEnumerable<KmlStyleMapPair> {

        protected List<KmlStyleMapPair> Pairs = new List<KmlStyleMapPair>();

        #region Properties

        public int Count => Pairs.Count;

        public KmlStyleMapPair Normal => Pairs.FirstOrDefault(x => x.Key == "normal");

        public KmlStyleMapPair Highlight => Pairs.FirstOrDefault(x => x.Key == "highlight");

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an empty style map.
        /// </summary>
        public KmlStyleMap() { }

        /// <summary>
        /// Initializes a new style map from the specified array of <paramref name="pairs"/>.
        /// </summary>
        /// <param name="pairs">An array of <see cref="KmlStyleMapPair"/>.</param>
        public KmlStyleMap(params KmlStyleMapPair[] pairs) {
            Pairs = pairs?.ToList() ?? new List<KmlStyleMapPair>();
        }

        /// <summary>
        /// Initializes a new style map from the specified collection of <paramref name="pairs"/>.
        /// </summary>
        /// <param name="pairs">A collection of <see cref="KmlStyleMapPair"/>.</param>
        public KmlStyleMap(IEnumerable<KmlStyleMapPair> pairs) {
            Pairs = pairs?.ToList() ?? new List<KmlStyleMapPair>();
        }

        protected KmlStyleMap(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            Pairs = xml.GetElements("kml:Pair", namespaces, KmlStyleMapPair.Parse).ToList();
        }

        #endregion

        #region Member methods

        public void Add(KmlStyleMapPair pair) {
            Pairs.Add(pair);
        }

        public void Remove(KmlStyleMapPair pair) {
            Pairs.Remove(pair);
        }

        public void Clear() {
            Pairs.Clear();
        }

        public override XElement ToXElement() {
            
            XElement xml = base.ToXElement();

            foreach (KmlStyleMapPair pair in Pairs) {
                xml.Add(pair.ToXElement());
            }

            return xml;

        }

        public IEnumerator<KmlStyleMapPair> GetEnumerator() {
            return Pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Static methods

        public new static KmlStyleMap Parse(XElement xml) {
            return xml == null ? null : new KmlStyleMap(xml, Namespaces);
        }

        public new static KmlStyleMap Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlStyleMap(xml, namespaces);
        }

        #endregion

    }

}