---
order: 999
---

# Utils

The static <code type="Skybrud.Essentials.Maps.GeoJson.GeoJsonUtils, Skybrud.Essentials.Maps">GeoJsonUtils</code> utilities class lets you do a bunch of different stuff in relation to GeoJSON.

## Parsing

The <code method="Skybrud.Essentials.Maps.GeoJson.GeoJsonUtils.Parse, Skybrud.Essentials.Maps">GeoJsonUtils.Parse</code> methods lets you parse a raw JSON string into the corresponding <code type="Skybrud.Essentials.Maps.GeoJson.GeoJsonObject, Skybrud.Essentials.Maps">GeoJsonObject</code>.

If you have a JSON string with a feature or geometry, you can use the method like:

```csharp
// Parse the JSON
GeoJsonObject obj = GeoJsonUtils.Parse("{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}");
```

In this example, we can see that the JSON string represents a **Point**, so the result will be an instance of <code type="Skybrud.Essentials.Maps.GeoJson.Geometry.GeoJsonPoint, Skybrud.Essentials.Maps">GeoJsonObject</code> (which is a subclass of <code type="Skybrud.Essentials.Maps.GeoJson.GeoJsonObject, Skybrud.Essentials.Maps">GeoJsonObject</code>).

An overload lets you specify the return type via a generic type parameter:

```csharp
// Parse the JSON
GeoJsonPoint obj = GeoJsonUtils.Parse<GeoJsonPoint>("{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}");
```

## Loading

Where the <code method="Skybrud.Essentials.Maps.GeoJson.GeoJsonUtils.Parse, Skybrud.Essentials.Maps">GeoJsonUtils.Parse</code> methods can be used for parsing a raw JSON string that you already have in memory, you can use the similar <code method="Skybrud.Essentials.Maps.GeoJson.GeoJsonUtils.Load, Skybrud.Essentials.Maps">GeoJsonUtils.Load</code> methods to load a JSON file from disk:

```csharp
// Parse the JSON
GeoJsonObject obj = GeoJsonUtils.Parse("C:/MyGeoJsonPoint.json");
```

And again with a generic type parameter:

```csharp
// Parse the JSON
GeoJsonPoint obj = GeoJsonUtils.Load<GeoJsonPoint>("C:/MyGeoJsonPoint.json");
```
