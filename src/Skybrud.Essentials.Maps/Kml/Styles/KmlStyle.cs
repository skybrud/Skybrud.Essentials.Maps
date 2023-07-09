using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Xml.Extensions;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public class KmlStyle : KmlStyleSelector {

        #region Properties

        public KmlIconStyle? IconStyle { get; set; }

        public KmlLabelStyle? LabelStyle { get; set; }

        public KmlLineStyle? LineStyle { get; set; }

        public KmlPolyStyle? PolyStyle { get; set; }

        public KmlBalloonStyle? BalloonStyle { get; set; }

        #endregion

        #region Constructors

        public KmlStyle() { }

        protected KmlStyle(XElement xml, IXmlNamespaceResolver namespaces) : base(xml, namespaces) {
            IconStyle = xml.GetElement("kml:IconStyle", namespaces, x => KmlIconStyle.Parse(x, namespaces));
            LabelStyle = xml.GetElement("kml:LabelStyle", namespaces, x => KmlLabelStyle.Parse(x, namespaces));
            LineStyle = xml.GetElement("kml:LineStyle", namespaces, x => KmlLineStyle.Parse(x, namespaces));
            PolyStyle = xml.GetElement("kml:PolyStyle", namespaces, x => KmlPolyStyle.Parse(x, namespaces));
            BalloonStyle = xml.GetElement("kml:BalloonStyle", namespaces, x => KmlBalloonStyle.Parse(x, namespaces));
        }

        #endregion

        #region Member methods

        public override XElement ToXElement() {

            XElement xml = base.ToXElement();

            if (IconStyle is not null) xml.Add(IconStyle.ToXElement());
            if (LabelStyle is not null) xml.Add(LabelStyle.ToXElement());
            if (LineStyle is not null) xml.Add(LineStyle.ToXElement());
            if (PolyStyle is not null) xml.Add(PolyStyle.ToXElement());
            if (BalloonStyle is not null) xml.Add(BalloonStyle.ToXElement());

            return xml;

        }

        #endregion

        #region Static methods

        public static new KmlStyle Parse(XElement xml) {
            return new KmlStyle(xml, Namespaces);
        }

        public static new KmlStyle Parse(XElement xml, IXmlNamespaceResolver? namespaces) {
            return new KmlStyle(xml, namespaces ?? Namespaces);
        }

        #endregion

    }

}