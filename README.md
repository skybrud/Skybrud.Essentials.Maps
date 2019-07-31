# Skybrud.Essentials.Maps

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) <!--[![NuGet](https://img.shields.io/nuget/v/Skybrud.Essentials..Maps.svg)](https://www.nuget.org/packages/Skybrud.Essentials.Maps) [![NuGet](https://img.shields.io/nuget/dt/Skybrud.Essentials.Maps.svg)](https://www.nuget.org/packages/Skybrud.Essentials.Maps)-->

Skybrud.Essentials.Maps is a .NET package for working with maps and geospatial data and geometry.

It also supports working with popular formats such as [**GeoJSON**](https://en.wikipedia.org/wiki/GeoJSON), [**KML**](https://en.wikipedia.org/wiki/Keyhole_Markup_Language) (Keyhole Markup Language) and [**WKT**](https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry) (Well Known Text).

### Installation

To install the Skybrud.Essentials.Maps, simply pick one of the methods below:

1. [**NuGet Package**][NuGetPackage]  
   Install this NuGet package in your Visual Studio project. Makes updating easy.
2. [**ZIP file**][GitHubRelease]  
   Grab a ZIP file of the latest release; unzip and move the `Skybrud.Essentials.Maps.dll`and other DLLs matching your target framework to the bin directory of your project.



### Dependencies

- [**Skybrud.Essentials**](https://github.com/skybrud/Skybrud.Essentials)<br />A package with logic for handling various common tasks in .NET.

  - [**Json.NET**](https://github.com/jamesnk/newtonsoft.json)<br />Used for searializing/deserializing JSON.

- [**Skybrud.Essentials.Http**](https://github.com/skybrud/Skybrud.Essentials.Http)<br />HTTP package used for resolving network links in KML files.



### Found a bug? Have a question?

* Please feel free to [**create an issue**][Issues], and I will get back to you ;)



### Changelog

The [**releases page**][GitHubReleases] lists the relevant changes from each release.

### Usage

#### Geometry, lines and shapes

Define a point on a map:

```csharp
// Initialize a new point by latitude and longitude
IPoint point = new Point(55.708151, 9.536131);
```

Calculate the distance between two points:

```csharp
// Initialize the points by latitude and longitude
IPoint a = new Point(55.708151, 9.536131);
IPoint b = new Point(55.708069, 9.536000);

// Calculate the distance in metres
double distance = DistanceUtils.GetDistance(a, b);
```

Define a line on a map:

```csharp
// Initialize the points by latitude and longitude
IPoint a = new Point(55.708151, 9.536131);
IPoint b = new Point(55.708069, 9.536000);

// Initialize a new line from the two points
ILine line = new Line(a, b);

// Get the length of the line
double length = line.GetLength();
```

Define a line string (polyline) on a map:

```csharp
// Initialize the points by latitude and longitude
IPoint a = new Point(55.708151, 9.536131);
IPoint b = new Point(55.708069, 9.536000);
IPoint c = new Point(55.708061, 9.536172);

// Initialize a new line string from the multiple points
ILineString line = new LineString(a, b, c);

// Get the length of the line string
double length = line.GetLength();
```
Define a polygon on a map:

```csharp
// Initialize the points by latitude and longitude
IPoint a = new Point(55.708151, 9.536131);
IPoint b = new Point(55.708069, 9.536000);
IPoint c = new Point(55.708061, 9.536172);
IPoint d = new Point(55.708145, 9.536222);

IPoint f = new Point(55.708113, 9.536086);

// Initialize a new polygon from the multiple points
IPolygon polygon = new Polygon(a, b, c, d, a);

// Get the circumferences of the polygon
double circumference = polygon.GetCircumference();

// Get the area of the polygon
double area = polygon.GetArea();

// Get the bounding of the polygon
IRectangle bbox = polygon.GetBoundingBox();

// Does the polygon contain point "f"?
bool contains = polygon.Contains(f);
```





   
[NuGetPackage]: https://www.nuget.org/packages/Skybrud.Essentials.Maps
[GitHubRelease]: https://github.com/skybrud/Skybrud.Essentials.Maps/releases/latest
[GitHubReleases]: https://github.com/skybrud/Skybrud.Essentials.Maps/releases
[Changelog]: https://github.com/skybrud/Skybrud.Essentials.Maps/releases
[Issues]: https://github.com/skybrud/Skybrud.Essentials.Maps/issues
