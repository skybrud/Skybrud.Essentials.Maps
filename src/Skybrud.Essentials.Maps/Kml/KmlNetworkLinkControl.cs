using System;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Geometry;
using Skybrud.Essentials.Strings.Extensions;

namespace Skybrud.Essentials.Maps.Kml {

    /// <summary>
    /// Controls the behavior of files fetched by a <see cref="KmlNetworkLink"/>>.
    /// </summary>
    public class KmlNetworkLinkControl : KmlObject {

        #region Properties

        public TimeSpan MinRefreshPeriod { get; set; }

        public TimeSpan MaxSessionLength { get; set; }

        public string Cookie { get; set; }

        public string Message { get; set; }

        public string LinkName { get; set; }

        public string Expires { get; set; }

        #endregion

        #region Constructors

        public KmlNetworkLinkControl() {
            MaxSessionLength = TimeSpan.FromSeconds(-1);
        }

        protected KmlNetworkLinkControl(XElement xml, XmlNamespaceManager namespaces) {
            throw new NotImplementedException();
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            
            XElement xml = base.NewXElement("NetworkLinkControl");

            if (MinRefreshPeriod.TotalSeconds > 0) xml.Add(NewXElement("minRefreshPeriod", MinRefreshPeriod.TotalSeconds));
            if (MaxSessionLength.TotalSeconds > -1) xml.Add(NewXElement("maxSessionLength", MaxSessionLength.TotalSeconds));

            if (Cookie.HasValue()) xml.Add(NewXElement("cookie", Cookie));
            if (Message.HasValue()) xml.Add(NewXElement("Message", Message));
            if (LinkName.HasValue()) xml.Add(NewXElement("LinkName", LinkName));
            if (Expires.HasValue()) xml.Add(NewXElement("expires", Expires));

            return xml;

        }

        #endregion

        #region Static methods

        public static KmlNetworkLinkControl Parse(XElement xml) {
            return xml == null ? null : new KmlNetworkLinkControl(xml, Namespaces);
        }

        public static KmlNetworkLinkControl Parse(XElement xml, XmlNamespaceManager namespaces) {
            return xml == null ? null : new KmlNetworkLinkControl(xml, namespaces);
        }

        #endregion

    }

}