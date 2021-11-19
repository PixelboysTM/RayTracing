using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RayTracing
{
    public struct Color
    {
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public Color Copy => new (Red, Green, Blue);

        public Color(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public Color(string hex)
        {
            var match = Regex.Match(hex,
                "#?(?<red>[abcdefABCDEF0-9]{2})(?<green>[abcdefABCDEF0-9]{2})(?<blue>[abcdefABCDEF0-9]{2})");
            if (!match.Success)
                throw new EvaluateException("String is not a hex color code!");

            var red = match.Groups["red"].Value;
            var green = match.Groups["green"].Value;
            var blue = match.Groups["blue"].Value;

            Red = Int32.Parse(red, NumberStyles.HexNumber) / 255.0;
            Green = Int32.Parse(green, NumberStyles.HexNumber) / 255.0;
            Blue = Int32.Parse(blue, NumberStyles.HexNumber) / 255.0;
        }
        
        private bool Equals(Color other)
        {
            return Red.Is(other.Red) && Green.Is(other.Green) && Blue.Is(other.Blue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((Color)obj);
        }

        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !left.Equals(right);
        }

        public static Color operator +(Color tuple, Color right)
        {
            return new Color(tuple.Red + right.Red, tuple.Green + right.Green, tuple.Blue + right.Blue);
        }
        
        public static Color operator -(Color tuple, Color right)
        {
            return new Color(tuple.Red - right.Red, tuple.Green - right.Green, tuple.Blue - right.Blue);
        }

        public static Color operator *(Color color, double scalar)
        {
            return new Color(color.Red * scalar, color.Green * scalar, color.Blue * scalar);
        }
        
        public static Color operator *(Color color, Color scalar)
        {
            return new Color(color.Red * scalar.Red, color.Green * scalar.Green, color.Blue * scalar.Blue);
        }

        public static Color Black => new(0, 0, 0);
    }
}