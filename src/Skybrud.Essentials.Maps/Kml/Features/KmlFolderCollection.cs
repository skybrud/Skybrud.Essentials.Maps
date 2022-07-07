using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Skybrud.Essentials.Maps.Kml.Features {

    /// <summary>
    /// Class representing a KML <c>&lt;FolderCollection&gt;</c> element.
    /// </summary>
    public class KmlFolderCollection : IEnumerable<KmlFolder> {

        private readonly List<KmlFolder> _folders;

        private readonly Dictionary<string, KmlFolder> _lookup;

        #region Properties

        /// <summary>
        /// Gets the amount of folders in the collection.
        /// </summary>
        public int Count => _folders.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;FolderCollection&gt;</c> element.
        /// </summary>
        public KmlFolderCollection() {
            _folders = new List<KmlFolder>();
            _lookup = new Dictionary<string, KmlFolder>();
        }

        /// <summary>
        /// Initializes a new KML <c>&lt;FolderCollection&gt;</c> element containing the specified <paramref name="folders"/>.
        /// </summary>
        /// <param name="folders">An array of folders to add to the folder collection.</param>
        public KmlFolderCollection(IEnumerable<KmlFolder> folders) {
            _folders = folders?.ToList() ?? new List<KmlFolder>();
            _lookup = _folders.Where(x => string.IsNullOrWhiteSpace(x.Id) == false).ToDictionary(x => x.Id);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds the specified <paramref name="folder"/> to the <c>&lt;FolderCollection&gt;</c>.
        /// </summary>
        /// <param name="folder">The folder to be added.</param>
        public void Add(KmlFolder folder) {
            if (folder == null) throw new ArgumentNullException(nameof(folder));
            _folders.Add(folder);
            if (string.IsNullOrWhiteSpace(folder.Id)) return;
            _lookup[folder.Id] = folder;
        }

        /// <summary>
        /// Returns whether the <c>&lt;FolderCollection&gt;</c> contains a <c>&lt;Folder&gt;</c> with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID to search for.</param>
        /// <returns><c>true</c> if a <c>&lt;Folder&gt;</c> with a matching ID was found; otherwise <c>false</c>.</returns>
        public bool Contains(string id) {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            return _lookup.ContainsKey(id);
        }

        /// <inheritdoc />
        public IEnumerator<KmlFolder> GetEnumerator() {
            return _folders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Operator overloading

        /// <summary>
        /// Initializes a new KML <c>&lt;FolderCollection&gt;</c> element containing the specified <paramref name="folders"/>.
        /// </summary>
        /// <param name="folders">An array of folders to add to the folder collection.</param>
        public static implicit operator KmlFolderCollection(KmlFolder[] folders) {
            return new KmlFolderCollection(folders);
        }

        #endregion

    }

}