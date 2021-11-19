namespace RayTracing.Patterns
{
    public class CheckerPattern : Pattern
    {
        public Color A { get; }
        public Color B { get; }

        public CheckerPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }

        public override Color PatternAt(Tuple point)
            => (System.Math.Floor(point.X) + System.Math.Floor(point.Y) + System.Math.Floor(point.Z)) % 2.0 == 0
                ? A.Copy
                : B.Copy;
    }
}