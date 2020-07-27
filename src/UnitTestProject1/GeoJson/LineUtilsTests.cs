using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybrud.Essentials.Maps;
using Skybrud.Essentials.Maps.Geometry;

namespace UnitTestProject1.GeoJson {

    [TestClass]
    public class LineUtilsTests  {

        [TestMethod]
        public void ComputeOffset() {

            const double r = 6378137;

            IPoint o = new Point(55.861858, 9.824622);

            IPoint r1 = MapsUtils.ComputeOffset(o, 100, 0, r);
            IPoint r2 = MapsUtils.ComputeOffset(o, 100, 45, r);
            IPoint r3 = MapsUtils.ComputeOffset(o, 100, 90, r);
            IPoint r4 = MapsUtils.ComputeOffset(o, 100, 135, r);
            IPoint r5 = MapsUtils.ComputeOffset(o, 100, 180, r);
            IPoint r6 = MapsUtils.ComputeOffset(o, 100, 225, r);
            IPoint r7 = MapsUtils.ComputeOffset(o, 100, 270, r);
            IPoint r8 = MapsUtils.ComputeOffset(o, 100, 315, r);
            IPoint r9 = MapsUtils.ComputeOffset(o, 100, 360, r);

            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r1, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r2, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r3, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r4, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r5, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r6, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r7, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r8, r).ToString("N4"));
            Assert.AreEqual("100.0000", DistanceUtils.GetDistance(o, r9, r).ToString("N4"));

        }

    }

}