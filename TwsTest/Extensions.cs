using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwsTest
{
    public static class Extensions
    {
        #region String Extensions
        public static bool IsInteger(this string str, bool allowSign = true, int? maxLength = null, int? maxValue = null, int? minValue = null)
        {
            if (!allowSign && str.Contains('-')) return false;
            str = str.Trim().Trim('-');
            if (str.Any(c => c < '0' || c > '9')) return false;
            if (maxLength.HasValue && str.Length > maxLength.Value) return false;
            if (!maxValue.HasValue && !minValue.HasValue) return true;
            // parse the integer
            var v = 0;
            if (!int.TryParse(str, out v)) return false;
            if (!maxValue.HasValue && v > maxValue.Value) return false;
            if (!minValue.HasValue && v < minValue.Value) return false;
            return true;
        }

        public static string[] SplitLines(this string str)
        {
            return str.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        public static string TrimEndNewLine(this string str)
        {
            if (!str.EndsWith(Environment.NewLine)) return str;
            if (str == Environment.NewLine) return string.Empty;
            return str.Remove(str.Length - Environment.NewLine.Length);
        }
        #endregion String Extensions

        #region Direction Extention

        public static string ToStr(this Directions d)
        {
            switch (d)
            {
                case Directions.North:
                    return "N";
                case Directions.East:
                    return "E";
                case Directions.South:
                    return "S";
                case Directions.West:
                    return "W";
            }
            return "";
        }
        #endregion  Direction Extention
    }
}
