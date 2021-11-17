
using System;

namespace RayTracing
{
    public class Camera
    {
        public int HSize { get; }
        public int VSize { get; }
        public double FieldOfView { get; }
        public Matrix4x4 Transform { get; set; } = Matrix4x4.Identity;
        
        public double HalfWidth { get; private set; }
        public double HalfHeight { get; private set; }
        public double PixelSize { get; }

        public Camera(int hSize, int vSize, double fieldOfView)
        {
            HSize = hSize;
            VSize = vSize;
            FieldOfView = fieldOfView;
            
            var halfView = System.Math.Tan(FieldOfView / 2.0);
            var aspect =  1.0 * HSize / VSize;

            if (aspect >= 1)
            {
                HalfWidth = halfView;
                HalfHeight = halfView / aspect;
            }
            else
            {
                HalfWidth = halfView * aspect;
                HalfHeight = halfView;
            }

            PixelSize = (HalfWidth * 2) / HSize;
        }

        public Ray RayForPixel(int px, int py)
        {
            var xOffset = (px + 0.5) * PixelSize;
            var yOffset = (py + 0.5) * PixelSize;

            var worldX = HalfWidth - xOffset;
            var worldY = HalfHeight - yOffset;

            var pixel = Transform.Inverse * Tuple.Point(worldX, worldY, -1);
            var origin = Transform.Inverse * Tuple.Point(0, 0, 0);
            var direction = (pixel - origin).Normalised;

            return new Ray(origin, direction);
        }

        public Canvas Render(World world, bool print = true)
        {
            var image = new Canvas(HSize, VSize);

            for (int y = 0; y < VSize; y++)
            {
                if (print)
                    Console.WriteLine($"Row {y+1}/{VSize}");
                for (int x = 0; x < HSize; x++)
                {
                    var ray = RayForPixel(x, y);
                    var color = world.ColorAt(ray);
                    image[x, y] = color;
                }
            }

            return image;
        }
    }
}