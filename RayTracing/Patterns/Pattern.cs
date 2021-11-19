using RayTracing.Shapes;

namespace RayTracing.Patterns
{
    public abstract class Pattern
    {
        public Matrix4x4 Transform { get; set; } = Matrix4x4.Identity;
        
        public abstract Color PatternAt(Tuple point);

        public Color PatternAtObject(Shape obj, Tuple point)
        {
            var objectPoint = obj.Transform.Inverse * point;
            var patternSpace = Transform.Inverse * objectPoint;
            return PatternAt(patternSpace);
        }
    }
}