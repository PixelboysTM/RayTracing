using System;

namespace RayTracing
{
    public static class Extension
    {
        public static bool Is(this double a, double b)
        {
            return System.Math.Abs(a - b) < Constant.Epsilon;
        }

        public static double Sqrt(this double value)
        {
            return System.Math.Sqrt(value);
        }
    }
}