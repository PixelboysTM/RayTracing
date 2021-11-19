using RayTracing.Shapes;

namespace RayTracing.Patterns
{
    public class StripePattern : Pattern
    {
        public Color A { get; }
        public Color B { get; }

        public StripePattern(Color a, Color b)
        {
            A = a;
            B = b;
        }

        public override Color PatternAt(Tuple point)
            => System.Math.Floor(point.X) % 2 == 0 ? A : B;

        
    }
}