
using System.ComponentModel.Design;
using RayTracing;
using RayTracing.Light;
using RayTracing.Patterns;
using RayTracing.Shapes;
using Tuple = RayTracing.Tuple;
using static RayTracing.Tuple;
using Color = RayTracing.Color;
using Math = RayTracing.Math;

namespace ImageRenderer
{
    class Program
    {
        static void Main(string[] args)
        {
            var floor = new Plane
                {
                    Transform = Transformation.Scaling(10, 0.01, 10),
                    Material = new Material
                    {
                        Color = new Color(1, 0.9, 0.9),
                        Specular = 0
                    }
                };

                var leftWall = new Plane
                {
                    Transform = Transformation.Translation(0, 0, 5) * Transformation.RotationY(-RayTracing.Math.Pi / 4.0) *
                                Transformation.RotationX(RayTracing.Math.Pi / 2.0) * Transformation.Scaling(10, 0.01, 10), //NOTE: If not working Check Order on page 195
                    Material = new Material
                    {
                        Color = new Color(0.1,0.1,0.1),
                        Reflective = 0.5,
                        Specular = 0
                    }
                };

                var rightWall = new Plane
                {
                    Transform = Transformation.Translation(0, 0, 5) * Transformation.RotationY(RayTracing.Math.Pi / 4.0) *
                                Transformation.RotationX(RayTracing.Math.Pi / 2.0), //NOTE: If not working Check Order on page 195
                    Material = new Material
                    {
                        Pattern = new CheckerPattern(new Color(1,0,0), new Color(0,1,0)),
                        // {
                        //     Transform = Transformation.Translation(0,0.1,0)
                        // },
                        Specular = 0,
                        Color = new Color(1,0.9,0.9)
                    }
                };
                

                var middle = new Sphere
                {
                    Transform = Transformation.Translation(-0.5, 1, 0.5),
                    Material = new Material
                    {
                        Color = new Color(0.1, 1, 0.5),
                        Diffuse = 0.7,
                        Specular = 0.3,
                        Reflective = 0.05
                    }
                };

                var right = new Sphere
                {
                    Transform = Transformation.Translate(1.5, 0.5, -0.5).Scale(0.5, 0.5, 0.5).Build,
                    Material = new Material
                    {
                        Color = new Color(0.5, 1, 0.1),
                        Diffuse = 0.7,
                        Specular = 0.3
                    }
                };

                var left = new Sphere
                {
                    Transform = Transformation.Translate(-1.5, 0.33, -0.75).Scale(0.33, 0.33, 0.33).Build,
                    Material = new Material
                    {
                        Color = new Color(1, 0.8, 0.1),
                        Diffuse = 0.7,
                        Specular = 0.3
                    }
                };

                var world = new World();
                world.Objects.Add(floor);
                world.Objects.Add(leftWall);
                world.Objects.Add(rightWall);
                world.Objects.Add(middle);
                world.Objects.Add(left);
                world.Objects.Add(right);

                world.Light = new PointLight(Point(-10, 10, -10), new Color(1, 1, 1));
                var camera = new Camera(192, 108, RayTracing.Math.Pi / 2.0);
                camera.Transform = Transformation.View(Point(0, 1.5, -5), Point(0, 1, 0), Vector(0, 1, 0));
                
                var canvas = camera.Render(world);
                canvas.Save("img/Chapter11/reflection2.png");
        }
    }
}