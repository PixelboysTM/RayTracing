using System;
using System.Collections;
using System.Data;

namespace RayTracing
{
    public struct Matrix2x2 : IEnumerable
    {
        private double _m00;
        private double _m01;
        
        private double _m10;
        private double _m11;


        public double this[int column, int row]
        {
            get => (column, row) switch
            {
                (0,0) => _m00,
                (0,1) => _m01,
                
                (1,0) => _m10,
                (1,1) => _m11,
                
                _ => throw new IndexOutOfRangeException($"Index[{column}|{row}] not valid only range 0-3 is valid!")
            };

            set {
                switch (column, row)
                {
                    case (0,0): _m00 = value; break;
                    case (0,1): _m01 = value; break;
                    
                    case (1,0): _m10 = value; break;
                    case (1,1): _m11 = value; break;
                    
                    default:
                        throw new IndexOutOfRangeException($"Index[{column}|{row}] not valid only range 0-3 is valid!");
                }
            }
        }

        public void Add(
            double m00, double m01,
            double m10, double m11
            )
        {
            _m00 = m00;
            _m01 = m01;
            
            _m10 = m10;
            _m11 = m11;
        }

        public double Determinant => _m00 * _m11 - _m01 * _m10;

        
        
        public IEnumerator GetEnumerator()
        {
            return null;
        }
        
        private bool Equals(Matrix2x2 other)
        {
            return
                _m00.Is(other._m00) &&
                _m01.Is(other._m01) &&

                _m10.Is(other._m10) &&
                _m11.Is(other._m11);
        }

        public static bool operator ==(Matrix2x2 left, Matrix2x2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix2x2 left, Matrix2x2 right)
        {
            return !(left == right);
        }
    }
}