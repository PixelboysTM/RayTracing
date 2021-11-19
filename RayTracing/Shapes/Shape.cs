namespace RayTracing.Shapes
{
    public abstract class Shape
    {
        public Matrix4x4 Transform { get; set; } = Matrix4x4.Identity;
        
        public Material Material { get; set; } = new();

        public Intersection[] Intersect(Ray ray)
        {
            var localRay = ray.Transform(Transform.Inverse);
            return LocalIntersect(localRay);
        }

        protected abstract Intersection[] LocalIntersect(Ray localRay);

        public Tuple NormalAt(Tuple point)
        {
            var localPoint = Transform.Inverse * point;
            var localNormal = LocalNormalAt(localPoint);
            var worldNormal = Transform.Inverse.Transpose * localNormal;
            worldNormal.W = 0;
            return worldNormal.Normalised;
        }

        protected abstract Tuple LocalNormalAt(Tuple localPoint);
        
        public static bool operator ==(Shape left, Shape right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;
            
            
            return left.GetType() == right.GetType() && left.Material == right.Material && left.Transform == right.Transform;
        }

        public static bool operator !=(Shape left, Shape right)
        {
            return !(left == right);
        }
    }
}