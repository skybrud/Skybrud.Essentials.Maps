using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybrud.Essentials.Maps.Wkt;

namespace UnitTestProject1.WellKnownText.Geometries {
    
    [TestClass]
    public class MultiPointTests {

        [TestMethod]
        public void Parse1() {

            WktMultiPoint mp = WktMultiPoint.Parse("MULTIPOINT ((10 40), (40 30), (20 20), (30 10))");

            Assert.AreEqual(4, mp.Count);

            WktPoint p1 = mp[0];
            WktPoint p2 = mp[1];
            WktPoint p3 = mp[2];
            WktPoint p4 = mp[3];

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
        public void Parse2() {

            WktMultiPoint mp = WktMultiPoint.Parse("MULTIPOINT(10 40,40 30,20 20,30 10)");

            Assert.AreEqual(4, mp.Count);

            WktPoint p1 = mp[0];
            WktPoint p2 = mp[1];
            WktPoint p3 = mp[2];
            WktPoint p4 = mp[3];

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
        public void Parse3() {

            WktMultiPoint mp = WktMultiPoint.Parse("MULTIPOINT EMPTY");

            Assert.AreEqual(0, mp.Count);

        }

    }

}