using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;
using Skybrud.Essentials.Maps.Google;

namespace UnitTestProject1 {
    
    [TestClass]
    public class GooglePolylineAlgoritmTests {

        [TestMethod]
        public void Point() {

            Point point = new Point(55.70813, 9.53609);

            string result = GooglePolylineAlgoritm.Encode(point);

            Assert.AreEqual("yn_sIqoey@", result, "Encoding failed");

            Point decoded = GooglePolylineAlgoritm.Decode<Point>(result);

            Assert.AreEqual(point.Latitude, decoded.Latitude, "Decoding failed");
            Assert.AreEqual(point.Longitude, decoded.Longitude, "Decoding failed");

        }

        [TestMethod]
        public void LineString() {

            Point p1 = new Point(55.70813, 9.53609);
            Point p2 = new Point(55.70816, 9.53602);
            Point p3 = new Point(55.70807, 9.53600);

            LineString line = new LineString(p1, p2, p3);

            string result = GooglePolylineAlgoritm.Encode(line);

            Assert.AreEqual("yn_sIqoey@ELPB", result, "Encoding failed");

            LineString decoded = GooglePolylineAlgoritm.Decode<LineString>(result);

            Assert.AreEqual(p1.Latitude, decoded.Points[0].Latitude, "Decoding failed");
            Assert.AreEqual(p1.Longitude, decoded.Points[0].Longitude, "Decoding failed");

            Assert.AreEqual(p2.Latitude, decoded.Points[1].Latitude, "Decoding failed");
            Assert.AreEqual(p2.Longitude, decoded.Points[1].Longitude, "Decoding failed");

            Assert.AreEqual(p3.Latitude, decoded.Points[2].Latitude, "Decoding failed");
            Assert.AreEqual(p3.Longitude, decoded.Points[2].Longitude, "Decoding failed");

        }

    }

}