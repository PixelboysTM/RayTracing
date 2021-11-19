namespace RayTracing.Patterns
{
    public class RingPattern : Pattern
    {
        public Color A { get; }
        public Color B { get; }

        public RingPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }

        public override Color PatternAt(Tuple point)
            => System.Math.Floor((point.X * point.X + point.Z * point.Z).Sqrt()) % 2 == 0 ? A : B;
    }
}