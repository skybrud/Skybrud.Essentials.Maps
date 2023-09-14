using System;
using System.IO;
using System.IO.Compression;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Maps.Kml;
using Skybrud.Essentials.Maps.Kml.Constants;
using Skybrud.Essentials.Maps.Kml.Features;

namespace Skybrud.Essentials.Maps.Kmz;

/// <summary>
/// Class representing A KMZ file.
/// </summary>
public class KmzFile : IDisposable {

    #region Properties

    /// <summary>
    /// Gets a reference to the internal stream.
    /// </summary>
    protected Stream Stream { get; }

    /// <summary>
    /// Gets a reference to the underlying <see cref="ZipArchive"/> of the KMZ file.
    /// </summary>
    public ZipArchive Archive { get; }

    /// <summary>
    /// Gets the <c>doc.kml</c> KML file of the KMZ file.
    /// </summary>
    public KmlFile Kml { get; }

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

    /// <inheritdoc />
    public void Dispose() {
        Stream.Dispose();
        Archive.Dispose();
    }

    private KmlFile LoadKmlFile() {
        ZipArchiveEntry? entry = Archive.GetEntry("doc.kml");
        if (entry == null) throw new KmzException("KMZ file is missing 'doc.kml' entry");
        using Stream stream = entry.Open();
        using StreamReader reader = new(stream);
        return KmlFile.Parse(reader.ReadToEnd());
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Opens the KMZ file at the specified <paramref name="path"/> for reading.
    /// </summary>
    /// <param name="path">The path to the file on disk.</param>
    /// <returns>An instance of <see cref="KmzFile"/>.</returns>
    public static KmzFile OpenRead(string path) {
        return new KmzFile(path);
    }

    /// <summary>
    /// Parses the specified <paramref name="bytes"/> into an instances of <see cref="KmzFile"/>.
    /// </summary>
    /// <param name="bytes">The bytes representing the KMZ file.</param>
    /// <returns>An instance of <see cref="KmzFile"/>.</returns>
    public static KmzFile Parse(byte[] bytes) {
        return new KmzFile(bytes);
    }

    /// <summary>
    /// Returns the KML file at the specified <paramref name="path"/>.
    ///
    /// If the KMZ file's <see cref="KmlDocument.NetworkLink"/> property indicates another KML or KMZ file, that file will be retrieved and returned instead.
    /// </summary>
    /// <param name="path">The path to the file on disk.</param>
    /// <returns>An instance of <see cref="KmlFile"/>.</returns>
    public static KmlFile LoadKml(string path) {

        KmlFile kml;

        // Get the KML file from the KMZ file at "path"
        using (KmzFile kmz = OpenRead(path)) {
            kml = kmz.Kml;
        }

        // If the KML file doesn't have a network link, we'll just return it right away
        if (kml.Document.HasNetworkLink == false) return kml;

        // Get the KML or KMZ file specified by the network link
        HttpResponse response = (HttpResponse) HttpUtils.Requests.Get(kml.Document.NetworkLink.Link.Href);

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