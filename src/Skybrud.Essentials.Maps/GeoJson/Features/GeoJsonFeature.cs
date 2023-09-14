using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.GeoJson.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Features;

/// <summary>
/// Class representing a GeoJSON <strong>Feature</strong>.
/// </summary>
public class GeoJsonFeature : GeoJsonObject, IGeoJsonFeature {

    #region Properties

    /// <summary>
    /// Gets or sets the properties of the feature.
    /// </summary>
    [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
    public GeoJsonProperties Properties { get; set; }

    /// <summary>
    /// Gets or sets the geometry of the feature.
    /// </summary>
    [JsonProperty("geometry")]
    public GeoJsonGeometry Geometry { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance with the specified <paramref name="geometry"/>.
    /// </summary>
    /// <param name="geometry">The geometry of the feature.</param>
    public GeoJsonFeature(GeoJsonGeometry geometry) : base(GeoJsonType.Feature) {
        Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
        Properties = new GeoJsonProperties();
    }

    /// <summary>
    /// Initializes a new instance with the specified <paramref name="geometry"/>.
    /// </summary>
    /// <param name="geometry">The geometry of the feature.</param>
    /// <param name="properties">The properties of the feature.</param>
    public GeoJsonFeature(GeoJsonGeometry geometry, GeoJsonProperties? properties) : base(GeoJsonType.Feature) {
        Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
        Properties = properties ?? new GeoJsonProperties();
    }

    /// <summary>
    /// Initializes a new instance based on the specified <paramref name="json"/> object.
    /// </summary>
    /// <param name="json">An instance of <see cref="JObject"/> representing the feature.</param>
    protected GeoJsonFeature(JObject json) : base(GeoJsonType.Feature) {
        if (json is null) throw new ArgumentNullException(nameof(json));
        Geometry = json.GetObject<GeoJsonGeometry>("geometry")!;
        if (Geometry is null) throw new GeoJsonParseException("Failed parsing GeoJSON geometry.", json);
        Properties = json.GetObject("properties", GeoJsonProperties.Parse) ?? new GeoJsonProperties();
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonFeature"/>.
    /// </summary>
    /// <param name="json">The JSON string to be parsed.</param>
    /// <returns>An instance of <see cref="GeoJsonFeature"/>.</returns>
    public static GeoJsonFeature Parse(string json) {
        if (string.IsNullOrWhiteSpace(json)) throw new ArgumentNullException(nameof(json));
        return ParseJsonObject(json, Parse);
    }

    /// <summary>
    /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonFeature"/>.
    /// </summary>
    /// <param name="json">The instance of <see cref="JObject"/> to be parsed.</param>
    /// <returns>An instance of <see cref="GeoJsonFeature"/>.</returns>
    public static GeoJsonFeature Parse(JObject json) {
        if (json is null) throw new ArgumentNullException(nameof(json));
        return new GeoJsonFeature(json);
    }

    /// <summary>
    /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonFeature"/>.
    /// </summary>
    /// <param name="path">The path to a file on disk.</param>
    /// <returns>An instance of <see cref="GeoJsonFeature"/>.</returns>
    public static GeoJsonFeature Load(string path) {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
        return LoadJsonObject(path, Parse);
    }

    #endregion

}