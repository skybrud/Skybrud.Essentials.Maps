using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.GeoJson.Features;
using Skybrud.Essentials.Maps.GeoJson.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Json;

/// <summary>
/// JSON converter for serializing and deserializing <strong>GeoJSON</strong>.
/// </summary>
public class GeoJsonConverter : JsonConverter {

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {

        if (value == null) {
            writer.WriteNull();
            return;
        }

        switch (value) {

            case GeoJsonCoordinates coordinates:
                JArray.FromObject(coordinates.ToArray()).WriteTo(writer);
                return;

            case List<GeoJsonCoordinates> list:
                JArray.FromObject(list.Select(x => x.ToArray())).WriteTo(writer);
                return;

            case GeoJsonPoint point:
                new JObject {
                    {"type", point.Type.ToString() },
                    {"coordinates", JToken.FromObject(point.Coordinates.ToArray(), serializer)}
                }.WriteTo(writer);
                return;

            case GeoJsonMultiPoint multiPoint:
                new JObject {
                    {"type", multiPoint.Type.ToString() },
                    {"coordinates", JArray.FromObject(multiPoint.ToList(), serializer)}
                }.WriteTo(writer);
                return;

            case GeoJsonLineString lineString:
                new JObject {
                    {"type", lineString.Type.ToString() },
                    {"coordinates", JArray.FromObject(lineString.ToList(), serializer)}
                }.WriteTo(writer);
                return;

            case GeoJsonMultiLineString multiLineString:
                new JObject {
                    {"type", multiLineString.Type.ToString() },
                    {"coordinates", JArray.FromObject(multiLineString.ToList().Select(x => x.ToList()), serializer)}
                }.WriteTo(writer);
                return;

            case GeoJsonPolygon polygon:
                new JObject {
                    {"type", polygon.Type.ToString() },
                    {"coordinates", JArray.FromObject(polygon.Coordinates, serializer)}
                }.WriteTo(writer);
                return;

            case GeoJsonMultiPolygon multiPolygon:
                new JObject {
                    {"type", multiPolygon.Type.ToString() },
                    {"coordinates", JArray.FromObject(multiPolygon.Coordinates, serializer)}
                }.WriteTo(writer);
                return;

            case GeoJsonProperties properties:

                Dictionary<string, object> temp = new();
                foreach (KeyValuePair<string, object> pair in properties.Properties) {
                    if (pair.Value == null) continue;
                    temp.Add(pair.Key, pair.Value);
                }

                JObject.FromObject(temp).WriteTo(writer);
                return;

            default:
                writer.WriteNull();
                return;

        }

    }

    /// <inheritdoc />
    public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)  {

        if (reader.TokenType == JsonToken.Null) return null;
        if (reader.TokenType != JsonToken.StartObject) return null;

        JObject obj = JObject.Load(reader);

        string type = obj.Value<string>("type");

        switch (type?.ToLower()) {

            case "feature":
                return GeoJsonFeature.Parse(obj);

            case "featurecollection":
                return GeoJsonFeatureCollection.Parse(obj);

            case "point":
                return GeoJsonPoint.Parse(obj);

            case "multipoint":
                return GeoJsonMultiPoint.Parse(obj);

            case "linestring":
                return GeoJsonLineString.Parse(obj);

            case "multilinestring":
                return GeoJsonMultiLineString.Parse(obj);

            case "polygon":
                return GeoJsonPolygon.Parse(obj);

            case "multipolygon":
                return GeoJsonMultiPolygon.Parse(obj);

            default:
                if (objectType == typeof(GeoJsonProperties)) {
                    return ReadJsonProperties(reader);
                }
                throw new GeoJsonParseException($"Unknown shape: {type}", obj);

        }

    }

    /// <inheritdoc />
    public override bool CanConvert(Type objectType)  {
        return false;
    }

    private object ReadJsonProperties(JsonReader reader) {

        JObject obj = JObject.Load(reader);

        Dictionary<string, object> temp = obj.ToObject<Dictionary<string, object>>();

        return new GeoJsonProperties(temp);

    }

}