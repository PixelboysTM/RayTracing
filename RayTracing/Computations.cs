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
        public Tuple ReflectV { get; set; }
    }
}