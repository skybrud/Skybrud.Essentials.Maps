using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Skybrud.Essentials.Maps.Kml.Features {

    public class KmlFolderCollection : IEnumerable<KmlFolder> {

        private readonly List<KmlFolder> _folders;

        private readonly Dictionary<string, KmlFolder> _lookup;

        #region Properties

        public int Count => _folders.Count;

        #endregion

        #region Constructors

        public KmlFolderCollection(KmlFolder[] folders) {
            _folders = folders?.ToList() ?? new List<KmlFolder>();
            _lookup = _folders.Where(x => String.IsNullOrWhiteSpace(x.Id) == false).ToDictionary(x => x.Id);
        }

        #endregion

        #region Member methods

        public void Add(KmlFolder folder) {
            if (folder == null) throw new ArgumentNullException(nameof(folder));
            _folders.Add(folder);
            if (String.IsNullOrWhiteSpace(folder.Id)) return;
            _lookup[folder.Id] = folder;
        }

        public bool Contains(string id) {
            if (String.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            return _lookup.ContainsKey(id);
        }

        public IEnumerator<KmlFolder> GetEnumerator() {
            return _folders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Operator overloading

        public static implicit operator KmlFolderCollection(KmlFolder[] folders) {
            return new KmlFolderCollection(folders);
        }

        #endregion

    }

}