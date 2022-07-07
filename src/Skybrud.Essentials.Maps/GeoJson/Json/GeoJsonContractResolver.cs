using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Skybrud.Essentials.Maps.GeoJson.Json {
    
    /// <summary>
    /// JSON contract resolver used for serializing objects within the <c>Skybrud.Essentials.Maps.GeoJson</c> namespace.
    /// </summary>
    public class GeoJsonContractResolver : CamelCasePropertyNamesContractResolver {

        #region Properties

        /// <summary>
        /// Gets a reference to the singleton instance of this class.
        /// </summary>
        public static readonly GeoJsonContractResolver Instance = new();

        #endregion

        #region Member methods

        /// <inheritdoc />
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization) {
            
            JsonProperty property = base.CreateProperty(member, serialization);

            property.ShouldSerialize = x => ShouldSerialize(member, x);

            return property;

        }

        /// <inheritdoc />
        protected override JsonContract CreateContract(Type objectType) {

            JsonContract contract = base.CreateContract(objectType);

            if (objectType == typeof(GeoJsonCoordinates)) {
                contract.Converter = new GeoJsonWriteConverter();
            }

            if (objectType == typeof(GeoJsonProperties)) {
                contract.Converter = new GeoJsonWriteConverter();
            }

            return contract;

        }

        private bool ShouldSerialize(MemberInfo member, object obj) {

            if (member is not PropertyInfo property) return true;

            // Make sure to only serialize "properties" if the object has any properties
            if (property.Name == "Properties" && property.PropertyType == typeof(GeoJsonProperties)) {
                return property.GetValue(obj) is GeoJsonProperties { Count: > 0 };
            }

            //if (property.Name == "Altitude" && property.GetValue(obj) is double altitude) {
            //    return Math.Abs(altitude) > double.Epsilon;
            //}

            return true;

        }

        #endregion

    }

}