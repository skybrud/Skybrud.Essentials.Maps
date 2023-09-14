using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Essentials.Maps.GeoJson.Features;

/// <summary>
/// Class representing a GeoJSON <strong>FeatureCollection</strong>.
/// </summary>
public class GeoJsonFeatureCollection : GeoJsonObject, IGeoJsonFeature {

    #region Properties

    /// <summary>
    /// Gets or sets the name of the feature collection.
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a list of the features in this feature collection.
    /// </summary>
    [JsonProperty("features")]
    public List<GeoJsonFeature> Features { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new empty instance.
    /// </summary>
    public GeoJsonFeatureCollection() : base(GeoJsonType.FeatureCollection) {
        Features = new List<GeoJsonFeature>();
    }

    /// <summary>
    /// Initializes a new instance with the specified <paramref name="features"/>.
    /// </summary>
    /// <param name="features">An array with the features the collection should initially be based on.</param>
    public GeoJsonFeatureCollection(params GeoJsonFeature[] features) : base(GeoJsonType.FeatureCollection) {
        Features = features?.ToList() ?? new List<GeoJsonFeature>();
    }

    /// <summary>
    /// Initializes a new instance with the specified <paramref name="features"/>.
    /// </summary>
    /// <param name="features">An array with the features the collection should initially be based on.</param>
    public GeoJsonFeatureCollection(IEnumerable<GeoJsonFeature> features) : base(GeoJsonType.FeatureCollection) {
        Features = features?.ToList() ?? new List<GeoJsonFeature>();
    }

    /// <summary>
    /// Initializes a new instance based on the specified <paramref name="json"/>.
    /// </summary>
    /// <param name="json">An instance of <see cref="JObject"/> representing the <strong>FeatureCollection</strong>.</param>
    protected GeoJsonFeatureCollection(JObject json) : base(GeoJsonType.FeatureCollection) {
        Name = json.GetString("name");
        Features = json.GetArrayItems("features", GeoJsonFeature.Parse).ToList();
    }

    #endregion

    #region Member methods

    /// <summary>
    /// Adds the specified <paramref name="feature"/>.
    /// </summary>
    /// <param name="feature">The feature to be added.</param>
    public void Add(GeoJsonFeature feature) {
        if (feature == null) throw new ArgumentNullException(nameof(feature));
        Features.Add(feature);
    }

    /// <summary>
    /// Adds the specified collection of <paramref name="features"/>.
    /// </summary>
    /// <param name="features">A collection with the features to be added.</param>
    public void AddRange(IEnumerable<GeoJsonFeature> features) {
        if (features == null) throw new ArgumentNullException(nameof(features));
        Features.AddRange(features);
    }

    /// <summary>
    /// Removes the first occurence of the specified <paramref name="feature"/>.
    /// </summary>
    /// <param name="feature">The feature to be removed.</param>
    /// <returns><c>true</c> if item is successfully removed; otherwise, <c>false</c>. This method also returns false if item was not found in the feature collection.</returns>
    public bool Remove(GeoJsonFeature feature) {
        if (feature == null) throw new ArgumentNullException(nameof(feature));
        return Features.Remove(feature);
    }

    /// <summary>
    /// Returns whether this collection contains the specified <paramref name="feature"/>.
    /// </summary>
    /// <param name="feature">The feature.</param>
    /// <returns><c>true</c> if item is found in the collection; otherwise, <c>false</c>.</returns>
    public bool Contains(GeoJsonFeature feature) {
        if (feature == null) throw new ArgumentNullException(nameof(feature));
        return Features.Contains(feature);
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonFeatureCollection"/>.
    /// </summary>
    /// <param name="json">The JSON string to be parsed.</param>
    /// <returns>An instance of <see cref="GeoJsonFeatureCollection"/>.</returns>
    [return: NotNullIfNotNull("json")]
    public static GeoJsonFeatureCollection? Parse(string? json) {
        return json is null ? null : ParseJsonObject(json, Parse);
    }

    /// <summary>
    /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonFeatureCollection"/>.
    /// </summary>
    /// <param name="json">The instance of <see cref="JObject"/> to be parsed.</param>
    /// <returns>An instance of <see cref="GeoJsonFeatureCollection"/>.</returns>
    [return: NotNullIfNotNull("json")]
    public static GeoJsonFeatureCollection? Parse(JObject? json) {
        return json == null ? null : new GeoJsonFeatureCollection(json);
    }

    /// <summary>
    /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonFeatureCollection"/>.
    /// </summary>
    /// <param name="path">The path to a file on disk.</param>
    /// <returns>An instance of <see cref="GeoJsonFeatureCollection"/>.</returns>
    public static GeoJsonFeatureCollection Load(string path) {
        return LoadJsonObject(path, Parse)!;
    }

    #endregion

}