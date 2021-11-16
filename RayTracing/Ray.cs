namespace RayTracing
{
    public struct Ray
    {
        public Tuple Origin { get; init; }
        public Tuple Direction { get; init; }

        public Ray(Tuple origin, Tuple direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Tuple Position(double t) => Origin + Direction * t;

        public Ray Transform(Matrix4x4 mat)
            => new(mat * Origin, mat * Direction);
    }
}