namespace RayTracing.Light
{
    public struct PointLight
    {
        public Tuple Position { get; init; }
        public Color Intensity { get; init; }

        public PointLight(Tuple position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}