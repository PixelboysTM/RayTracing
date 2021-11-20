using RayTracing.Shapes;

namespace RayTracing
{
    public struct Computations
    {
        public double t { get; set; }
        public Shape Object { get; set; }
        public Tuple Point { get; set; }
        public Tuple EyeV { get; set; }
        public Tuple NormalV { get; set; }
        public bool Inside { get; set; }
        public Tuple OverPoint { get; set; }
        public Tuple UnderPoint { get; set; }
        public Tuple ReflectV { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }

        public double Schlick()
        {
            var cos = EyeV.Dot(NormalV);

            if (N1 > N2)
            {
                var n = N1 / N2;
                var sin2T = n * n * (1.0 - cos * cos);
                if (sin2T > 1.0)
                    return 1.0;

                var cosT = (1.0 - sin2T).Sqrt();
                cos = cosT;
            }

            var r0 = ((N1 - N2) / (N1 + N2)) * ((N1 - N2) / (N1 + N2));
            return r0 + (1 - r0) * System.Math.Pow(1 - cos, 5);
        }
    }
}