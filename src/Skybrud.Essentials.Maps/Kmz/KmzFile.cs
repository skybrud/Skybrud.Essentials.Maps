using System;
using System.IO;
using System.IO.Compression;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Maps.Kml;
using Skybrud.Essentials.Maps.Kml.Constants;

namespace Skybrud.Essentials.Maps.Kmz {

    public class KmzFile : IDisposable {

        #region Properties

        protected Stream Stream { get; private set; }

        public ZipArchive Archive { get; private set; }

        public KmlFile Kml { get; private set; }

        #endregion

        #region Constructors

        private KmzFile(string path) {
            Stream = new FileStream(path, FileMode.Open);
            Archive = new ZipArchive(Stream, ZipArchiveMode.Read);
            Kml = LoadKmlFile();
        }

        private KmzFile(byte[] bytes) {
            Stream = new MemoryStream(bytes);
            Archive = new ZipArchive(Stream, ZipArchiveMode.Read);
            Kml = LoadKmlFile();
        }

        #endregion

        #region Member methods

        public void Dispose() {
            Stream?.Dispose();
            Archive?.Dispose();
        }

        private KmlFile LoadKmlFile() {

            ZipArchiveEntry entry = Archive.GetEntry("doc.kml");

            if (entry == null) throw new KmzException("KMZ file is missing doc.kml entry");

            using (Stream stream = entry.Open()) {
                using (StreamReader reader = new StreamReader(stream)) {
                    return KmlFile.Parse(reader.ReadToEnd());
                }
            }

        }

        #endregion

        #region Static methods

        public static KmzFile OpenRead(string path) {
            return new KmzFile(path);
        }

        public static KmzFile Parse(byte[] bytes) {
            return new KmzFile(bytes);
        }

        public static KmlFile LoadKml(string path) {

            KmlFile kml;

            // Get the KML file from the KMZ file at "path"
            using (KmzFile kmz = OpenRead(path)) {
                kml = kmz.Kml;
            }

            // If the KML file doesn't have a network link, we'll just return it right away
            if (kml.Document.HasNetworkLink == false) return kml;

            // Get the KML or KMZ file specified by the network link
            HttpResponse response = (HttpResponse) HttpUtils.Http.DoHttpGetRequest(kml.Document.NetworkLink.Link.Href);

            // Parse the response body
            switch (response.ContentType) {

                case KmlConstants.KmlMimeType:
                    return KmlFile.Parse(response.Body);

                case KmlConstants.KmzMimeType:
                    using (KmzFile kmz = Parse(response.BinaryBody)) return kmz.Kml;

                default:
                    throw new KmzException("Unknown mime type " + response.ContentType);

            }

        }

        #endregion

    }

}