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