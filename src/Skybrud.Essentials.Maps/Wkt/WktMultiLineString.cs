using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Skybrud.Essentials.Maps.Wkt {

    public class WktMultiLineString : WktShape {

        #region Properties

        public WktLineString[] Lines { get; }

        #endregion

        #region Constructors

        public WktMultiLineString() {
            Lines = new WktLineString[0];
        }

        public WktMultiLineString(WktLineString[] lines) {
            Lines = lines ?? new WktLineString[0];
        }

        public WktMultiLineString(List<WktLineString> lines) {
            Lines = lines?.ToArray() ?? new WktLineString[0];
        }

        public WktMultiLineString(IEnumerable<WktLineString> lines) {
            Lines = lines?.ToArray() ?? new WktLineString[0];
        }

        #endregion

        #region Member methods

        public override string ToString() {
            return ToString(WktFormatting.Default);
        }

        public string ToString(WktFormatting formatting) {
            return "MULTILINESTRING " + (Lines.Length == 0 ? "EMPTY" : "(" + String.Join(", ", from line in Lines select ToString(line, formatting, 0)) + ")");
        }

        #endregion

        #region Static methods

        public new static WktMultiLineString Parse(string str) {

            if (String.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            str = str.Trim();

            if (!str.StartsWith("MULTILINESTRING")) throw new Exception("Input string is in an invalid format");

            if (str.Equals("MULTILINESTRING EMPTY")) return new WktMultiLineString();

            str = str.Substring(15).Trim().Replace("\n", " ");

            MatchCollection matches = Regex.Matches(str, "\\(([0-9\\., ]+)\\)");
            if (matches.Count == 0) throw new Exception("Input string is in an invalid format");

            return new WktMultiLineString(
                from Match match in matches
                select WktLineString.Parse(match.Groups[1].Value)
            );

        }

        #endregion

    }

}