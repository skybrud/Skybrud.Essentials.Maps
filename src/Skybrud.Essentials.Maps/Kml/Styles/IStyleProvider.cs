using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Skybrud.Essentials.Maps.Kml.Styles;

public interface IStyleProvider : IEnumerable<KmlStyleSelector> {

    KmlStyle GetStyleById(string id);

    bool TryGetStyleById(string id, [NotNullWhen(true)] out KmlStyle? style);

    KmlStyleMap GetStyleMapById(string id);

    bool TryGetStyleMapById(string id, [NotNullWhen(true)] out KmlStyleMap? style);

}