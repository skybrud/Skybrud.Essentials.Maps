using Newtonsoft.Json;
using Skybrud.Essentials.Json.Converters.Enums;
using Skybrud.Essentials.Strings;

namespace Skybrud.Essentials.Maps.GeoJson {

    /// <summary>
    /// Enum class indicating the type of a <strong>GeoJson</strong> object.
    /// </summary>
    [JsonConverter(typeof(EnumStringConverter), TextCasing.PascalCase)]
    public enum GeoJsonType {

        /// <summary>
        /// Indicates that the object is a <strong>Feature</strong>.
        /// </summary>
        Feature,
        
        /// <summary>
        /// Indicates that the object is a <strong>FeatureCollection</strong>.
        /// </summary>
        FeatureCollection,

        /// <summary>
        /// Indicates that the object is a <strong>GeometryCollection</strong>.
        /// </summary>
        GeometryCollection,

        /// <summary>
        /// Indicates that the object is a <strong>Point</strong>.
        /// </summary>
        Point,

        /// <summary>
        /// Indicates that the object is a <strong>LineString</strong>.
        /// </summary>
        LineString,

        /// <summary>
        /// Indicates that the object is a <strong>Polygon</strong>.
        /// </summary>
        Polygon,

        /// <summary>
        /// Indicates that the object is a <strong>MultiPoint</strong>.
        /// </summary>
        MultiPoint,

        /// <summary>
        /// Indicates that the object is a <strong>MultiLineString</strong>.
        /// </summary>
        MultiLineString,

        /// <summary>
        /// Indicates that the object is a <strong>MultiPolygon</strong>.
        /// </summary>
        MultiPolygon

    }

}