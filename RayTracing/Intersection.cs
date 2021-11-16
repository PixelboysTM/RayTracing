using System.Linq;
using RayTracing.Shapes;

namespace RayTracing
{
    public class Intersection
    {
        public double t { get; init; }
        public Sphere Object { get; init; }
        
        public Intersection(double t, Sphere obj)
        {
            this.t = t;
            Object = obj;
        }

        public static Intersection[] Combine(params Intersection[] intersections)
        {
            var list = intersections.ToList();
            list.Sort((intersection, intersection1) =>
            {
                if (intersection.t > intersection1.t)
                    return 1;
                if (intersection.t < intersection1.t)
                    return -1;
                return 0;
            });
            return list.ToArray();
        }
    }
}