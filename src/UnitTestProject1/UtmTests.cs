using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybrud.Essentials.Maps;

namespace UnitTestProject1 {
    
    [TestClass]
    public class UtmTests {

        [TestMethod]
        public void ToLatLng() {

            MapsUtils.Utm.ToLatLng(716908.1, 6168607, "32N", out double latitude, out double longitude);

            //Assert.AreEqual("55.6149297077", latitude.ToString("N10"));
            //Assert.AreEqual("12.4444743511", longitude.ToString("N10"));

            Assert.AreEqual("55.614930", latitude.ToString("N6"));
            Assert.AreEqual("12.444474", longitude.ToString("N6"));

            //Assert.AreEqual("55.61492971", latitude.ToString("N8"));
            //Assert.AreEqual("12.44447435", longitude.ToString("N8"));

        }

    }

}