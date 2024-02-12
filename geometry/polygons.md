---
order: -7
---

# Polygons

A polygon is a closed path shape of multiple points and for which the first and last point of the path is the same (hence being closed). In this package, a polygon is described by the `IPolygon` interface.

```csharp
// Initialize the points that make of the polygon
IPoint a = new Point(55.710583, 9.523840);
IPoint b = new Point(55.703988, 9.535406);
IPoint c = new Point(55.706689, 9.544073);
IPoint d = new Point(55.712944, 9.538897);
IPoint e = new Point(55.715683, 9.529235);

// Initialize the polygon (notice that "a" is specified twice)
IPolygon polygon = new Polygon(a, b, c, d, e, a);
```

## Inner coordinates

The example above specifies the outer coordinates of the polygon. In many cases, to fully represent a shape, it may also be needed to specify one or more sets of inner coordinates, which then represents parts of the polygon that should be excluded.

For instance in Danmark, Frederiksberg Municipality is located inside Copenhagen Municipality, and as such the part of Frederiksberg should be excluded from the shape of Copenhagen.

![image](https://user-images.githubusercontent.com/3634580/62412130-7c1b5f00-b5fe-11e9-91ad-0ffd3789d2c9.png) *Multiple polygons making up the shape of Copenhagen Municipality. Source: Google Maps & kortforsyningen.dk*

The shape for Copenhagen Municipality has many points, so for a more simple example, you could initialize the polygon like shown below:

```csharp
// Initialize an array with the outer points
Point[] outer = {
    new Point(55.710583, 9.523840),
    new Point(55.703988, 9.535406),
    new Point(55.706689, 9.544073),
    new Point(55.712944, 9.538897),
    new Point(55.715683, 9.529235),
    new Point(55.710583, 9.523840)
};

// Initialize an array with the first set of inner points
Point[] inner1 = {
    new Point(55.715121, 9.529524),
    new Point(55.710649, 9.524975),
    new Point(55.712631, 9.537978),
    new Point(55.715121, 9.529524)
};

// Initialize an array with the second set of inner points
Point[] inner2 = {
    new Point(55.704411, 9.535317),
    new Point(55.706780, 9.543171),
    new Point(55.709609, 9.540682),
    new Point(55.704411, 9.535317)
};

// Initialize the polygon
IPolygon polygon = new Polygon(outer, inner1, inner2);
```

You can add as many inner polygons as you need - as long as they are inside the outer coordinates of the polygon.

## Multi polygons

As seen for from the exmaples above, a shape may also consist of multiple polygons. Copenhagen Municipality is made up of five polygons - two large polygons on Zealand and Amager respectively and then three smaller polygons making up the islands in Øresund.

You can initialize a new multi polygon shape like shown below:

```csharp
// The first polygon
Point[] first = {
    new Point(55.710583, 9.523840),
    new Point(55.703988, 9.535406),
    new Point(55.706689, 9.544073),
    new Point(55.712944, 9.538897),
    new Point(55.715683, 9.529235),
    new Point(55.710583, 9.523840),
};

// The second polygon
Point[] second = {
    new Point(55.715121, 9.529524),
    new Point(55.710649, 9.524975),
    new Point(55.712631, 9.537978),
    new Point(55.715121, 9.529524)
};

// The third polygon
Point[] third = {
    new Point(55.704411, 9.535317),
    new Point(55.706780, 9.543171),
    new Point(55.709609, 9.540682),
    new Point(55.704411, 9.535317)
};

// Initialize the multi polygon
IMultiPolygon multi = new MultiPolygon(new Polygon(first), new Polygon(second), new Polygon(third));
```

## Methods

Both the `IPolygon` and `IMultiPolygon` interfaces inherit the following methods from the `IShape` interface:

**Contains**  
To test whether a given point is contained within the polygon, you can use the `Contains` method as shown below:

```csharp
// Initialize the point
IPoint p = new Point(55.708119, 9.536085);

// Does the rectangle contain "p"?
bool contains = polygon.Contains(p);
```

**Bounding box**  
Calling the `GetBoundingBox` on the polygon will return a rectangle that contains the polygon:

```csharp
// Get the bounding box
IRectangle bbox = polygon.GetBoundingBox();
```

**Center**  
The `IShape` interface also specifies the `GetCenter` method:

```csharp
// Get the center point of the polygon
IPoint center = polygon.GetCenter();
```

Notice that for some shapes, the center point will naturally be located within the shape. For a polygon, this however depends on the path, so for some polygons, the center point may be located outside the polygon it self.

**GetCircumference** and **GetArea**  
Finally the interface also specifies the `GetCircumference` and `GetArea` methods:

```csharp
// Get the circumference (in metres)
double circumference = polygon.GetCircumference();

// Get the area (in square metres)
double area = polygon.GetArea();
```

As this package uses [**SI units**](https://en.wikipedia.org/wiki/International_System_of_Units), the results from the `GetCircumference` and `GetArea` methods will be measured in metres and square metres respectively.
