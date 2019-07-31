using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Skybrud.Essentials.Maps.Kml {

    public abstract class KmlCollectionBase<T> : IEnumerable<T> {
        
        private readonly List<T> _children;

        protected KmlCollectionBase() {
            _children = new List<T>();
        }

        protected KmlCollectionBase(IEnumerable<T> children) {
            _children = children?.ToList() ?? new List<T>();
        }

        public void Add(T item) {
            _children.Add(item);
        }

        public void AddRange(IEnumerable<T> collection) {
            _children.AddRange(collection);
        }

        public IEnumerator<T> GetEnumerator() {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}