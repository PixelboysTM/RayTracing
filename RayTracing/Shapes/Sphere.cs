using System;

namespace RayTracing.Shapes
{
    public class Sphere
    {
        public Matrix4x4 Transform { get; set; } = Matrix4x4.Identity;
        public Material Material { get; set; } = new();
        
        public Intersection[] Intersect(Ray ray)
        {
            var ray2 = ray.Transform(Transform.Inverse);
            
            var sphereToRay = ray2.Origin - Tuple.Point(0, 0, 0);
            
            var a = ray2.Direction.Dot(ray2.Direction);
            var b = 2 * ray2.Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                return Array.Empty<Intersection>();

            var t1 = (-b - discriminant.Sqrt()) / (2.0 * a);
            var t2 = (-b + discriminant.Sqrt()) / (2.0 * a);

            return new[] { new Intersection(t1, this), new Intersection(t2, this) };
        }

        public Tuple NormalAt(Tuple point)
        {
            var objectPoint = Transform.Inverse * point;
            var objectNormal = (objectPoint - Tuple.Point(0, 0, 0));
            var worldNormal = Transform.Inverse.Transpose * objectNormal;
            worldNormal.W = 0;
            return worldNormal.Normalised;
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
    }
}