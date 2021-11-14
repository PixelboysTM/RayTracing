using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;

namespace RayTracing
{
    public struct Matrix4x4 : IEnumerable
    {
        private double _m00;
        private double _m01;
        private double _m02;
        private double _m03;
        
        private double _m10;
        private double _m11;
        private double _m12;
        private double _m13;
        
        private double _m20;
        private double _m21;
        private double _m22;
        private double _m23;
        
        private double _m30;
        private double _m31;
        private double _m32;
        private double _m33;


        public double this[int column, int row]
        {
            get => (column, row) switch
            {
                (0,0) => _m00,
                (0,1) => _m01,
                (0,2) => _m02,
                (0,3) => _m03,
                
                (1,0) => _m10,
                (1,1) => _m11,
                (1,2) => _m12,
                (1,3) => _m13,
                
                (2,0) => _m20,
                (2,1) => _m21,
                (2,2) => _m22,
                (2,3) => _m23,
                
                (3,0) => _m30,
                (3,1) => _m31,
                (3,2) => _m32,
                (3,3) => _m33,
                
                _ => throw new IndexOutOfRangeException($"Index[{column}|{row}] not valid only range 0-3 is valid!")
            };

            set {
                switch (column, row)
                {
                    case (0,0): _m00 = value; break;
                    case (0,1): _m01 = value; break;
                    case (0,2): _m02 = value; break;
                    case (0,3): _m03 = value; break;
                    
                    case (1,0): _m10 = value; break;
                    case (1,1): _m11 = value; break;
                    case (1,2): _m12 = value; break;
                    case (1,3): _m13 = value; break;
                    
                    case (2,0): _m20 = value; break;
                    case (2,1): _m21 = value; break;
                    case (2,2): _m22 = value; break;
                    case (2,3): _m23 = value; break;
                    
                    case (3,0): _m30 = value; break;
                    case (3,1): _m31 = value; break;
                    case (3,2): _m32 = value; break;
                    case (3,3): _m33 = value; break;
                    
                    default:
                        throw new IndexOutOfRangeException($"Index[{column}|{row}] not valid only range 0-3 is valid!");
                }
            }
        }

        public void Add(
            double m00, double m01, double m02, double m03,
            double m10, double m11, double m12, double m13,
            double m20, double m21, double m22, double m23,
            double m30, double m31, double m32, double m33
            )
        {
            _m00 = m00;
            _m01 = m01;
            _m02 = m02;
            _m03 = m03;
            
            _m10 = m10;
            _m11 = m11;
            _m12 = m12;
            _m13 = m13;
            
            _m20 = m20;
            _m21 = m21;
            _m22 = m22;
            _m23 = m23;
            
            _m30 = m30;
            _m31 = m31;
            _m32 = m32;
            _m33 = m33;
        }


        public IEnumerator GetEnumerator()
        {
            return null;
        }

        private bool Equals(Matrix4x4 other)
        {
            return
                _m00.Is(other._m00) &&
                _m01.Is(other._m01) &&
                _m02.Is(other._m02) &&
                _m03.Is(other._m03) &&

                _m10.Is(other._m10) &&
                _m11.Is(other._m11) &&
                _m12.Is(other._m12) &&
                _m13.Is(other._m13) &&

                _m20.Is(other._m20) &&
                _m21.Is(other._m21) &&
                _m22.Is(other._m22) &&
                _m23.Is(other._m23) &&

                _m30.Is(other._m30) &&
                _m31.Is(other._m31) &&
                _m32.Is(other._m32) &&
                _m33.Is(other._m33);
        }

        public static bool operator ==(Matrix4x4 left, Matrix4x4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix4x4 left, Matrix4x4 right)
        {
            return !(left == right);
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            return new Matrix4x4
            {
                {
                    a._m00 * b._m00 + a._m01 * b._m10 + a._m02 * b._m20 + a._m03 * b._m30,
                    a._m00 * b._m01 + a._m01 * b._m11 + a._m02 * b._m21 + a._m03 * b._m31,
                    a._m00 * b._m02 + a._m01 * b._m12 + a._m02 * b._m22 + a._m03 * b._m32,
                    a._m00 * b._m03 + a._m01 * b._m13 + a._m02 * b._m23 + a._m03 * b._m33,
                    
                    
                    a._m10 * b._m00 + a._m11 * b._m10 + a._m12 * b._m20 + a._m13 * b._m30,
                    a._m10 * b._m01 + a._m11 * b._m11 + a._m12 * b._m21 + a._m13 * b._m31,
                    a._m10 * b._m02 + a._m11 * b._m12 + a._m12 * b._m22 + a._m13 * b._m32,
                    a._m10 * b._m03 + a._m11 * b._m13 + a._m12 * b._m23 + a._m13 * b._m33,
                    
                    a._m20 * b._m00 + a._m21 * b._m10 + a._m22 * b._m20 + a._m23 * b._m30,
                    a._m20 * b._m01 + a._m21 * b._m11 + a._m22 * b._m21 + a._m23 * b._m31,
                    a._m20 * b._m02 + a._m21 * b._m12 + a._m22 * b._m22 + a._m23 * b._m32,
                    a._m20 * b._m03 + a._m21 * b._m13 + a._m22 * b._m23 + a._m23 * b._m33,
                    
                    a._m30 * b._m00 + a._m31 * b._m10 + a._m32 * b._m20 + a._m33 * b._m30,
                    a._m30 * b._m01 + a._m31 * b._m11 + a._m32 * b._m21 + a._m33 * b._m31,
                    a._m30 * b._m02 + a._m31 * b._m12 + a._m32 * b._m22 + a._m33 * b._m32,
                    a._m30 * b._m03 + a._m31 * b._m13 + a._m32 * b._m23 + a._m33 * b._m33
                }
            };
        }

        public Matrix4x4 Transpose => new()
        {
            {
                _m00,_m10,_m20,_m30,
                _m01,_m11,_m21,_m31,
                _m02,_m12,_m22,_m32,
                _m03,_m13,_m23,_m33
            }
        };
        
        public Matrix3x3 SubMatrix(int row, int column)
        {
            if (row < 0 || row > 3 || column < 0 || column > 3)
                throw new IndexOutOfRangeException("Row and Column must be in range 0-2");
            
            Matrix3x3 m = new Matrix3x3();
            int ix = 0;
            for (int x = 0; x < 4; x++)
            {
                if (row == x)
                    continue;
                int iy = 0;
                for (int y = 0; y < 4; y++)
                {
                    if (column == y)
                        continue;

                    m[ix, iy] = this[x,y];
                    
                    iy++;
                }

                ix++;
            }

            return m;
        }
        

        public static Tuple operator *(Matrix4x4 m, Tuple t)
        {
            return new Tuple(
                m._m00 * t.X + m._m01 * t.Y + m._m02 * t.Z + m._m03 * t.W,
                m._m10 * t.X + m._m11 * t.Y + m._m12 * t.Z + m._m13 * t.W,
                m._m20 * t.X + m._m21 * t.Y + m._m22 * t.Z + m._m23 * t.W,
                m._m30 * t.X + m._m31 * t.Y + m._m32 * t.Z + m._m33 * t.W
            );
        }

        public static Matrix4x4 Identity => new()
        {
            {
                1,0,0,0,
                0,1,0,0,
                0,0,1,0,
                0,0,0,1
            }
        };

        public double Determinant => _m00 * Cofactor(0, 0) + _m01 * Cofactor(0, 1) + _m02 * Cofactor(0, 2) +
                                     _m03 * Cofactor(0, 3);

        public bool Invertible => !Determinant.Is(0);
        public Matrix4x4 Inverse {
            get
            {
                if (!Invertible)
                    throw new InvalidOperationException("Matrix is not invertible!");

                var m2 = new Matrix4x4();
                var det = Determinant;
                for (int row = 0; row < 4; row++)
                {
                    for (int column = 0; column < 4; column++)
                    {
                        var c = Cofactor(row, column);
                        m2[column, row] = c / det;
                    }
                }

                return m2;
            }
        }


        public double Minor(int row, int column)
        {
            return SubMatrix(row, column).Determinant;
        }

        public double Cofactor(int row, int column)
        {
            return (row + column) % 2 == 0 ? Minor(row, column) : -Minor(row, column);
        }
    }
}