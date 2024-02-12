---
order: -9
---

# Lines

The `ILine` interface represents a line between two instances of `IPoint`. For instance, you could initialize a new line like:

```csharp
// Initialize two points by latitude and longitude
IPoint a = new Point(55.708151, 9.536131);
IPoint b = new Point(55.708069, 9.536000);

// Initialize a new line from the two points
ILine line = new Line(a, b);
```

The `ILine` interface gives you a few options - such as using the `GetLength` method for determining the length of said line:

```csharp
// Calculate the length of the line in metres
double length = line.GetLength();
```

The interface also defines a `GetCenter` method to get the center point of the line:

```csharp
// Calculate the center point of the line
IPoint center = line.GetCenter();
```

Finally you can also use the `GetBoundingBox` method to get an instance of `IRectangle` that contains the line:

```csharp
// Calculate the bounding box of the line
IRectangle boundingBox = line.GetBoundingBox();
```

## Line strings

Where the `ILine` interface represents a single line between two points, the `ILineString` interface describes a collection of connected lines - this is typically called a line string or a polyline (similar to a polygon).

```csharp
// Initialize the points by latitude and longitude
IPoint a = new Point(55.69924, 9.55798);
IPoint b = new Point(55.69341, 9.57227);
IPoint c = new Point(55.69065, 9.58866);
IPoint d = new Point(55.69338, 9.60939);

// Initialize a new line string from the four points
ILineString lineString = new LineString(a, b, c, d);
```

Similar to `ILine`, the `ILineString` interfaces lets you call the `GetLength`, `GetCenter` and `GetBoundingBox` methods:


```csharp
// Calculate the length of the line string in metres
double length = lineString.GetLength();

// Calculate the center point of the line string
IPoint center = lineString.GetCenter();

// Calculate the bounding box of the line string
IRectangle boundingBox = lineString.GetBoundingBox();
```
