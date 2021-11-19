namespace RayTracing.Patterns
{
    public class GradientPattern : Pattern
    {
        public Color A { get; }
        public Color B { get; }

        public GradientPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }

        public override Color PatternAt(Tuple point)
            => A + (B - A) * (point.X - System.Math.Floor(point.X));
    }
}