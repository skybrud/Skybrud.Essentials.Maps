using System.Collections.Generic;

namespace Skybrud.Essentials.Maps.Kml.Styles {

    public interface IStyleProvider : IEnumerable<KmlStyleSelector> {

        KmlStyle GetStyleById(string id);

        bool TryGetStyleById(string id, out KmlStyle style);

        KmlStyleMap GetStyleMapById(string id);

        bool TryGetStyleMapById(string id, out KmlStyleMap style);

    }

}