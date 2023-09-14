using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Skybrud.Essentials.Maps.Kml.Styles;

public class KmlStyleSelectorCollection : IStyleProvider, IEnumerable<KmlStyle> , IEnumerable<KmlStyleMap> {

    private readonly List<KmlStyleSelector> _selectors;

    private readonly Dictionary<string, KmlStyleSelector> _lookup;

    #region Properties

    public int Count => _selectors.Count;

    #endregion

    #region Constructors

    public KmlStyleSelectorCollection() {
        _selectors = new List<KmlStyleSelector>();
        _lookup = new Dictionary<string, KmlStyleSelector>();
    }

    public KmlStyleSelectorCollection(params KmlStyleSelector[]? selectors) {
        _selectors = selectors?.ToList() ?? new List<KmlStyleSelector>();
        _lookup = _selectors.Where(x => string.IsNullOrWhiteSpace(x.Id) == false).ToDictionary(x => x.Id!);
    }

    public KmlStyleSelectorCollection(IEnumerable<KmlStyleSelector>? selectors) {
        _selectors = selectors?.ToList() ?? new List<KmlStyleSelector>();
        _lookup = _selectors.Where(x => string.IsNullOrWhiteSpace(x.Id) == false).ToDictionary(x => x.Id!);
    }

    #endregion

    #region Member methods

    public void Add(KmlStyle style) {
        if (style == null) throw new ArgumentNullException(nameof(style));
        _selectors.Add(style);
        if (string.IsNullOrWhiteSpace(style.Id)) return;
        _lookup[style.Id!] = style;
    }

    public void Add(KmlStyleMap styleMap) {
        if (styleMap == null) throw new ArgumentNullException(nameof(styleMap));
        _selectors.Add(styleMap);
        if (string.IsNullOrWhiteSpace(styleMap.Id)) return;
        _lookup[styleMap.Id!] = styleMap;
    }

    public bool Contains(string id) {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        return _lookup.ContainsKey(id);
    }

    public KmlStyle GetStyleById(string id) {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        return (KmlStyle) _lookup[id];
    }

    public KmlStyleMap GetStyleMapById(string id) {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        return (KmlStyleMap) _lookup[id];
    }

    public bool TryGetStyleById(string id, [NotNullWhen(true)] out KmlStyle? style) {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        _lookup.TryGetValue(id, out KmlStyleSelector? selector);
        style = selector as KmlStyle;
        return style != null;
    }

    public bool TryGetStyleMapById(string id, [NotNullWhen(true)] out KmlStyleMap? styleMap) {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        _lookup.TryGetValue(id, out KmlStyleSelector? selector);
        styleMap = selector as KmlStyleMap;
        return styleMap != null;
    }

    public IEnumerator<KmlStyleSelector> GetEnumerator() {
        return _selectors.GetEnumerator();
    }

    IEnumerator<KmlStyle> IEnumerable<KmlStyle>.GetEnumerator() {
        return _selectors.OfType<KmlStyle>().GetEnumerator();
    }

    IEnumerator<KmlStyleMap> IEnumerable<KmlStyleMap>.GetEnumerator() {
        return _selectors.OfType<KmlStyleMap>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    #endregion

    #region Operator overloading

    public static implicit operator KmlStyleSelectorCollection(KmlStyleSelector[] styles) {
        return new KmlStyleSelectorCollection(styles);
    }

    #endregion

}