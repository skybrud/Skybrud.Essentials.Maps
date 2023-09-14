using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Features;
using Skybrud.Essentials.Maps.GeoJson.Json;

namespace Skybrud.Essentials.Maps.GeoJson;

/// <summary>
/// Class representing a list of properties of a <see cref="GeoJsonFeature"/>.
/// </summary>
[JsonConverter(typeof(GeoJsonReadConverter))]
public class GeoJsonProperties : IEnumerable<KeyValuePair<string, object>> {

    #region Properties

    /// <summary>
    /// Gets the amount of properties.
    /// </summary>
    [JsonIgnore]
    public int Count => Properties.Count;

    /// <summary>
    /// Gets a reference to the internal dictionary.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, object> Properties { get; }

    /// <summary>
    /// Gets an array with the keys of the internal dictionary.
    /// </summary>
    [JsonIgnore]
    public IReadOnlyList<string> Keys => Properties.Keys.ToArray();

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a System.Collections.Generic.KeyNotFoundException, and a set operation creates a new element with the specified key.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
    /// <exception cref="KeyNotFoundException">The property is retrieved and <paramref name="key"/> does not exist in the collection.</exception>
    [JsonIgnore]
    public object this[string key] {
        get => Properties[key];
        set => Properties[key] = value;
    }

    /// <summary>
    /// Gets or sets the name of the feature.
    /// </summary>
    public string? Name {
        get => Properties.TryGetValue("name", out object? value) ? value as string : null;
        set => SetOrRemoveIfNull("name", value);
    }

    /// <summary>
    /// Gets or sets the description of the feature.
    /// </summary>
    public string? Description {
        get => Properties.TryGetValue("description", out object? value) ? value as string : null;
        set => SetOrRemoveIfNull("description", value);
    }

    public string? Fill {
        get => Properties.TryGetValue("fill", out object? value) ? value as string : null;
        set => SetOrRemoveIfNull("fill", value);
    }

    public float? FillOpacity {
        get => Properties.TryGetValue("fill-opacity", out object? value) ? Convert.ToSingle(value) : null;
        set => SetOrRemoveIfNull("fill-opacity", value);
    }

    public string? MarkerColor {
        get => Properties.TryGetValue("marker-color", out object? value) ? value as string : null;
        set => SetOrRemoveIfNull("marker-color", value);
    }

    public string? MarkerSymbol {
        get => Properties.TryGetValue("marker-symbol", out object? value) ? value as string : null;
        set => SetOrRemoveIfNull("marker-symbol", value);
    }

    public string? Stroke {
        get => Properties.TryGetValue("stroke", out object? value) ? value as string : null;
        set => SetOrRemoveIfNull("stroke", value);
    }

    public float? StrokeWidth {
        get => Properties.TryGetValue("stroke-width", out object? value) ? Convert.ToSingle(value) : null;
        set => SetOrRemoveIfNull("stroke-width", value);
    }

    public float? StrokeOpacity {
        get => Properties.TryGetValue("stroke-opacity", out object? value) ? Convert.ToSingle(value) : null;
        set => SetOrRemoveIfNull("stroke-opacity", value);
    }

    public string? MarkerSize {
        get => Properties.TryGetValue("marker-size", out object? value) ? value as string : null;
        set => SetOrRemoveIfNull("marker-size", value);
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new properties object.
    /// </summary>
    public GeoJsonProperties() {
        Properties = new Dictionary<string, object>();
    }

    /// <summary>
    /// Initializes a new instance based on the specified <paramref name="properties"/>.
    /// </summary>
    /// <param name="properties">A dictionary with the properties.</param>
    public GeoJsonProperties(Dictionary<string, object> properties) {
        Properties = properties;
    }

    /// <summary>
    /// Initislizes a new instance from the specified <paramref name="json"/> object.
    /// </summary>
    /// <param name="json">The JSON object.</param>
    protected GeoJsonProperties(JObject? json) {
        Properties = json?.ToObject<Dictionary<string, object>>() ?? new Dictionary<string, object>();
    }

    #endregion

    #region Member methods

    private void SetOrRemoveIfNull(string key, object? value) {
        if (value is null) {
            Properties.Remove(key);
        } else {
            Properties[key] = value;
        }
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
        return Properties.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    #endregion

    #region Static methods

    /// <summary>
    /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonProperties"/>.
    /// </summary>
    /// <param name="json">The JSON object.</param>
    /// <returns>An instance of <see cref="GeoJsonProperties"/>.</returns>
    public static GeoJsonProperties Parse(JObject json) {
        return new GeoJsonProperties(json);
    }

    /// <summary>
    /// Attempts to parse the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonProperties"/>.
    /// </summary>
    /// <param name="json">The JSON object.</param>
    /// <param name="result">When this method returns, holds an instance of <see cref="GeoJsonProperties"/> if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public static bool Parse(JObject? json, out GeoJsonProperties? result) {

        if (json is null) {
            result = null;
            return false;
        }

        result = new GeoJsonProperties(json);
        return true;

    }

    #endregion

}