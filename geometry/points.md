---
order: -10
---

# Points

When working with the `Skybrud.Essentials.Maps.Geometry` namespace, the <code type="Skybrud.Essentials.Maps.Geometry.IPoint, Skybrud.Essentials">IPoint</code> interface is the most basic type. By specifying the latitude and logitude properties, it defines a point in the world.

The interface let's you declare your own classes that implement it - eg. in situations where an instance should specify more information than just the latitude and longitude. In most cases, you can however use the <code type="Skybrud.Essentials.Maps.Geometry.Point, Skybrud.Essentials">Point</code> class:

```csharp
// Initialize two points by latitude and longitude
IPoint a = new Point(55.708151, 9.536131);
IPoint b = new Point(55.708069, 9.536000);
```

Besides telling you where a given point is in the world, an instance of <code type="Skybrud.Essentials.Maps.Geometry.IPoint, Skybrud.Essentials">IPoint</code> doesn't really do much more than that. But by using the `DinstanceUtils` class, you can determine the distance between two points:

```csharp
// Calculate the distance in metres
double distance = DistanceUtils.GetDistance(a, b);
```

The distance is measured in metres as it's an [**SI unit**](https://en.wikipedia.org/wiki/International_System_of_Units).
