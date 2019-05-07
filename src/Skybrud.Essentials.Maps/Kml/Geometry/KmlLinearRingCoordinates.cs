using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Skybrud.Essentials.Strings;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    public class KmlLinearRingCoordinates : KmlObject, IEnumerable<KmlPointCoordinates> {

        private readonly List<KmlPointCoordinates> _list;

        #region Properties

        public int Count => _list.Count;

        public KmlPointCoordinates this[int index] => _list.ElementAt(index);

        #endregion

        #region Constructors

        public KmlLinearRingCoordinates() {
            _list = new List<KmlPointCoordinates>();
        }

        public KmlLinearRingCoordinates(double[][] array) {
            _list = new List<KmlPointCoordinates>();
            foreach (double[] item in array) {
                _list.Add(new KmlPointCoordinates(item));
            }
        }

        public KmlLinearRingCoordinates(params KmlPointCoordinates[] coordinates) {
            _list = coordinates?.ToList() ?? new List<KmlPointCoordinates>();
        }

        public KmlLinearRingCoordinates(XElement xml) {
            string[] value = xml.Value.Split(new []{ " ", "\n", "\r", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            _list = value.Select(x => new KmlPointCoordinates(StringUtils.ParseDoubleArray(x))).ToList();
        }

        #endregion

        #region Member methods

        public void Add(KmlPointCoordinates coordinates) {
            _list.Add(coordinates);
        }

        public override XElement ToXElement() {
            string value = String.Join(" ", from p in _list select p.ToString());
            return NewXElement("coordinates", value);
        }

        public IEnumerator<KmlPointCoordinates> GetEnumerator() {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Static methods

        public static KmlLinearRingCoordinates Parse(XElement xml) {
            return xml == null ? null : new KmlLinearRingCoordinates(xml);

        }

        #endregion

    }

}