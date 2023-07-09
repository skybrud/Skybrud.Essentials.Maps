using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.GeoJson;
using Skybrud.Essentials.Maps.GeoJson.Features;
using Skybrud.Essentials.Maps.GeoJson.Geometry;
using Skybrud.Essentials.Maps.Kml;
using Skybrud.Essentials.Maps.Kml.Exceptions;
using Skybrud.Essentials.Maps.Kml.Features;
using Skybrud.Essentials.Maps.Kml.Geometry;
using Skybrud.Essentials.Maps.Kml.Styles;
using Skybrud.Essentials.Strings.Extensions;

namespace Skybrud.Essentials.Maps.Converters {

    public class KmlToGeoJsonConverter {

        public virtual GeoJsonFeatureCollection[] Convert(KmlFile kml) {
            if (kml.Feature == null) return ArrayUtils.Empty<GeoJsonFeatureCollection>();
            return kml.Feature switch {
                KmlDocument document => ConvertDocument(document),
                KmlPlacemark placemark => new[] { new GeoJsonFeatureCollection(ConvertPlacemark(placemark)), },
                _ => throw new KmlException("Unsupported feature " + kml.Feature.GetType())
            };
        }

        protected virtual GeoJsonFeatureCollection[] ConvertDocument(KmlDocument document) {
            return ConvertDocument(document, default);
        }

        protected virtual GeoJsonFeatureCollection[] ConvertDocument(KmlDocument document, IStyleProvider provider) {

            // Use the style provider of the document if not explicitly specified
            provider ??= document.StyleSelectors;

            // Initialize a new list of collections
            List<GeoJsonFeatureCollection> collections = new();

            if (document.Features.Any(x => x is KmlFolder)) {

                GeoJsonFeatureCollection defaultCollection = null;

                foreach (KmlFeature feature in document.Features) {

                    switch (feature) {

                        case KmlDocument:
                            throw new KmlException("Weird place for a <Document>, huh?");

                        case KmlFolder folder:
                            collections.Add(ConvertFolder(folder, provider));
                            break;

                        case KmlPlacemark placemark:

                            // Make sure the default feature collection is initialized
                            if (defaultCollection == null) collections.Add(defaultCollection = new GeoJsonFeatureCollection());

                            // As the placemark isn't located inside a folder, we should append it to a "default" feature collection"
                            defaultCollection.Add(ConvertPlacemark(placemark, provider));

                            break;

                        default:
                            throw new KmlException($"Unsupported feature {feature.GetType()}");

                    }

                }

            } else {

                GeoJsonFeatureCollection features = new();

                foreach (KmlFeature feature in document.Features) {

                    switch (feature) {

                        case KmlDocument:
                            throw new KmlException("Weird place for a <Document>, huh?");

                        case KmlPlacemark placemark:
                            features.Add(ConvertPlacemark(placemark, provider));
                            break;

                        default:
                            throw new KmlException($"Unsupported feature {feature.GetType()}");

                    }

                }

                collections.Add(features);

            }

            return collections.ToArray();

        }

        public virtual GeoJsonObject ConvertFeature(KmlFeature feature) {
            return ConvertFeature(feature, null);
        }

        public virtual GeoJsonObject ConvertFeature(KmlFeature feature, IStyleProvider provider) {
            return feature switch {
                KmlDocument => throw new NotImplementedException(),
                //return ConvertDocument(document, provider ?? document.StyleSelectors);
                KmlFolder folder => ConvertFolder(folder, provider),
                KmlPlacemark placemark => ConvertPlacemark(placemark, provider),
                _ => throw new KmlException("Unsupported feature " + feature.GetType())
            };
        }

        public virtual GeoJsonFeatureCollection ConvertFolder(KmlFolder folder, IStyleProvider provider) {

            GeoJsonFeatureCollection collection = new();

            foreach (KmlFeature feature in folder.Features) {
                ConvertFolder(folder, collection, feature, provider);
            }

            return collection;

        }

        protected virtual void ConvertFolder(KmlFolder folder, GeoJsonFeatureCollection collection, KmlFeature feature, IStyleProvider provider) {

            switch (feature) {

                case KmlPlacemark placemark:

                    collection.Add(ConvertPlacemark(placemark, provider));

                    break;

                case KmlDocument document:
                    // Why would you have a document in a folder? (although it seems legal according to the specification)

                case KmlFolder childFolder:
                    // Why would you have folder in another folder? (although it seems legal according to the specification)

                default:
                    throw new KmlException($"Unsupported feature {feature.GetType()}");

            }

        }

        public GeoJsonFeature ConvertPlacemark(KmlPlacemark placemark) {
            return ConvertPlacemark(placemark, default);
        }

        public virtual GeoJsonFeature ConvertPlacemark(KmlPlacemark placemark, IStyleProvider provider) {

            // Initialize a new feature
            GeoJsonFeature feature = new();

            // Convert the name and description
            if (placemark.Name.HasValue()) feature.Properties.Name = placemark.Name;
            if (placemark.Description.HasValue()) feature.Properties.Description = placemark.Description;

            feature.Geometry = placemark.Geometry switch {
                KmlPoint point => Convert(point),
                KmlLineString lineString => Convert(lineString),
                KmlPolygon polygon => Convert(polygon),
                _ => throw new KmlException($"Geometry type {placemark.Geometry.GetType()} not supported")
            };

            if (string.IsNullOrWhiteSpace(placemark.StyleUrl) == false && provider != null) {
                if (provider.TryGetStyleMapById(placemark.StyleUrl.Substring(1), out KmlStyleMap styleMap)) {
                    if (styleMap.Normal != null && provider.TryGetStyleById(styleMap.Normal.StyleUrl.Substring(1), out KmlStyle style)) {
                        ConvertProperties(style, feature.Properties);
                    }
                } else {
                    if (provider.TryGetStyleById(placemark.StyleUrl.Substring(1), out KmlStyle style)) {
                        ConvertProperties(style, feature.Properties);
                    }
                }
            }

            return feature;

        }

        protected virtual void ConvertProperties(KmlStyle style, GeoJsonProperties properties) {
            if (style == null) return;
            ConvertProperties(style.IconStyle, properties);
            ConvertProperties(style.LineStyle, properties);
            ConvertProperties(style.PolyStyle, properties);
        }

        protected virtual void ConvertProperties(KmlIconStyle style, GeoJsonProperties properties) {

            if (style == null) return;

            if (string.IsNullOrWhiteSpace(style.Icon?.Href) == false) {
                properties.MarkerSymbol = style.Icon.Href;
            }

            if (style.Scale > 0) properties.MarkerSize = style.Scale.ToString("N2");

            if (style.Color.HasValue()) properties.MarkerColor = style.Color;

        }

        protected virtual void ConvertProperties(KmlLineStyle style, GeoJsonProperties properties) {

            if (style == null) return;

            if (style.Color.HasValue() && KmlUtils.TryParseHexColor(style.Color, out byte r, out byte g, out byte b, out float a)) {
                properties.Stroke = "#" + KmlUtils.RgbToHex(r, g, b);
                properties.StrokeOpacity = (float) Math.Round(a, 2);
            }

            if (style.Width > 0) {
                properties.StrokeWidth = style.Width;
            }

        }

        protected virtual void ConvertProperties(KmlPolyStyle style, GeoJsonProperties properties) {

            if (style == null) return;

            if (style.Color.HasValue() && KmlUtils.TryParseHexColor(style.Color, out byte r, out byte g, out byte b, out float a)) {
                properties.Fill = "#" + KmlUtils.RgbToHex(r, g, b);
                properties.FillOpacity = (float) Math.Round(a, 2);
            }

        }

        public virtual GeoJsonPoint Convert(KmlPoint point) {

            double latitude = point.Coordinates.Latitude;
            double longitude = point.Coordinates.Longitude;
            double altitude = point.Coordinates.Altitude;

            return new GeoJsonPoint(longitude, latitude, altitude);

        }

        public virtual GeoJsonLineString Convert(KmlLineString lineString) {

            double[][] coordinates = lineString.Select(x => new [] {x.Longitude, x.Latitude}).ToArray();

            return new GeoJsonLineString(coordinates);

        }

        public virtual GeoJsonPolygon Convert(KmlPolygon polygon) {

            double[][][] array = new double[polygon.InnerBoundaries.Count + 1][][];

            array[0] = new double[polygon.OuterBoundaries.LinearRing.Coordinates.Count][];

            for (int i = 0; i < polygon.OuterBoundaries.LinearRing.Coordinates.Count; i++) {
                KmlPointCoordinates c = polygon.OuterBoundaries.LinearRing.Coordinates[i];
                array[0][i] = c.HasAltitude ? new [] { c.Longitude, c.Latitude, c.Altitude } : new [] { c.Longitude, c.Latitude };
            }

            if (polygon.InnerBoundaries != null) {
                for (int j = 0; j < polygon.InnerBoundaries.Count; j++) {
                    array[j + 1] = new double[polygon.InnerBoundaries[j].LinearRing.Coordinates.Count][];
                    for (int k = 0; k < polygon.InnerBoundaries[j].LinearRing.Coordinates.Count; k++) {
                        KmlPointCoordinates c = polygon.InnerBoundaries[j].LinearRing.Coordinates[k];
                        array[j + 1][k] = c.HasAltitude ? new[] { c.Longitude, c.Latitude, c.Altitude } : new[] { c.Longitude, c.Latitude };
                    }
                }
            }

            return new GeoJsonPolygon(array);

        }

    }

}