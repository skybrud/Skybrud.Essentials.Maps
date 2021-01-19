using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace UnitTestProject1.GeoJson.Geometries {
    
    [TestClass]
    public class GeoJsonMultiPolygonTests {

        [TestMethod]
        public void Constructor1() {

            GeoJsonMultiPolygon multiPolygon = new GeoJsonMultiPolygon();
            
            Assert.AreEqual(GeoJsonType.MultiPolygon, multiPolygon.Type);

            Assert.AreEqual(0, multiPolygon.Count);

        }

        [TestMethod]
        public void Constructor2() {

            var outer1 = new [] {
                new [] { 9.531275, 55.714505 },
                new [] { 9.531503, 55.714701 },
                new [] { 9.531278, 55.714791 },
                new [] { 9.531038, 55.714599 },
                new [] { 9.531275, 55.714505 }
            };

            var outer2 = new [] {
                new [] { 9.531355, 55.714570 },
                new [] { 9.531450, 55.714528 },
                new [] { 9.531613, 55.714660 },
                new [] { 9.531504, 55.714702 },
                new [] { 9.531355, 55.714570 }
            };

            var coordinates = new []  {
                new [] { outer1 },
                new [] { outer2 }
            };


            GeoJsonMultiPolygon multiPolygon = new GeoJsonMultiPolygon(coordinates);
            
            Assert1(multiPolygon);

        }

        [TestMethod]
        public void Constructor3() {

            IPolygon polygon1 = new Polygon(
                new Point(55.714505, 9.531275),
                new Point(55.714701, 9.531503),
                new Point(55.714791, 9.531278),
                new Point(55.714599, 9.531038),
                new Point(55.714505, 9.531275)
            );

            IPolygon polygon2 = new Polygon(
                new Point(55.714570, 9.531355),
                new Point(55.714528, 9.531450),
                new Point(55.714660, 9.531613),
                new Point(55.714702, 9.531504),
                new Point(55.714570, 9.531355)
            );

            IPolygon[] array = { polygon1, polygon2};

            List<IPolygon> list = new List<IPolygon> { polygon1, polygon2 };

            GeoJsonMultiPolygon multiPolygon1 = new GeoJsonMultiPolygon(array);

            GeoJsonMultiPolygon multiPolygon2 = new GeoJsonMultiPolygon(list);

            Assert1(multiPolygon1);
            
            Assert1(multiPolygon2);

        }

        [TestMethod]
        public void Constructor4() {

            var polygon1 = new GeoJsonPolygon(
                new GeoJsonCoordinates(9.531275, 55.714505),
                new GeoJsonCoordinates(9.531503, 55.714701),
                new GeoJsonCoordinates(9.531278, 55.714791),
                new GeoJsonCoordinates(9.531038, 55.714599),
                new GeoJsonCoordinates(9.531275, 55.714505)
            );

            var polygon2 = new GeoJsonPolygon(
                new GeoJsonCoordinates(9.531355, 55.714570),
                new GeoJsonCoordinates(9.531450, 55.714528),
                new GeoJsonCoordinates(9.531613, 55.714660),
                new GeoJsonCoordinates(9.531504, 55.714702),
                new GeoJsonCoordinates(9.531355, 55.714570)
            );

            GeoJsonMultiPolygon multiPolygon = new GeoJsonMultiPolygon(polygon1, polygon2);
            
            Assert1(multiPolygon);

        }

        [TestMethod]
        public void Constructor5() {

            var polygon1 = new GeoJsonPolygon(
                new GeoJsonCoordinates(9.531275, 55.714505),
                new GeoJsonCoordinates(9.531503, 55.714701),
                new GeoJsonCoordinates(9.531278, 55.714791),
                new GeoJsonCoordinates(9.531038, 55.714599),
                new GeoJsonCoordinates(9.531275, 55.714505)
            );

            var polygon2 = new GeoJsonPolygon(
                new GeoJsonCoordinates(9.531355, 55.714570),
                new GeoJsonCoordinates(9.531450, 55.714528),
                new GeoJsonCoordinates(9.531613, 55.714660),
                new GeoJsonCoordinates(9.531504, 55.714702),
                new GeoJsonCoordinates(9.531355, 55.714570)
            );

            List<GeoJsonPolygon> polygons = new List<GeoJsonPolygon> { polygon1, polygon2 };

            GeoJsonMultiPolygon multiPolygon = new GeoJsonMultiPolygon(polygons);
            
            Assert1(multiPolygon);

        }

        [TestMethod]
        public void Parse1() {

            GeoJsonMultiPolygon multiPolygon = GeoJsonMultiPolygon.Parse(Json1);

            Assert1(multiPolygon);

        }

        [TestMethod]
        public void Deserialize1() {

            GeoJsonMultiPolygon multiPolygon = JsonConvert.DeserializeObject<GeoJsonMultiPolygon>(Json1);

            Assert1(multiPolygon);

        }

        [TestMethod]
        public void ToJson1() {

            GeoJsonPolygon polygon1 = new GeoJsonPolygon(
                new GeoJsonCoordinates(9.531275, 55.714505),
                new GeoJsonCoordinates(9.531503, 55.714701),
                new GeoJsonCoordinates(9.531278, 55.714791),
                new GeoJsonCoordinates(9.531038, 55.714599),
                new GeoJsonCoordinates(9.531275, 55.714505)
            );

            GeoJsonPolygon polygon2 = new GeoJsonPolygon(
                new GeoJsonCoordinates(9.531355, 55.714570),
                new GeoJsonCoordinates(9.531450, 55.714528),
                new GeoJsonCoordinates(9.531613, 55.714660),
                new GeoJsonCoordinates(9.531504, 55.714702),
                new GeoJsonCoordinates(9.531355, 55.714570)
            );

            GeoJsonMultiPolygon multiPolygon = new GeoJsonMultiPolygon(polygon1, polygon2);

            Assert.AreEqual(2, multiPolygon.Count);

            Assert.AreEqual(Json1, multiPolygon.ToJson(Formatting.None));

        }

        private void Assert1(GeoJsonMultiPolygon multiPolygon) {
            
            Assert.AreEqual(GeoJsonType.MultiPolygon, multiPolygon.Type);

            Assert.AreEqual(2, multiPolygon.Count);

            GeoJsonPolygon polygon1 = multiPolygon[0];

            Assert.AreEqual(5, polygon1.Outer.Count);
            Assert.AreEqual(0, polygon1.Inner.Count);

            Assert.AreEqual(9.531275, polygon1.Outer[0].X, "Polygon 1 - #1 X");
            Assert.AreEqual(55.714505, polygon1.Outer[0].Y, "Polygon 1 - #1 X");

            Assert.AreEqual(9.531503, polygon1.Outer[1].X, "Polygon 1 - #2 X");
            Assert.AreEqual(55.714701, polygon1.Outer[1].Y, "Polygon 1 - #2 X");

            Assert.AreEqual(9.531278, polygon1.Outer[2].X, "Polygon 1 - #3 X");
            Assert.AreEqual(55.714791, polygon1.Outer[2].Y, "Polygon 1 - #3 X");

            Assert.AreEqual(9.531038, polygon1.Outer[3].X, "Polygon 1 - #4 X");
            Assert.AreEqual(55.714599, polygon1.Outer[3].Y, "Polygon 1 - #4 X");

            Assert.AreEqual(9.531275, polygon1.Outer[4].X, "Polygon 1 - #5 X");
            Assert.AreEqual(55.714505, polygon1.Outer[4].Y, "Polygon 1 - #5 X");

            GeoJsonPolygon polygon2 = multiPolygon[1];

            Assert.AreEqual(5, polygon2.Outer.Count);
            Assert.AreEqual(0, polygon2.Inner.Count);

            Assert.AreEqual(9.531355, polygon2.Outer[0].X, "Polygon 2 - #1 X");
            Assert.AreEqual(55.714570, polygon2.Outer[0].Y, "Polygon 2 - #1 X");

            Assert.AreEqual(9.531450, polygon2.Outer[1].X, "Polygon 2 - #2 X");
            Assert.AreEqual(55.714528, polygon2.Outer[1].Y, "Polygon 2 - #2 X");

            Assert.AreEqual(9.531613, polygon2.Outer[2].X, "Polygon 2 - #3 X");
            Assert.AreEqual(55.714660, polygon2.Outer[2].Y, "Polygon 2 - #3 X");

            Assert.AreEqual(9.531504, polygon2.Outer[3].X, "Polygon 2 - #4 X");
            Assert.AreEqual(55.714702, polygon2.Outer[3].Y, "Polygon 2 - #4 X");

            Assert.AreEqual(9.531355, polygon2.Outer[4].X, "Polygon 2 - #5 X");
            Assert.AreEqual(55.714570, polygon2.Outer[4].Y, "Polygon 2 - #5 X");

        }

        const string Json1 = "{\"type\":\"MultiPolygon\",\"coordinates\":[[[[9.531275,55.714505],[9.531503,55.714701],[9.531278,55.714791],[9.531038,55.714599],[9.531275,55.714505]]],[[[9.531355,55.71457],[9.53145,55.714528],[9.531613,55.71466],[9.531504,55.714702],[9.531355,55.71457]]]]}";

    }

}