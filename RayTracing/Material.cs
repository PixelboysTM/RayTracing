using RayTracing.Light;
using RayTracing.Patterns;
using RayTracing.Shapes;

namespace RayTracing
{
    public class Material
    {
        public Pattern Pattern { get; set; } = null;
        public Color Color { get; set; } = new(1, 1, 1);
        public double Ambient { get; set; } = 0.1;
        public double Diffuse { get; set; } = 0.9;
        public double Specular { get; set; } = 0.9;
        public double Shininess { get; set; } = 200.0;
        public double Reflective { get; set; } = 0.0;

        public static bool operator ==(Material left, Material right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;
            return left.Color == right.Color && left.Ambient.Is(right.Ambient) && left.Diffuse.Is(right.Diffuse) && left.Specular.Is(right.Specular) &&
                   left.Shininess.Is(right.Shininess) && left.Pattern == right.Pattern;
        }

        public static bool operator !=(Material left, Material right)
        {
            return !(left == right);
        }

        public Color Lighting(Shape obj, PointLight light, Tuple point, Tuple eyev, Tuple normalv, bool inShadow = false)
        {
            var color = Pattern?.PatternAtObject(obj, point) ?? Color;
            
            var effectiveColor = color * light.Intensity;
            var lightv = (light.Position - point).Normalised;
            var ambient = effectiveColor * Ambient;

            var lightDotNormal = lightv.Dot(normalv);

            Color diffuse;
            Color specular;
            if (lightDotNormal < 0.0)
            {
                diffuse = RayTracing.Color.Black;
                specular = RayTracing.Color.Black;
            }
            else
            {
                diffuse = effectiveColor * Diffuse * lightDotNormal;

                var reflectv = (-lightv).Reflect(normalv);
                var reflectDotEye = reflectv.Dot(eyev);

                if (reflectDotEye <= 0.0)
                    specular = Color.Black;
                else
                {
                    var factor = System.Math.Pow(reflectDotEye, Shininess);
                    specular = light.Intensity * Specular * factor;
                }
            }

            if (inShadow)
                return ambient;
            return ambient + diffuse + specular;
        }
    }
}