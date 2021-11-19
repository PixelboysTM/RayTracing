using System;

namespace RayTracing.Shapes
{
    public class TestShape : Shape
    {
        public Ray SavedRay { get; private set; }
        protected override Intersection[] LocalIntersect(Ray localRay)
        {
            SavedRay = localRay;
            return Array.Empty<Intersection>();
        }

        protected override Tuple LocalNormalAt(Tuple localPoint)
        {
            return Tuple.Vector(localPoint.X, localPoint.Y, localPoint.Z);
        }
    }
}