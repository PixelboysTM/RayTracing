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

        public static bool operator ==(PointLight left, PointLight right)
        {
            return left.Intensity == right.Intensity && left.Position == right.Position;
        }

        public static bool operator !=(PointLight left, PointLight right)
        {
            return !(left == right);
        }
    }
}