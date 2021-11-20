using System;
using System.Collections.Generic;
using RayTracing.Light;
using RayTracing.Shapes;

namespace RayTracing
{
    public class World
    {
        public List<Shape> Objects { get; set; } = new();
        public PointLight? Light { get; set; }

        public bool Contains(Shape obj)
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
            Objects = new List<Shape>
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

        public Color ShadeHit(Computations comps, int remaining = Constant.MaxRecursionDepth)
        {
            var shadowed = IsShadowed(comps.OverPoint);
            
            if (Light == null)
                throw new MemberAccessException("No Light is Specified");

            var surface = comps.Object.Material.Lighting(comps.Object, Light.Value, comps.OverPoint, comps.EyeV,
                comps.NormalV, shadowed);

            var reflected = ReflectedColor(comps, remaining);
            var refracted = RefractedColor(comps, remaining);

            var material = comps.Object.Material;
            if (material.Reflective > 0 && material.Transparency > 0)
            {
                var reflectance = comps.Schlick();
                return surface + reflected * reflectance + refracted * (1 - reflectance);
            }

            return surface + reflected + refracted;


        }

        public Color ColorAt(Ray ray, int remaining = Constant.MaxRecursionDepth)
        {
            var xs = IntersectWorld(ray);
            var hit = xs.Hit();
            if (hit is null)
                return Color.Black;

            var comps = hit.PrepareComputations(ray, xs);
            var c = ShadeHit(comps, remaining);
            return c;
        }

        public bool IsShadowed(Tuple point)
        {
            if (Light == null)
                throw new NullReferenceException("No Light source set!");
            
            var v = Light.Value.Position - point;
            var distance = v.Magnitude;

            var direction = v.Normalised;
            
            var r = new Ray(point, direction);
            var intersections = IntersectWorld(r);

            var h = intersections.Hit();
            return h is not null && h.t < distance;
        }

        public Color ReflectedColor(Computations comps, int remaining)
        {
            if (comps.Object.Material.Reflective == 0 || remaining < 1)
                return Color.Black;

            var reflectedRay = new Ray(comps.OverPoint, comps.ReflectV);
            var color = ColorAt(reflectedRay, remaining-1);
            return color * comps.Object.Material.Reflective;
        }

        public Color RefractedColor(Computations comps, int remaining)
        {
            if (comps.Object.Material.Transparency.Is(0) || remaining == 0)
                return Color.Black;

            var nRatio = comps.N1 / comps.N2;

            var cosI = comps.EyeV.Dot(comps.NormalV);

            var sin2T = nRatio * nRatio * (1 - cosI * cosI);
            if (sin2T > 1.0)
                return Color.Black;

            var cosT = (1.0 - sin2T).Sqrt();

            var direction = comps.NormalV * (nRatio * cosI - cosT) - comps.EyeV * nRatio;

            var refractedRay = new Ray(comps.UnderPoint, direction);

            var color = ColorAt(refractedRay, remaining - 1) * comps.Object.Material.Transparency;
            
            return color;
        }
    }
}