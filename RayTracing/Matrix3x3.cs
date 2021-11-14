using System;
using System.Collections;
using System.Data;

namespace RayTracing
{
    public struct Matrix3x3 : IEnumerable
    {
        private double _m00;
        private double _m01;
        private double _m02;
        
        private double _m10;
        private double _m11;
        private double _m12;
        
        private double _m20;
        private double _m21;
        private double _m22;


        public double this[int column, int row]
        {
            get => (column, row) switch
            {
                (0,0) => _m00,
                (0,1) => _m01,
                (0,2) => _m02,
                
                (1,0) => _m10,
                (1,1) => _m11,
                (1,2) => _m12,
                
                (2,0) => _m20,
                (2,1) => _m21,
                (2,2) => _m22,
                
                _ => throw new IndexOutOfRangeException($"Index[{column}|{row}] not valid only range 0-3 is valid!")
            };

            set {
                switch (column, row)
                {
                    case (0,0): _m00 = value; break;
                    case (0,1): _m01 = value; break;
                    case (0,2): _m02 = value; break;
                    
                    case (1,0): _m10 = value; break;
                    case (1,1): _m11 = value; break;
                    case (1,2): _m12 = value; break;
                    
                    case (2,0): _m20 = value; break;
                    case (2,1): _m21 = value; break;
                    case (2,2): _m22 = value; break;
                    
                    default:
                        throw new IndexOutOfRangeException($"Index[{column}|{row}] not valid only range 0-3 is valid!");
                }
            }
        }

        public double Determinant => Cofactor(0, 0) * _m00 + Cofactor(0, 1) * _m01 + Cofactor(0, 2) * _m02;

        public void Add(
            double m00, double m01, double m02,
            double m10, double m11, double m12,
            double m20, double m21, double m22
            )
        {
            _m00 = m00;
            _m01 = m01;
            _m02 = m02;
            
            _m10 = m10;
            _m11 = m11;
            _m12 = m12;
            
            _m20 = m20;
            _m21 = m21;
            _m22 = m22;
        }
        
        private bool Equals(Matrix3x3 other)
        {
            return
                _m00.Is(other._m00) &&
                _m01.Is(other._m01) &&
                _m02.Is(other._m02) &&

                _m10.Is(other._m10) &&
                _m11.Is(other._m11) &&
                _m12.Is(other._m12) &&

                _m20.Is(other._m20) &&
                _m21.Is(other._m21) &&
                _m22.Is(other._m22);
        }

        public static bool operator ==(Matrix3x3 left, Matrix3x3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix3x3 left, Matrix3x3 right)
        {
            return !(left == right);
        }


        public IEnumerator GetEnumerator()
        {
            return null;
        }

        public Matrix2x2 SubMatrix(int row, int column)
        {
            if (row < 0 || row > 2 || column < 0 || column > 2)
                throw new IndexOutOfRangeException("Row and Column must be in range 0-2");
            
            Matrix2x2 m = new Matrix2x2();
            int ix = 0;
            for (int x = 0; x < 3; x++)
            {
                if (row == x)
                    continue;
                int iy = 0;
                for (int y = 0; y < 3; y++)
                {
                    if (column == y)
                        continue;

                    m[ix,iy] = this[x,y];
                    
                    iy++;
                }

                ix++;
            }

            return m;
        }

        public double Minor(int row, int column) => SubMatrix(row, column).Determinant;

        public double Cofactor(int row, int column)
        {
            var minor = Minor(row, column);
            return row == 1 && column == 0 ||
                   row == 0 && column == 1 ||
                   row == 2 && column == 1 ||
                   row == 1 && column == 2
                ? -minor
                : minor;
        }
    }
}