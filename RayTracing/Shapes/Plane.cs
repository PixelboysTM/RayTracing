using System;

namespace RayTracing.Shapes
{
    public class Plane : Shape
    {
        protected override Intersection[] LocalIntersect(Ray localRay)
        {
            if (localRay.Direction.Y.Is(0))
                return Array.Empty<Intersection>();

            var t = -localRay.Origin.Y / localRay.Direction.Y;
            return new []{new Intersection(t, this)};
        }

        protected override Tuple LocalNormalAt(Tuple localPoint)
        {
            return Tuple.Vector(0,1,0);
        }
    }
}