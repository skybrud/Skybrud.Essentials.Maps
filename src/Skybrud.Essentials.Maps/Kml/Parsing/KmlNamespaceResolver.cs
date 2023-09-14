using System.Xml;
using Skybrud.Essentials.Maps.Kml.Constants;

namespace Skybrud.Essentials.Maps.Kml.Parsing;

public class KmlNamespaceResolver {

    public static readonly IXmlNamespaceResolver Instance;

    static KmlNamespaceResolver() {

        XmlNameTable table = new NameTable();
        XmlNamespaceManager manager = new(table);
        manager.AddNamespace("kml", KmlConstants.DefaultNamespace);
        Instance = manager;

    }

}