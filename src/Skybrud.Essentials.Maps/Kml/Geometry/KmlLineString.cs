using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public class KmlLineString : KmlGeometry, IEnumerable<KmlPointCoordinates> {

        private readonly List<KmlPointCoordinates> _list;

        #region Properties

        public int Count => _list.Count;

        public KmlPointCoordinates this[int index] => _list.ElementAt(index);

        #endregion

        #region Constructors

        public KmlLineString() {
            _list = new List<KmlPointCoordinates>();
        }
        
        public KmlLineString(params KmlPointCoordinates[] coordinates) {
            _list = coordinates?.ToList() ?? new List<KmlPointCoordinates>();
        }

        protected KmlLineString(XElement xml, XmlNamespaceManager namespaces) : base(xml, namespaces) {
            string[] coordinates = xml.GetElementValue("kml:coordinates", namespaces).Split(new[] { " ", "\n", "\r", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            _list = coordinates.Select(x => new KmlPointCoordinates(StringUtils.ParseDoubleArray(x))).ToList();
        }

        #endregion

        #region Member methods

        public void Add(KmlPointCoordinates coordinates) {
            _list.Add(coordinates);
        }

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            string coordinates = string.Join(" ", from p in _list select p.ToString());
            xml.Add(NewXElement("coordinates", coordinates));

            return xml;

        }

        public IEnumerator<KmlPointCoordinates> GetEnumerator() {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        public static KmlLineString Parse(XElement xml) {
            return xml == null ? null : new KmlLineString(xml, Namespaces);

        }

        public static KmlLineString Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlLineString(xml, namespaces);
        }

    }

}