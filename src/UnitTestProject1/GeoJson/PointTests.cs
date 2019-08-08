using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;

namespace UnitTestProject1.GeoJson {

    [TestClass]
    public class PointTests {

        [TestMethod]
        public void Constructor1() {

            GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

        }

        [TestMethod]
        public void Constructor2() {

            GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116, 100);

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(100, point.Altitude, "Altitude");

        }

        [TestMethod]
        public void Constructor3() {

            GeoJsonPoint point = new GeoJsonPoint(new[] { 9.536067, 55.708116 });

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

        }

        [TestMethod]
        public void Constructor4() {

            GeoJsonPoint point = new GeoJsonPoint(new[] { 9.536067, 55.708116, 100 });

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(100, point.Altitude, "Altitude");

        }

        [TestMethod]
        public void Constructor5() {
            
            // Initialize a new point
            IPoint p = new Point(55.708116, 9.536067);

            // Convert the IPoint to a GeoJsonPoint
            GeoJsonPoint point = new GeoJsonPoint(p);

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

        }
        
        [TestMethod]
        public void ToJson1() {
            
            // Initialize a new point
            GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);

            // Convert the point to a JSON string
            string json = point.ToJson(Formatting.None);

            Assert.AreEqual("{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}", json);

        }
        
        [TestMethod]
        public void ToJson2() {
            
            // Initialize a new point
            GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);

            // Convert the point to a JSON string
            string json = point.ToJson(Formatting.Indented);

            Assert.AreEqual("{\r\n  \"type\": \"Point\",\r\n  \"coordinates\": [\r\n    9.536067,\r\n    55.708116\r\n  ]\r\n}", json);

        }

        [TestMethod]
        public void ToPoint() {
            
            // Initialize a new point
            IPoint p1 = new Point(55.708116, 9.536067);

            // Convert the IPoint to a GeoJsonPoint
            GeoJsonPoint point = new GeoJsonPoint(p1);

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

            // Convert back to an IPoint
            IPoint p2 = point.ToPoint();

            Assert.AreEqual(55.708116, p2.Latitude, "Latitude");
            Assert.AreEqual(9.536067, p2.Longitude, "Longitude");

        }
        
        [TestMethod]
        public void Parse1() {

            GeoJsonPoint point = GeoJsonPoint.Parse("{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}");

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

        }
        
        [TestMethod]
        public void Parse2() {

            GeoJsonPoint point = GeoJsonPoint.Parse("{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116,100]}");

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(100, point.Altitude, "Altitude");

        }

    }

}