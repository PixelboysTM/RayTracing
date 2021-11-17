using System;
using System.Collections.Generic;
using RayTracing.Light;
using RayTracing.Shapes;

namespace RayTracing
{
    public class World
    {
        public List<Sphere> Objects { get; set; } = new();
        public PointLight? Light { get; set; }

        public bool Contains(Sphere obj)
        {
            foreach (var o in Objects)
            {
                if (o == obj)
                    return true;
            }

            return false;
        }

        public static World Default => new()
        {
            Objects = new List<Sphere>
            {
                new Sphere
                {
                    Material = new Material
                    {
                        Color = new Color(0.8, 1.0, 0.6),
                        Diffuse = 0.7,
                        Specular = 0.2
                    }
                },
                new Sphere
                {
                    Transform = Transformation.Scaling(0.5, 0.5, 0.5)
                }

            },
            Light = new(Tuple.Point(-10, 10, -10), new Color(1, 1, 1))

        };

        public Intersection[] IntersectWorld(Ray ray)
        {
            List<Intersection> xs = new List<Intersection>();
            Objects.ForEach(obj =>
            {
                var x = obj.Intersect(ray);
                xs.AddRange(x);
            });
            return Intersection.Combine(xs.ToArray());
        }

        public Color ShadeHit(Computations comps)
        {
            if (Light != null)
                return comps.Object.Material.Lighting(Light.Value, comps.Point, comps.EyeV, comps.NormalV);

            throw new MemberAccessException("No Light is Specified");
        }

        public Color ColorAt(Ray ray)
        {
            var xs = IntersectWorld(ray);
            var hit = xs.Hit();
            if (hit is null)
                return Color.Black;

            var comps = hit.PrepareComputations(ray);
            var c = ShadeHit(comps);
            return c;
        }
    }
}