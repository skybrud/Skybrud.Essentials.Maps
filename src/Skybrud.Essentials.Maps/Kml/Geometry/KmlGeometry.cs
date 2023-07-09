using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Geometry {

    /// <summary>
    /// Class representing a KML <c>&lt;Geometry&gt;</c> element.
    /// </summary>
    public abstract class KmlGeometry : KmlObject {

        #region Properties

        /// <summary>
        /// Get or sets the ID of the geometry.
        /// </summary>
        public string? Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty KML <c>&lt;Geometry&gt;</c> element.
        /// </summary>
        protected KmlGeometry() { }

        /// <summary>
        /// Initializes a new KML <c>&lt;Geometry&gt;</c> element.
        /// </summary>
        /// <param name="xml">The XML element the document should be based on.</param>
        protected KmlGeometry(XElement xml) {
            Id = xml.GetAttributeValue("id");
        }

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (!string.IsNullOrWhiteSpace(Id)) xml.Add(new XAttribute("id", Id!));

            return xml;

        }

        #endregion

    }

}