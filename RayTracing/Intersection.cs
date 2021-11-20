using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
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

        public Computations PrepareComputations(Ray ray, Intersection[] xs = null)
        {
            xs ??= new[] { this };
            
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
            comps.UnderPoint = comps.Point - comps.NormalV * Constant.Epsilon;
            comps.ReflectV = ray.Direction.Reflect(comps.NormalV);
            
            //Refraction
            var containers = new List<Shape>();

            foreach (var i in xs)
            {
                if (i == this)
                {
                    if (containers.Count == 0)
                        comps.N1 = 1.0;
                    else
                        comps.N1 = containers[^1].Material.RefractiveIndex;
                }

                if (containers.Contains(i.Object))
                    containers.Remove(i.Object);
                else
                    containers.Add(i.Object);

                if (i == this)
                {
                    if (containers.Count == 0)
                    {
                        comps.N2 = 1.0;
                    }
                    else
                    {
                        comps.N2 = containers[^1].Material.RefractiveIndex;
                    }
                    
                    break;
                }
            }

            return comps;
        }

        public static bool operator ==(Intersection left, Intersection right)
        {
            if (right is null || left is null)
                return false;
            
            return left.t.Is(right.t) && left.Object == right.Object;
        }

        public static bool operator !=(Intersection left, Intersection right)
        {
            return !(left == right);
        }
    }
}