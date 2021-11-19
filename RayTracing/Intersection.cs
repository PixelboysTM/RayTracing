using System.Linq;
using RayTracing.Shapes;

namespace RayTracing
{
    public class Intersection
    {
        public double t { get; init; }
        public Shape Object { get; init; }
        
        public Intersection(double t, Shape obj)
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

        public Computations PrepareComputations(Ray ray)
        {
            var comps = new Computations();

            comps.t = t;
            comps.Object = Object;

            comps.Point = ray.Position(comps.t);
            comps.EyeV = -ray.Direction;
            comps.NormalV = comps.Object.NormalAt(comps.Point);

            if (comps.NormalV.Dot(comps.EyeV) < 0.0)
            {
                comps.Inside = true;
                comps.NormalV = -comps.NormalV;
            }
            else
                comps.Inside = false;

            comps.OverPoint = comps.Point + comps.NormalV * Constant.Epsilon;
            comps.ReflectV = ray.Direction.Reflect(comps.NormalV);

            return comps;
        }
    }
}