namespace Skybrud.Essentials.Maps.Kml;

public enum KmlLinkRefreshMode {

    /// <summary>
    /// Refresh when the file is loaded and whenever the Link parameters change (the default)
    /// </summary>
    OnChange,

    /// <summary>
    /// Refresh every <em>n</em> seconds (specified in <see cref="KmlLink.RefreshInterval"/>).
    /// </summary>
    OnInterval,

    /// <summary>
    /// Refresh the file when the expiration time is reached. If a fetched file has a
    /// <see cref="KmlNetworkLinkControl"/>, the <see cref="KmlNetworkLinkControl.Expires"/> time takes precedence
    /// over expiration times specified in HTTP headers. If no <see cref="KmlNetworkLinkControl.Expires"/> time is
    /// specified, the HTTP <em>max-age</em> header is used (if present). If <em>max-age</em> is not present, the
    /// <em>Expires</em> HTTP header is used (if present).
    /// </summary>
    OnExpire

}