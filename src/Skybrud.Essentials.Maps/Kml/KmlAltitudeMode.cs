namespace Skybrud.Essentials.Maps.Kml {

    /// <summary>
    /// Specifies how altitude components in the &lt;coordinates&gt; element are interpreted. 
    /// </summary>
    public enum KmlAltitudeMode {

        Unspecified,

        /// <summary>
        /// Indicates to ignore an altitude specification (for example, in the &lt;coordinates&gt; tag).
        /// </summary>
        ClampToGround,

        /// <summary>
        /// Sets the altitude of the element relative to the actual ground elevation of a particular location. For
        /// example, if the ground elevation of a location is exactly at sea level and the altitude for a point is set
        /// to 9 meters, then the elevation for the icon of a point placemark elevation is 9 meters with this mode.
        /// 
        /// However, if the same coordinate is set over a location where the ground elevation is 10 meters above sea
        /// level, then the elevation of the coordinate is 19 meters. A typical use of this mode is for placing
        /// telephone poles or a ski lift.
        /// </summary>
        RelativeToGround,

        /// <summary>
        /// Sets the altitude of the coordinate relative to sea level, regardless of the actual elevation of the
        /// terrain beneath the element. For example, if you set the altitude of a coordinate to 10 meters with an
        /// absolute altitude mode, the icon of a point placemark will appear to be at ground level if the terrain
        /// beneath is also 10 meters above sea level. If the terrain is 3 meters above sea level, the placemark will
        /// appear elevated above the terrain by 7 meters. A typical use of this mode is for aircraft placement.
        /// </summary>
        Absolute

    }

}