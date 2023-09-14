using System;
using System.Globalization;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Strings;

namespace Skybrud.Essentials.Maps.Kml.Geometry;

/// <summary>
/// Class representing the <c>&lt;coordinates&gt;</c> element within a KML <c>&lt;Point&gt;</c> element.
/// </summary>
public class KmlPointCoordinates : KmlObject, IPoint {

    #region Properties

    /// <summary>
    /// Gets or sets the latitude of the point (&gt;= −90 and &lt;= 90).
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the point (&gt;= −180 and &lt;= 180).
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the meters above sea level.
    /// </summary>
    public double Altitude { get; set; }

    /// <summary>
    /// Gets whether this point has an altitude value.
    /// </summary>
    public bool HasAltitude => Math.Abs(Altitude) > double.Epsilon;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new empty KML <c>&lt;coordinates&gt;</c> element.
    /// </summary>
    public KmlPointCoordinates() { }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element based on the specified <paramref name="latitude"/> and <paramref name="longitude"/>.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    public KmlPointCoordinates(double latitude, double longitude) {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element based on the specified <paramref name="latitude"/>, <paramref name="longitude"/> and <paramref name="altitude"/>.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="altitude">The altitude.</param>
    public KmlPointCoordinates(double latitude, double longitude, double altitude) {
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element based on the specified <paramref name="array"/>.
    /// </summary>
    /// <param name="array">An array of coordinates that make up the element.</param>
    public KmlPointCoordinates(double[] array) {
        Latitude = array[1];
        Longitude = array[0];
        Altitude = array.Length == 3 ? array[2] : 0;
    }

    /// <summary>
    /// Initializes a new KML <c>&lt;coordinates&gt;</c> element.
    /// </summary>
    /// <param name="xml">The XML element the document should be based on.</param>
    protected KmlPointCoordinates(XElement xml) {

        if (xml is null) throw new ArgumentNullException(nameof(xml));

        double[] array = StringUtils.ParseDoubleArray(xml.Value);

        Latitude = array[1];
        Longitude = array[0];
        Altitude = array.Length == 3 ? array[2] : 0;

    }

    #endregion

    #region Member methods

    /// <inheritdoc />
    public override string ToString() {

        // Determine whether the altitude should be included
        string format = HasAltitude ? "{0},{1},{2}" : "{0},{1}";

        // Generate the string value
        return string.Format(CultureInfo.InvariantCulture, format, Longitude, Latitude, Altitude);

    }

    /// <inheritdoc />
    public override XElement ToXElement() {
        return NewXElement("coordinates",ToString());
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="xml"/> element into an instance of <see cref="KmlPointCoordinates"/>.
    /// </summary>
    /// <param name="xml">The XML element representing the document.</param>
    /// <returns>An instance of <see cref="KmlPointCoordinates"/>.</returns>
    public static KmlPointCoordinates Parse(XElement xml) {
        if (xml == null) throw new ArgumentNullException(nameof(xml));
        return new KmlPointCoordinates(xml);
    }

    #endregion

}