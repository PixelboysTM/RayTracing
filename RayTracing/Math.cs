namespace RayTracing
{
    public static class Math
    {
        public static double Sin(double radians) => System.Math.Sin(radians);
        public static double Cos(double radians) => System.Math.Cos(radians);

        public static double Pi => System.Math.PI;

        public static double DegToRad(double deg) => deg / 180.0 * System.Math.PI;
        public static double RadToDeg(double rad) => rad * 180.0 / System.Math.PI;
    }
}