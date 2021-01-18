using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;

namespace UnitTestProject1.GeoJson.Geometries {
    
    [TestClass]
    public class GeoJsonMultiPointTests {

        [TestMethod]
        public void Constructor1() {

            GeoJsonMultiPoint multiPoint = new GeoJsonMultiPoint();

            Assert.AreEqual(0, multiPoint.Count);

        }

        [TestMethod]
        public void Constructor2() {

            double[][] array = {
                new [] { 9.536067, 55.708116, 1.125 },
                new [] { 9.536000, 55.708068, 2.250 },
                new [] { 9.536169, 55.708062, 4.500 }
            };

            GeoJsonMultiPoint multiPoint = new GeoJsonMultiPoint(array);

            Assert.AreEqual(3, multiPoint.Count);

            GeoJsonCoordinates p1 = multiPoint[0];
            GeoJsonCoordinates p2 = multiPoint[1];
            GeoJsonCoordinates p3 = multiPoint[2];

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

            GeoJsonMultiPoint multiPoint = new GeoJsonMultiPoint(sample);

            Assert.AreEqual(3, multiPoint.Count);

            GeoJsonCoordinates p1 = multiPoint[0];
            GeoJsonCoordinates p2 = multiPoint[1];
            GeoJsonCoordinates p3 = multiPoint[2];

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

            GeoJsonMultiPoint multiPoint = new GeoJsonMultiPoint(sample);

            Assert.AreEqual(3, multiPoint.Count);

            GeoJsonCoordinates p1 = multiPoint[0];
            GeoJsonCoordinates p2 = multiPoint[1];
            GeoJsonCoordinates p3 = multiPoint[2];

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
        public void Parse1() {

            GeoJsonMultiPoint mp = GeoJsonMultiPoint.Parse("{\"type\":\"MultiPoint\",\"coordinates\":[[10.0,40.0],[40.0,30.0],[20.0,20.0],[30.0,10.0]]}");

            Assert.AreEqual(4, mp.Count);

            GeoJsonCoordinates p1 = mp[0];
            GeoJsonCoordinates p2 = mp[1];
            GeoJsonCoordinates p3 = mp[2];
            GeoJsonCoordinates p4 = mp[3];

            Assert.AreEqual(10, p1.X, "#1 X");
            Assert.AreEqual(40, p1.Y, "#1 Y");

            Assert.AreEqual(40, p2.X, "#2 X");
            Assert.AreEqual(30, p2.Y, "#2 Y");

            Assert.AreEqual(20, p3.X, "#3 X");
            Assert.AreEqual(20, p3.Y, "#3 Y");

            Assert.AreEqual(30, p4.X, "#4 X");
            Assert.AreEqual(10, p4.Y, "#4 Y");

        }

        [TestMethod]
        public void Deserialize1() {

            GeoJsonMultiPoint mp = JsonConvert.DeserializeObject<GeoJsonMultiPoint>("{\"type\":\"MultiPoint\",\"coordinates\":[[10.0,40.0],[40.0,30.0],[20.0,20.0],[30.0,10.0]]}");

            Assert.AreEqual(4, mp.Count);

            GeoJsonCoordinates p1 = mp[0];
            GeoJsonCoordinates p2 = mp[1];
            GeoJsonCoordinates p3 = mp[2];
            GeoJsonCoordinates p4 = mp[3];

            Assert.AreEqual(10, p1.X, "#1 X");
            Assert.AreEqual(40, p1.Y, "#1 Y");

            Assert.AreEqual(40, p2.X, "#2 X");
            Assert.AreEqual(30, p2.Y, "#2 Y");

            Assert.AreEqual(20, p3.X, "#3 X");
            Assert.AreEqual(20, p3.Y, "#3 Y");

            Assert.AreEqual(30, p4.X, "#4 X");
            Assert.AreEqual(10, p4.Y, "#4 Y");

        }

        [TestMethod]
        public void ToJson1() {

            GeoJsonMultiPoint mp = new GeoJsonMultiPoint {
                new GeoJsonCoordinates(10, 40),
                new GeoJsonCoordinates(40, 30),
                new GeoJsonCoordinates(20, 20),
                new GeoJsonCoordinates(30, 10)
            };

            Assert.AreEqual(4, mp.Count);

            Assert.AreEqual("{\"type\":\"MultiPoint\",\"coordinates\":[[10.0,40.0],[40.0,30.0],[20.0,20.0],[30.0,10.0]]}", mp.ToJson(Formatting.None));

        }

    }

}