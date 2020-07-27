using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Features;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Geometry;

namespace UnitTestProject1.GeoJson.Features {

    [TestClass]
    public class FeatureTests {

        [TestMethod]
        public void Constructor1() {
            
            GeoJsonFeature feature = new GeoJsonFeature(new GeoJsonPoint(9.536067, 55.708116));

            Assert.IsNotNull(feature.Geometry);
            Assert.IsNotNull(feature.Properties);

            GeoJsonPoint point = feature.Geometry as GeoJsonPoint;
            
            Assert.IsNotNull(point);

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

        }

        [TestMethod]
        public void Constructor2() {
            
            GeoJsonFeature feature = new GeoJsonFeature(
                new GeoJsonPoint(9.536067, 55.708116),
                new GeoJsonProperties {
                    Name = "My feature"
                }
            );

            Assert.IsNotNull(feature.Geometry);
            Assert.IsNotNull(feature.Properties);

            GeoJsonPoint point = feature.Geometry as GeoJsonPoint;
            
            Assert.IsNotNull(point);

            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

            Assert.AreEqual("My feature", feature.Properties.Name);

        }
        
        [TestMethod]
        public void ToJson1() {
            
            // Initialize a new feature
            GeoJsonFeature feature = new GeoJsonFeature(
                new GeoJsonPoint(9.536067, 55.708116)
            );

            // Convert the feature to a JSON string
            string json = feature.ToJson(Formatting.None);

            Assert.AreEqual("{\"type\":\"Feature\",\"geometry\":{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}}", json);

        }
        
        [TestMethod]
        public void ToJson2() {
            
            GeoJsonFeature feature = new GeoJsonFeature(
                new GeoJsonPoint(9.536067, 55.708116),
                new GeoJsonProperties {
                    Name = "My feature"
                }
            );

            // Convert the point to a JSON string
            string json = feature.ToJson(Formatting.None);

            Assert.AreEqual("{\"type\":\"Feature\",\"properties\":{\"name\":\"My feature\"},\"geometry\":{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}}", json);

        }
        
        [TestMethod]
        public void Parse1() {

            const string json = "{\"type\":\"Feature\",\"geometry\":{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116]}}";

            GeoJsonFeature feature = GeoJsonFeature.Parse(json);

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Geometry);
            Assert.IsNotNull(feature.Properties);

            GeoJsonPoint point = feature.Geometry as GeoJsonPoint;

            Assert.IsNotNull(point);
            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(0, point.Altitude, "Altitude");

        }
        
        [TestMethod]
        public void Parse2() {
            
            const string json = "{\"type\":\"Feature\",\"properties\":{\"name\":\"My feature\"},\"geometry\":{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116,100]}}";

            GeoJsonFeature feature = GeoJsonFeature.Parse(json);

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Geometry);
            Assert.IsNotNull(feature.Properties);

            GeoJsonPoint point = feature.Geometry as GeoJsonPoint;

            Assert.IsNotNull(point);
            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(100, point.Altitude, "Altitude");

            Assert.AreEqual("My feature", feature.Properties.Name);
            Assert.AreEqual(null, feature.Properties.Description);

        }
        
        [TestMethod]
        public void Parse3() {
            
            const string json = "{\"type\":\"Feature\",\"properties\":{\"name\":\"My feature\"},\"geometry\":{\"type\":\"Point\",\"coordinates\":[9.536067,55.708116,100]}}";

            GeoJsonObject obj = GeoJsonUtils.Parse(json);

            Assert.IsNotNull(obj);

            GeoJsonFeature feature = obj as GeoJsonFeature;

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Geometry);
            Assert.IsNotNull(feature.Properties);

            GeoJsonPoint point = feature.Geometry as GeoJsonPoint;

            Assert.IsNotNull(point);
            Assert.AreEqual(9.536067, point.X, "X");
            Assert.AreEqual(55.708116, point.Y, "Y");
            Assert.AreEqual(100, point.Altitude, "Altitude");

            Assert.AreEqual("My feature", feature.Properties.Name);
            Assert.AreEqual(null, feature.Properties.Description);

        }

    }

}