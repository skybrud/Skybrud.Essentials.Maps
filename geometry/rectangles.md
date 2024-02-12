---
order: -8
---

# Rectangles

With this package, you can use the `IRectangle` interface or the `Rectangle` class to represent a rectangle on a map. A rectangle is identified by two points - the south west point (bottom left corner) and the north east point (top right corner):

```csharp
// Initialize the two points
IPoint southWest = new Point(55.691582, 9.488185);
IPoint northEast = new Point(55.721617, 9.584030);

// Initialize the rectangle
IRectangle rectangle = new Rectangle(southWest, northEast);
```

Normally the south west point would have lower latitude and longitude values than the north east corner, but it may not always be the case. For instance, New Zealand spans across the line of <code>-180/+180</code> longitude, and as such the longitude of the south west corner will have a higher value the longitude of the north east corner:

```csharp
// Initialize the two points
IPoint southWest = new Point(-52.619419, 165.869437);
IPoint northEast = new Point(-29.231342, -175.831536);

// Initialize the rectangle
IRectangle rectangle = new Rectangle(southWest, northEast);
```

## Methods

The `IRectangle` interface inherits the following methods from the `IShape` interface:

**Contains**  
To test whether a given point is contained within the rectangle, you can use the `Contains` method as shown below:

```csharp
// Initialize the point
IPoint p = new Point(55.708119, 9.536085);

// Does the rectangle contain "p"?
bool contains = rectangle.Contains(p);
```

**Bounding box**  
As the `IShape` interface describes a generic shape, it specifies the `GetBoundingBox` method. For a rectangle, this will however result in an identical rectangle:

```csharp
// Get the bounding box
IRectangle bbox = rectangle.GetBoundingBox();
```

The `IShape` interface also specifies the `GetCenter`, `GetCircumference` and `GetArea` methods:

```csharp
// Get the center point of the rectangle
IPoint center = rectangle.GetCenter();

// Get the circumference (in metres)
double circumference = rectangle.GetCircumference();

// Get the area (in square metres)
double area = rectangle.GetArea();
```

As this package uses [**SI units**](https://en.wikipedia.org/wiki/International_System_of_Units), the results from the `GetCircumference` and `GetArea` methods will be measured in metres and square metres respectively.
