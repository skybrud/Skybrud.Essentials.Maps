using System;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml {
    
    /// <see>
    ///     <cref>https://developers.google.com/kml/documentation/kmlreference#link</cref>
    /// </see>
    public class KmlLink : KmlObject {

        #region Properties

        /// <summary>
        /// Gets or sets a URL (either an HTTP address or a local file specification).
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the refresh mode of the link.
        /// </summary>
        public KmlLinkRefreshMode RefreshMode { get; set; }

        /// <summary>
        /// Indicates to refresh the file every <em>n</em> seconds.
        /// </summary>
        public TimeSpan RefreshInterval { get; set; }

        #endregion

        #region Constructors

        public KmlLink() { }

        protected KmlLink(XElement xml, XmlNamespaceManager namespaces) {
            Href = xml.GetElementValue("kml:href", namespaces);
            RefreshMode = xml.GetElementValueAsEnum("kml:refreshMode", namespaces, KmlLinkRefreshMode.OnChange);
            RefreshInterval = TimeSpan.FromSeconds(xml.GetElementValueAsInt32("kml:refreshInterval", namespaces));
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {
            XElement xml = base.NewXElement("Link");
            if (Href.HasValue()) xml.Add(NewXElement("href", Href));
            if (RefreshMode != KmlLinkRefreshMode.OnChange) xml.Add(NewXElement("refreshMode", RefreshMode.ToCamelCase()));
            if (RefreshMode == KmlLinkRefreshMode.OnInterval) xml.Add(NewXElement("refreshInterval", RefreshInterval.TotalSeconds));
            return xml;
        }

        #endregion

        #region Static methods

        public static KmlLink Parse(XElement xml) {
            return new KmlLink(xml, Namespaces);
        }

        public static KmlLink Parse(XElement xml, XmlNamespaceManager namespaces) {
            return new KmlLink(xml, namespaces);
        }

        #endregion

    }

}