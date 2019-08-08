using Newtonsoft.Json;
using Skybrud.Essentials.Json.Converters.Enums;
using Skybrud.Essentials.Strings;

namespace Skybrud.Essentials.Maps.GeoJson {

    [JsonConverter(typeof(EnumStringConverter), TextCasing.PascalCase)]
    public enum GeoJsonType {

        Feature,
        FeatureCollection,

        GeometryCollection,

        Point,
        LineString,
        Polygon,

        MultiPoint,
        MultiLineString,
        MultiPolygon

    }

}