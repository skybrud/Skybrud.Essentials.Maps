using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;

// ReSharper disable InconsistentNaming

namespace UnitTestProject1.GeoJson.Geometries {
    
    [TestClass]
    public class GeoJsonMultiLineStringTests {

        [TestMethod]
        public void Constructor1() {

            GeoJsonMultiLineString multiLineString = new GeoJsonMultiLineString();

            Assert.AreEqual(0, multiLineString.Count);

        }

        [TestMethod]
        public void Constructor2() {

            double[][][] array = {
                new [] {
                    new [] { 9.531255, 55.714869 },
                    new [] { 9.530941, 55.714614 },
                    new [] { 9.531290, 55.714482 }
                },
                new [] {
                    new [] { 9.531613, 55.714659 },
                    new [] { 9.531278, 55.714791 },
                    new [] { 9.531041, 55.714600 },
                    new [] { 9.531275, 55.714505 },
                    new [] { 9.531354, 55.714571 },
                    new [] { 9.531452, 55.714529 }
                }
            };

            GeoJsonMultiLineString multiLineString = new GeoJsonMultiLineString(array);

            Assert1(multiLineString);

        }

        [TestMethod]
        public void Constructor3() {

            var p1a = new Point(55.714869, 9.531255);
            var p1b = new Point(55.714614, 9.530941);
            var p1c = new Point(55.714482, 9.531290);

            LineString lineString1 = new LineString(p1a, p1b, p1c);

            var p2a = new Point(55.714659, 9.531613);
            var p2b = new Point(55.714791, 9.531278);
            var p2c = new Point(55.714600, 9.531041);
            var p2d = new Point(55.714505, 9.531275);
            var p2e = new Point(55.714571, 9.531354);
            var p2f = new Point(55.714529, 9.531452);

            LineString lineString2 = new LineString(p2a, p2b, p2c, p2d, p2e, p2f);

            List<LineString> list = new List<LineString> { lineString1, lineString2 };

            GeoJsonMultiLineString multiLineString = new GeoJsonMultiLineString(list);

            Assert1(multiLineString);

        }

        [TestMethod]
        public void Constructor4() {

            var p1a = new GeoJsonCoordinates(9.531255, 55.714869);
            var p1b = new GeoJsonCoordinates(9.530941, 55.714614);
            var p1c = new GeoJsonCoordinates(9.531290, 55.714482);

            GeoJsonLineString lineString1 = new GeoJsonLineString(p1a, p1b, p1c);

            var p2a = new GeoJsonCoordinates(9.531613, 55.714659);
            var p2b = new GeoJsonCoordinates(9.531278, 55.714791);
            var p2c = new GeoJsonCoordinates(9.531041, 55.714600);
            var p2d = new GeoJsonCoordinates(9.531275, 55.714505);
            var p2e = new GeoJsonCoordinates(9.531354, 55.714571);
            var p2f = new GeoJsonCoordinates(9.531452, 55.714529);

            GeoJsonLineString lineString2 = new GeoJsonLineString(p2a, p2b, p2c, p2d, p2e, p2f);

            GeoJsonMultiLineString multiLineString = new GeoJsonMultiLineString(lineString1, lineString2);

            Assert1(multiLineString);

        }

        [TestMethod]
        public void Constructor5() {

            var p1a = new GeoJsonCoordinates(9.531255, 55.714869);
            var p1b = new GeoJsonCoordinates(9.530941, 55.714614);
            var p1c = new GeoJsonCoordinates(9.531290, 55.714482);

            GeoJsonLineString lineString1 = new GeoJsonLineString(p1a, p1b, p1c);

            var p2a = new GeoJsonCoordinates(9.531613, 55.714659);
            var p2b = new GeoJsonCoordinates(9.531278, 55.714791);
            var p2c = new GeoJsonCoordinates(9.531041, 55.714600);
            var p2d = new GeoJsonCoordinates(9.531275, 55.714505);
            var p2e = new GeoJsonCoordinates(9.531354, 55.714571);
            var p2f = new GeoJsonCoordinates(9.531452, 55.714529);

            GeoJsonLineString lineString2 = new GeoJsonLineString(p2a, p2b, p2c, p2d, p2e, p2f);

            List<GeoJsonLineString> list = new List<GeoJsonLineString> { lineString1, lineString2 };

            GeoJsonMultiLineString multiLineString = new GeoJsonMultiLineString(list);

            Assert1(multiLineString);

        }

        [TestMethod]
        public void Parse1() {

            GeoJsonMultiLineString multiLineString = GeoJsonMultiLineString.Parse(Json1);

            Assert1(multiLineString);

        }

        [TestMethod]
        public void Deserialize1() {

            GeoJsonMultiLineString multiLineString = JsonConvert.DeserializeObject<GeoJsonMultiLineString>(Json1);

            Assert1(multiLineString);

        }

        [TestMethod]
        public void ToJson1() {

            GeoJsonLineString lineString1 = new GeoJsonLineString {
                new GeoJsonCoordinates(9.531255, 55.714869),
                new GeoJsonCoordinates(9.530941, 55.714614),
                new GeoJsonCoordinates(9.53129, 55.714482)
            };

            GeoJsonLineString lineString2 = new GeoJsonLineString {
                new GeoJsonCoordinates(9.531613, 55.714659),
                new GeoJsonCoordinates(9.531278, 55.714791),
                new GeoJsonCoordinates(9.531041, 55.7146),
                new GeoJsonCoordinates(9.531275, 55.714505),
                new GeoJsonCoordinates(9.531354, 55.714571),
                new GeoJsonCoordinates(9.531452, 55.714529)
            };

            GeoJsonMultiLineString multiLineString = new GeoJsonMultiLineString(lineString1, lineString2);

            Assert.AreEqual(2, multiLineString.Count);

            Assert.AreEqual(3, lineString1.Count);
            Assert.AreEqual(6, lineString2.Count);

            Assert.AreEqual(Json1, multiLineString.ToJson(Formatting.None));

        }

        private void Assert1(GeoJsonMultiLineString multiLineString) {

            Assert.AreEqual(2, multiLineString.Count);

            GeoJsonLineString lineString1 = multiLineString[0];

            Assert.AreEqual(3, lineString1.Count);

            Assert.AreEqual(9.531255, lineString1[0].X, "LineString 1 - #1 X");
            Assert.AreEqual(55.714869, lineString1[0].Y, "LineString 1 - #1 X");

            Assert.AreEqual(9.530941, lineString1[1].X, "LineString 1 - #2 X");
            Assert.AreEqual(55.714614, lineString1[1].Y, "LineString 1 - #2 X");

            Assert.AreEqual(9.53129, lineString1[2].X, "LineString 1 - #3 X");
            Assert.AreEqual(55.714482, lineString1[2].Y, "LineString 1 - #3 X");

            GeoJsonLineString lineString2 = multiLineString[1];

            Assert.AreEqual(9.531613, lineString2[0].X, "LineString 2 - #1 X");
            Assert.AreEqual(55.714659, lineString2[0].Y, "LineString 2 - #1 X");

            Assert.AreEqual(9.531278, lineString2[1].X, "LineString 2 - #2 X");
            Assert.AreEqual(55.714791, lineString2[1].Y, "LineString 2 - #2 X");

            Assert.AreEqual(9.531041, lineString2[2].X, "LineString 2 - #3 X");
            Assert.AreEqual(55.7146, lineString2[2].Y, "LineString 2 - #3 X");

            Assert.AreEqual(9.531275, lineString2[3].X, "LineString 2 - #4 X");
            Assert.AreEqual(55.714505, lineString2[3].Y, "LineString 2 - #4 X");

            Assert.AreEqual(9.531354, lineString2[4].X, "LineString 2 - #5 X");
            Assert.AreEqual(55.714571, lineString2[4].Y, "LineString 2 - #5 X");

            Assert.AreEqual(9.531452, lineString2[5].X, "LineString 2 - #6 X");
            Assert.AreEqual(55.714529, lineString2[5].Y, "LineString 2 - #6 X");

        }
        
        public const string Json1 = "{\"type\":\"MultiLineString\",\"coordinates\":[[[9.531255,55.714869],[9.530941,55.714614],[9.53129,55.714482]],[[9.531613,55.714659],[9.531278,55.714791],[9.531041,55.7146],[9.531275,55.714505],[9.531354,55.714571],[9.531452,55.714529]]]}";

    }

}