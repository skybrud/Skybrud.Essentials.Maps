using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;

namespace UnitTestProject1.GeoJson.Geometries {

    [TestClass]
    public class GeoJsonLineStringTests {

        [TestMethod]
        public void Constructor1() {

            GeoJsonLineString lineString = new GeoJsonLineString();

            Assert.AreEqual(0, lineString.Count);

        }

        [TestMethod]
        public void Constructor2() {

            double[][] array = {
                new [] { 9.536067, 55.708116, 1.125 },
                new [] { 9.536000, 55.708068, 2.250 },
                new [] { 9.536169, 55.708062, 4.500 }
            };

            GeoJsonLineString lineString = new GeoJsonLineString(array);

            Assert.AreEqual(3, lineString.Count);

            GeoJsonCoordinates p1 = lineString[0];
            GeoJsonCoordinates p2 = lineString[1];
            GeoJsonCoordinates p3 = lineString[2];

            Assert.AreEqual(9.536067, p1.X, "#1 X");
            Assert.AreEqual(55.708116, p1.Y, "#1 Y");
            Assert.AreEqual(1.125, p1.Altitude, "#1 Altitude");

            Assert.AreEqual(9.536000, p2.X, "#2 X");
            Assert.AreEqual(55.708068, p2.Y, "#2 Y");
            Assert.AreEqual(2.250, p2.Altitude, "#2 Altitude");

            Assert.AreEqual(9.536169, p3.X, "#3 X");
            Assert.AreEqual(55.708062, p3.Y, "#3 Y");
            Assert.AreEqual(4.500, p3.Altitude, "#3 Altitude");

        }

        [TestMethod]
        public void Constructor3() {

            List<IPoint> sample = new List<IPoint> {
                new Point(55.708116, 9.536067),
                new Point(55.708068, 9.536000),
                new Point(55.708062, 9.536169)
            };

            GeoJsonLineString lineString = new GeoJsonLineString(sample);

            Assert.AreEqual(3, lineString.Count);

            GeoJsonCoordinates p1 = lineString[0];
            GeoJsonCoordinates p2 = lineString[1];
            GeoJsonCoordinates p3 = lineString[2];

            Assert.AreEqual(9.536067, p1.X, "#1 X");
            Assert.AreEqual(55.708116, p1.Y, "#1 Y");
            Assert.AreEqual(0, p1.Altitude, "#1 Altitude");

            Assert.AreEqual(9.536000, p2.X, "#2 X");
            Assert.AreEqual(55.708068, p2.Y, "#2 Y");
            Assert.AreEqual(0, p2.Altitude, "#2 Altitude");

            Assert.AreEqual(9.536169, p3.X, "#3 X");
            Assert.AreEqual(55.708062, p3.Y, "#3 Y");
            Assert.AreEqual(0, p3.Altitude, "#3 Altitude");

        }

        [TestMethod]
        public void Constructor4() {

            List<GeoJsonCoordinates> sample = new List<GeoJsonCoordinates> {
                new GeoJsonCoordinates(9.536067, 55.708116),
                new GeoJsonCoordinates(9.536000, 55.708068),
                new GeoJsonCoordinates(9.536169, 55.708062)
            };

            GeoJsonLineString lineString = new GeoJsonLineString(sample);

            Assert.AreEqual(3, lineString.Count);

            GeoJsonCoordinates p1 = lineString[0];
            GeoJsonCoordinates p2 = lineString[1];
            GeoJsonCoordinates p3 = lineString[2];

            Assert.AreEqual(9.536067, p1.X, "#1 X");
            Assert.AreEqual(55.708116, p1.Y, "#1 Y");
            Assert.AreEqual(0, p1.Altitude, "#1 Altitude");

            Assert.AreEqual(9.536000, p2.X, "#2 X");
            Assert.AreEqual(55.708068, p2.Y, "#2 Y");
            Assert.AreEqual(0, p2.Altitude, "#2 Altitude");

            Assert.AreEqual(9.536169, p3.X, "#3 X");
            Assert.AreEqual(55.708062, p3.Y, "#3 Y");
            Assert.AreEqual(0, p3.Altitude, "#3 Altitude");

        }

        [TestMethod]
        public void Constructor5() {

            ILineString sample = new LineString(
                new Point(55.708116, 9.536067),
                new Point(55.708068, 9.536000),
                new Point(55.708062, 9.536169)
            );

            GeoJsonLineString lineString = new GeoJsonLineString(sample);

            Assert.AreEqual(3, lineString.Count);

            GeoJsonCoordinates p1 = lineString[0];
            GeoJsonCoordinates p2 = lineString[1];
            GeoJsonCoordinates p3 = lineString[2];

            Assert.AreEqual(9.536067, p1.X, "#1 X");
            Assert.AreEqual(55.708116, p1.Y, "#1 Y");
            Assert.AreEqual(0, p1.Altitude, "#1 Altitude");

            Assert.AreEqual(9.536000, p2.X, "#2 X");
            Assert.AreEqual(55.708068, p2.Y, "#2 Y");
            Assert.AreEqual(0, p2.Altitude, "#2 Altitude");

            Assert.AreEqual(9.536169, p3.X, "#3 X");
            Assert.AreEqual(55.708062, p3.Y, "#3 Y");
            Assert.AreEqual(0, p3.Altitude, "#3 Altitude");

        }
        
        [TestMethod]
        public void ToJson1() {

            double[][] array = {
                new [] { 9.536067, 55.708116, 1.125 },
                new [] { 9.536000, 55.708068, 2.250 },
                new [] { 9.536169, 55.708062, 4.500 }
            };

            GeoJsonLineString lineString = new GeoJsonLineString(array);

            string json = lineString.ToJson(Formatting.None);

            Assert.AreEqual("{\"type\":\"LineString\",\"coordinates\":[[9.536067,55.708116,1.125],[9.536,55.708068,2.25],[9.536169,55.708062,4.5]]}", json);

        }
        
        [TestMethod]
        public void ToJson2() {

            double[][] array = {
                new [] { 9.536067, 55.708116, 1.125 },
                new [] { 9.536000, 55.708068, 2.250 },
                new [] { 9.536169, 55.708062, 4.500 }
            };

            GeoJsonLineString lineString = new GeoJsonLineString(array);

            string json = lineString.ToJson(Formatting.Indented);

            Assert.AreEqual("{\r\n  \"type\": \"LineString\",\r\n  \"coordinates\": [\r\n    [\r\n      9.536067,\r\n      55.708116,\r\n      1.125\r\n    ],\r\n    [\r\n      9.536,\r\n      55.708068,\r\n      2.25\r\n    ],\r\n    [\r\n      9.536169,\r\n      55.708062,\r\n      4.5\r\n    ]\r\n  ]\r\n}", json);

        }

        [TestMethod]
        public void ToLineString() {

            ILineString sample = new LineString(
                new Point(55.708116, 9.536067),
                new Point(55.708068, 9.536000),
                new Point(55.708062, 9.536169)
            );

            // Convert the IPoint to a GeoJsonPoint
            GeoJsonLineString lineString = new GeoJsonLineString(sample);

            GeoJsonCoordinates p1 = lineString[0];
            GeoJsonCoordinates p2 = lineString[1];
            GeoJsonCoordinates p3 = lineString[2];

            Assert.AreEqual(9.536067, p1.X, "#1 X");
            Assert.AreEqual(55.708116, p1.Y, "#1 Y");
            Assert.AreEqual(0, p1.Altitude, "#1 Altitude");

            Assert.AreEqual(9.536000, p2.X, "#2 X");
            Assert.AreEqual(55.708068, p2.Y, "#2 Y");
            Assert.AreEqual(0, p2.Altitude, "#2 Altitude");

            Assert.AreEqual(9.536169, p3.X, "#3 X");
            Assert.AreEqual(55.708062, p3.Y, "#3 Y");
            Assert.AreEqual(0, p3.Altitude, "#3 Altitude");

            ILineString result = lineString.ToLineString();

            Assert.AreEqual(55.708116, result.Points[0].Latitude, "#1 Latitude");
            Assert.AreEqual(9.536067, result.Points[0].Longitude, "#1 Longitude");

            Assert.AreEqual(55.708068, result.Points[1].Latitude, "#2 Latitude");
            Assert.AreEqual(9.536000, result.Points[1].Longitude, "#2 Longitude");

            Assert.AreEqual(55.708062, result.Points[2].Latitude, "#3 Latitude");
            Assert.AreEqual(9.536169, result.Points[2].Longitude, "#3 Longitude");

        }
        
        [TestMethod]
        public void Parse1() {

            GeoJsonLineString lineString = GeoJsonLineString.Parse("{\"type\":\"LineString\",\"coordinates\":[[9.536067,55.708116,1.125],[9.536,55.708068,2.25],[9.536169,55.708062,4.5]]}");

            Assert.AreEqual(3, lineString.Count);

            GeoJsonCoordinates p1 = lineString[0];
            GeoJsonCoordinates p2 = lineString[1];
            GeoJsonCoordinates p3 = lineString[2];

            Assert.AreEqual(9.536067, p1.X, "#1 X");
            Assert.AreEqual(55.708116, p1.Y, "#1 Y");
            Assert.AreEqual(1.125, p1.Altitude, "#1 Altitude");

            Assert.AreEqual(9.536000, p2.X, "#2 X");
            Assert.AreEqual(55.708068, p2.Y, "#2 Y");
            Assert.AreEqual(2.250, p2.Altitude, "#2 Altitude");

            Assert.AreEqual(9.536169, p3.X, "#3 X");
            Assert.AreEqual(55.708062, p3.Y, "#3 Y");
            Assert.AreEqual(4.500, p3.Altitude, "#3 Altitude");

        }

        [TestMethod]
        public void Deserialize1() {

            GeoJsonLineString lineString = JsonConvert.DeserializeObject<GeoJsonLineString>("{\"type\":\"LineString\",\"coordinates\":[[9.536067,55.708116,1.125],[9.536,55.708068,2.25],[9.536169,55.708062,4.5]]}");

            Assert.AreEqual(3, lineString.Count);

            GeoJsonCoordinates p1 = lineString[0];
            GeoJsonCoordinates p2 = lineString[1];
            GeoJsonCoordinates p3 = lineString[2];

            Assert.AreEqual(9.536067, p1.X, "#1 X");
            Assert.AreEqual(55.708116, p1.Y, "#1 Y");
            Assert.AreEqual(1.125, p1.Altitude, "#1 Altitude");

            Assert.AreEqual(9.536000, p2.X, "#2 X");
            Assert.AreEqual(55.708068, p2.Y, "#2 Y");
            Assert.AreEqual(2.250, p2.Altitude, "#2 Altitude");

            Assert.AreEqual(9.536169, p3.X, "#3 X");
            Assert.AreEqual(55.708062, p3.Y, "#3 Y");
            Assert.AreEqual(4.500, p3.Altitude, "#3 Altitude");

        }

    }

}