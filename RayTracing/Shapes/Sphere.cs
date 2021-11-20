using System;

namespace RayTracing.Shapes
{
    public class Sphere : Shape
    {

        protected override Intersection[] LocalIntersect(Ray localRay)
        {
            var sphereToRay = localRay.Origin - Tuple.Point(0, 0, 0);
            
            var a = localRay.Direction.Dot(localRay.Direction);
            var b = 2 * localRay.Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                return Array.Empty<Intersection>();

            var t1 = (-b - discriminant.Sqrt()) / (2.0 * a);
            var t2 = (-b + discriminant.Sqrt()) / (2.0 * a);

            return new[] { new Intersection(t1, this), new Intersection(t2, this) };
        }

        protected override Tuple LocalNormalAt(Tuple localPoint)
        {
            return (localPoint - Tuple.Point(0, 0, 0));
        }

        public static bool operator ==(Sphere left, Sphere right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;
            return left.Material == right.Material && left.Transform == right.Transform;
        }

        public static bool operator !=(Sphere left, Sphere right)
        {
            return !(left == right);
        }

#region STATIC

        public static Sphere GlassSphere => new()
        {
            Material = new Material
            {
                Transparency = 1.0,
                RefractiveIndex = 1.5
            }
        };

#endregion
    }
}