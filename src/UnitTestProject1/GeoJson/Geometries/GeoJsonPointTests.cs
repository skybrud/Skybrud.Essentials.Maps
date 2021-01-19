using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;

namespace UnitTestProject1.GeoJson.Geometries {

    [TestClass]
    public class GeoJsonPointTests {

        [TestMethod]
        public void Constructor1() {

            GeoJsonPoint point = new GeoJsonPoint();

            Assert.AreEqual(GeoJsonType.Point, point.Type);

            Assert.AreEqual(0, point.X);
            Assert.AreEqual(0, point.Y);
            Assert.AreEqual(0, point.Altitude);

        }

        [TestMethod]
        public void Constructor2() {

            GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116);

            Assert.AreEqual(GeoJsonType.Point, point.Type);

            Assert.AreEqual(9.536067, point.X);
            Assert.AreEqual(55.708116, point.Y);
            Assert.AreEqual(0, point.Altitude);

        }

        [TestMethod]
        public void Constructor3() {

            GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116, 1.125);

            Assert.AreEqual(GeoJsonType.Point, point.Type);

            Assert.AreEqual(9.536067, point.X);
            Assert.AreEqual(55.708116, point.Y);
            Assert.AreEqual(1.125, point.Altitude);

        }

        [TestMethod]
        public void Constructor4()  {

            double[] array = {9.536067, 55.708116, 1.125};

            GeoJsonPoint point = new GeoJsonPoint(array);

            Assert.AreEqual(GeoJsonType.Point, point.Type);

            Assert.AreEqual(9.536067, point.X);
            Assert.AreEqual(55.708116, point.Y);
            Assert.AreEqual(1.125, point.Altitude);

        }

        [TestMethod]
        public void Constructor5()  {

            IPoint p = new Point(55.708116, 9.536067);

            GeoJsonPoint point = new GeoJsonPoint(p);

            Assert.AreEqual(GeoJsonType.Point, point.Type);

            Assert.AreEqual(9.536067, point.X);
            Assert.AreEqual(55.708116, point.Y);
            Assert.AreEqual(0, point.Altitude);

        }
        
        [TestMethod]
        public void ToJson1() {

            GeoJsonPoint point = new GeoJsonPoint(9.536067, 55.708116, 1.125);

            string json = point.ToJson(Formatting.None);

            Assert.AreEqual(Json1, json);

        }
        
        [TestMethod]
        public void Parse1() {

            GeoJsonPoint point = GeoJsonPoint.Parse(Json1);

            Assert.AreEqual(GeoJsonType.Point, point.Type);

            Assert.AreEqual(9.536067, point.X);
            Assert.AreEqual(55.708116, point.Y);
            Assert.AreEqual(1.125, point.Altitude);

        }

        [TestMethod]
        public void Deserialize1() {

            GeoJsonPoint point = JsonConvert.DeserializeObject<GeoJsonPoint>(Json1);

            Assert.AreEqual(GeoJsonType.Point, point.Type);

            Assert.AreEqual(9.536067, point.X);
            Assert.AreEqual(55.708116, point.Y);
            Assert.AreEqual(1.125, point.Altitude);

        }

        private const string Json1 = "{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116,1.125]}";

    }

}