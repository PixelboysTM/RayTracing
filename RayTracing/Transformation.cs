namespace RayTracing
{
    public static class Transformation
    {
        public static Matrix4x4 Translation(double x, double y, double z)
            => new()
            {
                {
                    1, 0, 0, x,
                    0, 1, 0, y,
                    0, 0, 1, z,
                    0, 0, 0, 1
                }
            };

        public static Matrix4x4 Scaling(double x, double y, double z)
            => new()
            {
                {
                    x, 0, 0, 0,
                    0, y, 0, 0,
                    0, 0, z, 0,
                    0, 0, 0, 1
                }
            };

        public static Matrix4x4 RotationX(double rad)
            => new()
            {
                {
                    1, 0, 0, 0,
                    0, Math.Cos(rad), -Math.Sin(rad), 0,
                    0, Math.Sin(rad), Math.Cos(rad), 0,
                    0, 0, 0, 1
                }
            };

        public static Matrix4x4 RotationY(double rad)
            => new()
            {
                {
                    Math.Cos(rad), 0, Math.Sin(rad), 0,
                    0, 1, 0, 0,
                    -Math.Sin(rad), 0, Math.Cos(rad), 0,
                    0, 0, 0, 1
                }
            };

        public static Matrix4x4 RotationZ(double rad)
            => new()
            {
                {
                    Math.Cos(rad), -Math.Sin(rad), 0, 0,
                    Math.Sin(rad), Math.Cos(rad), 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                }
            };

        public static Matrix4x4 Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
            => new()
            {
                {
                    1, xy, xz, 0,
                    yx, 1, yz, 0,
                    zx, zy, 1, 0,
                    0, 0, 0, 1
                }
            };

        public static CTranslation Translate(double x, double y, double z) => new(x, y, z);
        public static CScaling Scale(double x, double y, double z) => new(x, y, z);
        public static CRotate Rotate(double x, double y, double z) => new(x, y, z);
        
        public class CTranslation
        {
            private readonly double _x, _y, _z;
            public Matrix4x4 Build => Translation(_x, _y, _z);

            internal CTranslation(double x, double y, double z)
            {
                _x = x;
                _y = y;
                _z = z;
            }

            public CScaling Scale(double x, double y, double z) => new CScaling(x, y, z, this);

        }
        
        public class CScaling
        {
            private readonly double _x, _y, _z;
            private CTranslation _cTranslation;

            internal CScaling(double x, double y, double z, CTranslation cTranslation = null)
            {
                _x = x;
                _y = y;
                _z = z;
                _cTranslation = cTranslation ?? new CTranslation(0,0,0);
            }
            
            public Matrix4x4 Build => _cTranslation.Build * Scaling(_x,_y,_z);
            public CRotate Rotate(double x, double y, double z) => new CRotate(x, y, z, this);
        }
        
        public class CRotate
        {
            private readonly double _x, _y, _z;
            private CScaling _cScaling;

            internal CRotate(double x, double y, double z, CScaling cScaling = null)
            {
                _x = x;
                _y = y;
                _z = z;
                _cScaling = cScaling??new CScaling(0,0,0);
            }
            
            public Matrix4x4 Build => _cScaling.Build * RotationX(_x) * RotationY(_y) * RotationZ(_z);
        }

        public static Matrix4x4 View(Tuple from, Tuple to, Tuple up)
        {
            var forward = (to - from).Normalised;
            var upn = up.Normalised;
            var left = forward.Cross(upn);
            var trueUp = left.Cross(forward);

            var orientation = new Matrix4x4
            {
                {
                    left.X, left.Y, left.Z, 0,
                    trueUp.X, trueUp.Y, trueUp.Z, 0,
                    -forward.X, -forward.Y, -forward.Z, 0,
                    0, 0, 0, 1
                }
            };
            return orientation * Translation(-from.X, -from.Y, -from.Z);
        }
    }
}