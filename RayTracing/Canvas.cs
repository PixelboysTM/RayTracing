using System;
using System.Drawing;
using System.IO;

namespace RayTracing
{
    public class Canvas
    {
        public int Width { get; init; }
        public int Height { get; init; }

        private Color[,] _data;

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            _data = new Color[Width, Height];
        }

        public bool Is(Color color)
        {
            foreach (var color1 in _data)
            {
                if (color1 != color)
                    return false;
            }
            return true;
        }

        public Color this[int x, int y]
        {
            get => _data[x, y].Copy;
            set => _data[x, y] = value.Copy;
        }

        
        public void Save(string file)
        {
            Bitmap bitmap = new Bitmap((int)Width, (int)Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var c = _data[x, y];
                    bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, C(c.Red), C(c.Green), C(c.Blue)));
                }
            }
            if (file.Contains('/'))
                Directory.CreateDirectory(file.Substring(0, file.LastIndexOf('/')));
            File.Create(file).Close();
            bitmap.Save(file);
        }

        private int C(double c)
        {
            return (int)System.Math.Clamp(c * 255, 0, 255);
        }
    }
}