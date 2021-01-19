using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace UnitTestProject1.GeoJson {

    [TestClass]
    public class PointTests {

        [TestMethod]
        public void Constructor1() {

            GeoJsonPolygon polygon = new GeoJsonPolygon();

            Assert.AreEqual(GeoJsonType.Polygon, polygon.Type);

            Assert.AreEqual(0, polygon.Outer.Count);
            Assert.AreEqual(0, polygon.Inner.Count);

        }

        [TestMethod]
        public void Constructor2() {

            var outer = new [] {
                new [] { 9.531275, 55.714505, 1 },
                new [] { 9.531503, 55.714701, 2 },
                new [] { 9.531278, 55.714791, 3 },
                new [] { 9.531038, 55.714599, 4 },
                new [] { 9.531275, 55.714505, 1 }
            };

            GeoJsonPolygon polygon = new GeoJsonPolygon(outer);

            Assert1(polygon);

        }

        [TestMethod]
        public void Constructor3() {

            double[][] outer = {
                new [] { 9.531275, 55.714505, 1 },
                new [] { 9.531503, 55.714701, 2 },
                new [] { 9.531278, 55.714791, 3 },
                new [] { 9.531038, 55.714599, 4 },
                new [] { 9.531275, 55.714505, 1 }
            };

            double[][][] coordinates = { outer };

            GeoJsonPolygon polygon = new GeoJsonPolygon(coordinates);

            Assert1(polygon);

        }

        [TestMethod]
        public void Constructor4() {

            IPoint[] outer = {
                new Point(55.714505, 9.531275),
                new Point(55.714701, 9.531503),
                new Point(55.714791, 9.531278),
                new Point(55.714599, 9.531038),
                new Point(55.714505, 9.531275)
            };

            GeoJsonPolygon polygon = new GeoJsonPolygon(outer);

            Assert2(polygon);

        }

        [TestMethod]
        public void Constructor5() {

            IPoint[] outer = {
                new Point(55.714505, 9.531275),
                new Point(55.714701, 9.531503),
                new Point(55.714791, 9.531278),
                new Point(55.714599, 9.531038),
                new Point(55.714505, 9.531275)
            };

            IPoint[][] coordinates = { outer };

            GeoJsonPolygon polygon = new GeoJsonPolygon(coordinates);

            Assert2(polygon);

        }

        [TestMethod]
        public void Constructor6() {

            GeoJsonCoordinates[] outer = {
                new GeoJsonCoordinates(9.531275, 55.714505, 1),
                new GeoJsonCoordinates(9.531503, 55.714701, 2),
                new GeoJsonCoordinates(9.531278, 55.714791, 3),
                new GeoJsonCoordinates(9.531038, 55.714599, 4),
                new GeoJsonCoordinates(9.531275, 55.714505, 1)
            };

            GeoJsonPolygon polygon = new GeoJsonPolygon(outer);

            Assert1(polygon);

        }

        [TestMethod]
        public void Constructor7() {

            List<GeoJsonCoordinates> outer = new List<GeoJsonCoordinates> {
                new GeoJsonCoordinates(9.531275, 55.714505, 1),
                new GeoJsonCoordinates(9.531503, 55.714701, 2),
                new GeoJsonCoordinates(9.531278, 55.714791, 3),
                new GeoJsonCoordinates(9.531038, 55.714599, 4),
                new GeoJsonCoordinates(9.531275, 55.714505, 1)
            };

            GeoJsonPolygon polygon = new GeoJsonPolygon(outer);

            Assert1(polygon);

        }

        [TestMethod]
        public void Constructor8() {

            IPoint[] outer = {
                new Point(55.714505, 9.531275),
                new Point(55.714701, 9.531503),
                new Point(55.714791, 9.531278),
                new Point(55.714599, 9.531038),
                new Point(55.714505, 9.531275)
            };

            IPolygon p = new Polygon(outer);

            GeoJsonPolygon polygon = new GeoJsonPolygon(p);

            Assert2(polygon);

        }
        
        [TestMethod]
        public void ToJson1() {

            GeoJsonCoordinates[] outer = {
                new GeoJsonCoordinates(9.531275, 55.714505, 1),
                new GeoJsonCoordinates(9.531503, 55.714701, 2),
                new GeoJsonCoordinates(9.531278, 55.714791, 3),
                new GeoJsonCoordinates(9.531038, 55.714599, 4),
                new GeoJsonCoordinates(9.531275, 55.714505, 1)
            };

            // Initialize a new polygon
            GeoJsonPolygon polygon = new GeoJsonPolygon(outer);

            // Convert the polygon to a JSON string
            string json = polygon.ToJson(Formatting.None);

            Assert.AreEqual(Json1, json);

        }
        
        [TestMethod]
        public void Parse1() {

            GeoJsonPolygon polygon = GeoJsonPolygon.Parse(Json1);

            Assert1(polygon);

        }
        
        [TestMethod]
        public void Deserialize1() {

            GeoJsonPolygon polygon = JsonConvert.DeserializeObject<GeoJsonPolygon>(Json1);

            Assert1(polygon);

        }

        private void Assert1(GeoJsonPolygon polygon) {

            Assert.AreEqual(GeoJsonType.Polygon, polygon.Type);

            Assert.AreEqual(5, polygon.Outer.Count);

            Assert.AreEqual(0, polygon.Inner.Count);

            Assert.AreEqual(9.531275, polygon.Outer[0].X, "#1 X");
            Assert.AreEqual(55.714505, polygon.Outer[0].Y, "#1 Y");
            Assert.AreEqual(1, polygon.Outer[0].Altitude, "#1 Altitude");

            Assert.AreEqual(9.531503, polygon.Outer[1].X, "#2 X");
            Assert.AreEqual(55.714701, polygon.Outer[1].Y, "#2 Y");
            Assert.AreEqual(2, polygon.Outer[1].Altitude, "#2 Altitude");

            Assert.AreEqual(9.531278, polygon.Outer[2].X, "#3 X");
            Assert.AreEqual(55.714791, polygon.Outer[2].Y, "#3 Y");
            Assert.AreEqual(3, polygon.Outer[2].Altitude, "#3 Altitude");

            Assert.AreEqual(9.531038, polygon.Outer[3].X, "#4 X");
            Assert.AreEqual(55.714599, polygon.Outer[3].Y, "#4 Y");
            Assert.AreEqual(4, polygon.Outer[3].Altitude, "#4 Altitude");

            Assert.AreEqual(9.531275, polygon.Outer[4].X, "#5 X");
            Assert.AreEqual(55.714505, polygon.Outer[4].Y, "#5 Y");
            Assert.AreEqual(1, polygon.Outer[4].Altitude, "#5 Altitude");

        }

        private void Assert2(GeoJsonPolygon polygon) {

            Assert.AreEqual(GeoJsonType.Polygon, polygon.Type);

            Assert.AreEqual(5, polygon.Outer.Count);

            Assert.AreEqual(0, polygon.Inner.Count);

            Assert.AreEqual(9.531275, polygon.Outer[0].X, "#1 X");
            Assert.AreEqual(55.714505, polygon.Outer[0].Y, "#1 Y");
            Assert.AreEqual(0, polygon.Outer[0].Altitude, "#1 Altitude");

            Assert.AreEqual(9.531503, polygon.Outer[1].X, "#2 X");
            Assert.AreEqual(55.714701, polygon.Outer[1].Y, "#2 Y");
            Assert.AreEqual(0, polygon.Outer[1].Altitude, "#2 Altitude");

            Assert.AreEqual(9.531278, polygon.Outer[2].X, "#3 X");
            Assert.AreEqual(55.714791, polygon.Outer[2].Y, "#3 Y");
            Assert.AreEqual(0, polygon.Outer[2].Altitude, "#3 Altitude");

            Assert.AreEqual(9.531038, polygon.Outer[3].X, "#4 X");
            Assert.AreEqual(55.714599, polygon.Outer[3].Y, "#4 Y");
            Assert.AreEqual(0, polygon.Outer[3].Altitude, "#4 Altitude");

            Assert.AreEqual(9.531275, polygon.Outer[4].X, "#5 X");
            Assert.AreEqual(55.714505, polygon.Outer[4].Y, "#5 Y");
            Assert.AreEqual(0, polygon.Outer[4].Altitude, "#5 Altitude");

        }

        const string Json1 = "{\"type\":\"Polygon\",\"coordinates\":[[[9.531275,55.714505,1.0],[9.531503,55.714701,2.0],[9.531278,55.714791,3.0],[9.531038,55.714599,4.0],[9.531275,55.714505,1.0]]]}";

    }

}