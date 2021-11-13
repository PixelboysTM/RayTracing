using System;

namespace RayTracing
{
    public struct Tuple
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public Tuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public bool IsPoint => W.Is(1.0);
        public bool IsVector => !IsPoint;
        public double Magnitude => Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        public Tuple Normalised => new Tuple(X, Y, Z, W) / Magnitude;

        public double Dot(Tuple b)
        {
            return X * b.X + Y * b.Y + Z * b.Z + W * b.W;
        }

        public Tuple Cross(Tuple b)
        {
            if (!IsVector)
                throw new InvalidOperationException("Cross product can only be applied to Vectors!");
            return Vector(Y * b.Z - Z * b.Y, Z * b.X - X * b.Z, X * b.Y - Y * b.X);
        }

        private bool Equals(Tuple other)
        {
            return X.Is(other.X) && Y.Is(other.Y) && Z.Is(other.Z) && W.Is(other.W);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((Tuple)obj);
        }

        public static bool operator ==(Tuple left, Tuple right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tuple left, Tuple right)
        {
            return !Equals(left, right);
        }

        public static Tuple operator +(Tuple tuple, Tuple right)
        {
            return new Tuple(tuple.X + right.X, tuple.Y + right.Y, tuple.Z + right.Z, tuple.W + right.W);
        }
        
        public static Tuple operator -(Tuple left, Tuple right)
        {
            return new Tuple(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }
        
        public static Tuple operator -(Tuple tuple)
        {
            return new Tuple(-tuple.X, -tuple.Y, -tuple.Z, -tuple.W);
        }
        
        public static Tuple operator *(Tuple tuple, double scalar)
        {
            return new Tuple(tuple.X * scalar, tuple.Y * scalar, tuple.Z * scalar, tuple.W * scalar);
        }
        
        public static Tuple operator /(Tuple tuple, double scalar)
        {
            return new Tuple(tuple.X / scalar, tuple.Y / scalar, tuple.Z / scalar, tuple.W / scalar);
        }

        public static Tuple Point(double x, double y, double z) => new(x, y, z, 1);
        public static Tuple Vector(double x, double y, double z) => new(x, y, z, 0);
    }
}