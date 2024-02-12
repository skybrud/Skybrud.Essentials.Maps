---
order: 1
---

# Point

In GeoJSON, a **Point** is the simplest of the supported geometries, as it refers to a single point on a map - identified by it's `x` (longitude) and `y` (latitude) coordinates.

With this package, a **Point** is represented by the <code type="Skybrud.Essentials.Maps.GeoJson.Geometry.GeoJsonPoint, Skybrud.Essentials.Maps">GeoJsonPoint</code> class. It may be initialized as:

```csharp
// Initialize a new point
GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);
```

The <code type="Skybrud.Essentials.Maps.GeoJson.Geometry.GeoJsonPoint, Skybrud.Essentials.Maps">GeoJsonPoint</code> class has a few different constructor overloads, so you may also use one of the following constructors instead:

**With a third parameter for the altitude**  
The parameters are specified in the order of `x`, `y` and `altitude`:

```csharp
// Initialize a new point
GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116, 0);
```

**Based on a double array**  
The array may have either two or three items depending on whether the altitude is specified. The first item is the `x` coordinate, the second item is the `y` coordinate, and the third item may be the optional altitude item.

```csharp
// Initialize a new point
GeoJsonPoint point = new GeoJsonPoint(new []{ 9.536067, 55.708116 });
```

**Based on an instance of IPoint**  
If you already have an instance of <code type="Skybrud.Essentials.Maps.Geometry.IPoint, Skybrud.Essentials">IPoint</code> you can use that as the first parameter for the constructor:


```csharp
// Initialize a new point
IPoint p = new Point(55.708116, 9.536067);

// Convert the IPoint to a GeoJsonPoint
GeoJsonPoint point = new GeoJsonPoint(p);
```


## Converting

One of the main purposes of this package is being able to convert back and forth between different formats. As an example, the `GeoJsonPoint.ToJson` method lets you convert a **Point** to a JSON string:

```csharp
// Initialize a new point
GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);

// Convert the point to a JSON string
string json = point.ToJson(Formatting.Indented);
```

With the example point above, the generated JSON would look like:

```json
{
  "type": "Point",
  "coordinates": [
    9.536067,
    55.708116
  ]
}
```

Or if you need to save the JSON to a file on your disk instead, there is also the `GeoJsonPoint.Save` method:

```csharp
// Initialize a new point
GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);

// Save the JSON to disk
point.Save("C:/MyPoint.json", Formatting.Indented);
```

The parameters are `path` and `formatting` respectively. The latter is used to specify the formatting of the JSON - it is optional, so if not specified, <code field="Newtonsoft.Json.Formatting.Indented">Formatting.Indented</code> is assumed.

To go the other way around, you can use the static `GeoJsonPoint.Parse` and `GeoJsonPoint.Load` methods to parse/load a JSON string into an instance of `GeoJsonPoint`:

```csharp
// Parse a raw JSON string into a new instance
GeoJsonPoint point = GeoJsonPoint.Parse("{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}");
```

```csharp
// Load the raw JSON string form the disk
GeoJsonPoint point = GeoJsonPoint.Load("C:/MyPoint.json");
```

Finally, you can also convert a <code type="Skybrud.Essentials.Maps.GeoJson.Geometry.GeoJsonPoint, Skybrud.Essentials.Maps">GeoJsonPoint</code> into an instance of <code type="Skybrud.Essentials.Maps.Geometry.IPoint">IPoint</code>:

```csharp
// Initialize a new point
GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);

// Convert "point" to an instance of IPoint
IPoint p = point.ToPoint();
```
