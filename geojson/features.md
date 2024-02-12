# Features

A feature is typically the root object, and can be either of the two following types:

## Feature

A GeoJSON **Feature** consists of a `type` property and a `geometry` property, but may also specify an optional `properties` property wil an object of custom properties.

Being a feature, the `type` property should always be `Feature` (the `GeoJsonFeature` class within this package will handle that for you). The `feature` property can be any of the [geometry types](./geometry) supported by GeoJSON.

A quick example of a feature is as shown below. The feature contains the geometry for a point. Notice that GeoJSON specifies coordinates as `x` and `y`, which corresponds with `longitude` and `latitude` (in that order), whereas one normally writes coordinates with `latitude` first, then `longitude`. The `properties` property is just a collection of custom properties, so it this case we can use `properties.name` to associate a name with the feature/point.

```json
{
  "type": "Feature",
  "geometry": {
    "type": "Point",
    "coordinates": [9.536067, 55.708116]
  },
  "properties": {
    "name": "Skybrud.dk HQ"
  }
}
```

To generate a JSON string like in the example above, you can use the `GeoJsonFeature`, `GeoJsonPoint` and `GeoJsonProperties` classes from this package:

```csharp
// Initialize a new feature for a point
GeoJsonFeature feature = new GeoJsonFeature {
    Geometry = new GeoJsonPoint(9.536067, 55.708116),
    Properties = new GeoJsonProperties {
        Name = "Skybrud.dk HQ"
    }
};

// Convert the feature to a JSON string
string json = feature.ToJson(Formatting.Indented);
```


## Feature Collection

Where a **Feature** specifies a geometry on the map, a **FeatureCollection** on the other hand can contain an array of **Feature** objects.

For a feature collection containing the feature from before, the JSON wil now look as:

```json
{
  "type": "FeatureCollection",
  "features": [
    {
      "type": "Feature",
      "geometry": {
        "type": "Point",
        "coordinates": [9.536067, 55.708116]
      },
      "properties": {
        "name": "Skybrud.dk HQ"
      }
    }
  ]
}
```

And the code for generating that JSON:

```csharp
// Initialize a new feature collection
GeoJsonFeatureCollection collection = new GeoJsonFeatureCollection();

// Add a new feature to the collection
collection.Features.Add(new GeoJsonFeature {
    Geometry = new GeoJsonPoint(9.536067, 55.708116),
    Properties = new GeoJsonProperties {
        Name = "Skybrud.dk HQ"
    }
});

// Convert the feature to a JSON string
string json = collection.ToJson(Formatting.Indented);
```

## Properties

The `properties` object is a collection of child properties of various types supported by JSON - it may be simple types such as numbers, booleans and strings, but more comples types such as arrays and objects are allowed as well.

With this package, the `properties` object is represented by the `GeoJsonProperties` class. It wraps around a standard instance of `Dictionary<string, object>`, so you may add a key with a value of any type to it, and JSON.net will do the rest when serializing to JSON:

```csharp
// Initialize a new properties dictionary
GeoJsonProperties properties = new GeoJsonProperties();

// Set the "name" and "address" properties
properties["name"] = "Skybrud.dk HQ";
properties["address"] = new {
    street = "Dæmningen 36",
    postal = "7100",
    city = "Vejle"
};
```

GeoJSON does not specify the format of any child properties of the `properties` property, so this is fully up to you. There does however seems to be some somewhat standardized properties, and as such the `GeoJsonProperties` class contains the following properties:

- **Name** (serialized as `name`)
- **Description** (serialized as `description`)
- **Fill** (serialized as `fill`)
- **FillOpacity** (serialized as `fill-opacity`)
- **MarkerColor** (serialized as `marker-color`)
- **MarkerSymbol** (serialized as `marker-symbol`)
- **Stroke** (serialized as `stroke`)
- **StrokeWidth** (serialized as `stroke-width`)
- **StrokeOpacity** (serialized as `stroke-opacity`)
- **MarkerSize** (serialized as `marker-size`)

Most of these are used for the visual presentation of the geometry of a **Feature**.

According to the GeoJSON specification, only a **Feature** may specify the `properties` property. But most GeoJSON parses will simply ignore any additional properties, so it should be somewhat safe to specify the `properties` property on a **FeatureCollection** as well.
