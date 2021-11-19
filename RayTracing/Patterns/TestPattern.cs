namespace RayTracing.Patterns
{
    public class TestPattern : Pattern
    {
        public override Color PatternAt(Tuple point)
            => new(point.X, point.Y, point.Z);
    }
}