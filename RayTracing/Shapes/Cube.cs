using System;
using static RayTracing.Math;

namespace RayTracing.Shapes
{
    public class Cube : Shape
    {
        protected override Intersection[] LocalIntersect(Ray localRay)
        {
            var (xtmin, xtmax) = CheckAxis(localRay.Origin.X, localRay.Direction.X);
            var (ytmin, ytmax) = CheckAxis(localRay.Origin.Y, localRay.Direction.Y);
            var (ztmin, ztmax) = CheckAxis(localRay.Origin.Z, localRay.Direction.Z);

            var tmin = Max(xtmin, ytmin, ztmin);
            var tmax = Min(xtmax, ytmax, ztmax);

            if (tmin > tmax)
                return Array.Empty<Intersection>();

            return Intersection.Combine(new Intersection(tmin, this), new Intersection(tmax, this));
        }

        private (double, double) CheckAxis(double origin, double direction)
        {
            var tminNumerator = (-1 - origin);
            var tmaxNumerator = (1 - origin);

            double tmin, tmax;
            if (System.Math.Abs(direction) >= Constant.Epsilon)
            {
                tmin = tminNumerator / direction;
                tmax = tmaxNumerator / direction;
            }
            else{
                tmin = tminNumerator * double.PositiveInfinity;
                tmax = tmaxNumerator * double.PositiveInfinity;
            }

            if (tmin > tmax) (tmax, tmin) = (tmin, tmax);

            return (tmin, tmax);
        }

        protected override Tuple LocalNormalAt(Tuple localPoint)
        {
            var maxc = Max(System.Math.Abs(localPoint.X), System.Math.Abs(localPoint.Y),
                System.Math.Abs(localPoint.Z));

            return maxc.Is(System.Math.Abs(localPoint.X)) ? Tuple.Vector(localPoint.X, 0, 0) :
                maxc.Is(System.Math.Abs(localPoint.Y)) ? Tuple.Vector(0, localPoint.Y, 0) :
                Tuple.Vector(0, 0, localPoint.Z);
        }
    }
}