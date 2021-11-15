using System;
using System.Drawing;
using System.Text.RegularExpressions;
using NUnit.Framework;
using RayTracing;
using Tuple = RayTracing.Tuple;
using static RayTracing.Tuple;
using Color = RayTracing.Color;
using Math = RayTracing.Math;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Chapter1()
        {
            {
                var a = new Tuple(4.3, -4.2, 3.1, 1.0);
                
                Assert.IsTrue(a.X.Is(4.3));
                Assert.IsTrue(a.Y.Is(-4.2));
                Assert.IsTrue(a.Z.Is(3.1));
                Assert.IsTrue(a.W.Is(1.0));
                Assert.IsTrue(a.IsPoint);
                Assert.IsFalse(a.IsVector);
            }
            
            {
                var a = new Tuple(4.3, -4.2, 3.1, 0.0);
                
                Assert.IsTrue(a.X.Is(4.3));
                Assert.IsTrue(a.Y.Is(-4.2));
                Assert.IsTrue(a.Z.Is(3.1));
                Assert.IsTrue(a.W.Is(0.0));
                Assert.IsFalse(a.IsPoint);
                Assert.IsTrue(a.IsVector);
            }

            {
                var p = Point(4, -4, 3);
                Assert.IsTrue(p == new Tuple(4, -4, 3, 1));
            }

            {
                var v = Vector(4, -4, 3);
                Assert.IsTrue(v == new Tuple(4, -4, 3, 0));
            }

            {
                var a1 = new Tuple(3, -2, 5, 1);
                var a2 = new Tuple(-2, 3, 1, 0);
                Assert.IsTrue(a1 + a2 == new Tuple(1,1,6,1));
            }

            {
                var p1 = Point(3, 2, 1);
                var p2 = Point(5, 6, 7);
                Assert.IsTrue(p1 - p2 == Vector(-2, -4, -6));
            }
            
            {
                var p = Point(3, 2, 1);
                var v = Vector(5, 6, 7);
                Assert.IsTrue(p - v == Point(-2, -4, -6));
            }

            {
                var v1 = Vector(3, 2, 1);
                var v2 = Vector(5, 6, 7);
                Assert.IsTrue(v1 - v2 == Vector(-2, -4, -6));
            }

            {
                var zero = Vector(0, 0, 0);
                var v = Vector(1, -2, 3);
                Assert.IsTrue(zero - v == Vector(-1,2,-3));
            }

            {
                var a = new Tuple(1, -2, 3, -4);
                Assert.IsTrue(-a  == new Tuple(-1,2,-3,4));
            }

            {
                var a = new Tuple(1, -2, 3, -4);
                Assert.IsTrue(a * 3.5 == new Tuple(3.5, -7, 10.5, -14));
            }

            {
                var a = new Tuple(1, -2, 3, -4);
                Assert.IsTrue(a * 0.5 == new Tuple(0.5, -1, 1.5, -2));
            }

            {
                var a = new Tuple(1, -2, 3, -4);
                Assert.IsTrue(a / 2 == new Tuple(0.5, -1, 1.5, -2));
            }

            {
                var v = Vector(1, 0, 0);
                Assert.IsTrue(v.Magnitude.Is(1));
            }
            
            {
                var v = Vector(0, 1, 0);
                Assert.IsTrue(v.Magnitude.Is(1));
            }
            
            {
                var v = Vector(0, 0, 1);
                Assert.IsTrue(v.Magnitude.Is(1));
            }
            
            {
                var v = Vector(1, 2, 3);
                Assert.IsTrue(v.Magnitude.Is(14.0.Sqrt()));
            }
            
            {
                var v = Vector(-1, -2, -3);
                Assert.IsTrue(v.Magnitude.Is(14.0.Sqrt()));
            }

            {
                var v = Vector(4, 0, 0);
                Assert.IsTrue(v.Normalised == Vector(1,0,0));
            }
            
            {
                var v = Vector(1, 2, 3);
                Assert.IsTrue(v.Normalised == Vector(0.26726,0.53452,0.80178));
            }

            {
                var v = Vector(1, 2, 3);
                var norm = v.Normalised;
                Assert.IsTrue(norm.Magnitude.Is(1));
            }

            {
                var a = Vector(1, 2, 3);
                var b = Vector(2, 3, 4);
                Assert.IsTrue(a.Dot(b).Is(20));
            }

            {
                var a = Vector(1, 2, 3);
                var b = Vector(2, 3, 4);
                Assert.IsTrue(a.Cross(b) == Vector(-1, 2, -1));
                Assert.IsTrue(b.Cross(a) == Vector(1, -2, 1));
            }
        }

        [Test]
        public void Chapter2()
        {
            {
                var c = new Color(-0.5, 0.4, 1.7);
                Assert.IsTrue(c.Red.Is(-0.5));
                Assert.IsTrue(c.Green.Is(0.4));
                Assert.IsTrue(c.Blue.Is(1.7));
            }

            {
                var c1 = new Color(0.9, 0.6, 0.75);
                var c2 = new Color(0.7, 0.1, 0.25);
                Assert.IsTrue(c1 + c2 == new Color(1.6, 0.7, 1.0));
            }
            
            {
                var c1 = new Color(0.9, 0.6, 0.75);
                var c2 = new Color(0.7, 0.1, 0.25);
                Assert.IsTrue(c1 - c2 == new Color(0.2, 0.5, 0.5));
            }

            {
                var c = new Color(0.2, 0.3, 0.4);
                Assert.IsTrue( c * 2 == new Color(0.4,0.6, 0.8));
            }

            {
                var c1 = new Color(1, 0.2, 0.4);
                var c2 = new Color(0.9, 1, 0.1);
                Assert.IsTrue(c1 * c2 == new Color(0.9, 0.2, 0.04));
            }

            {
                var c = new Canvas(10, 20);
                Assert.IsTrue(c.Width == 10);
                Assert.IsTrue(c.Height == 20);
                Assert.IsTrue(c.Is(new Color(0,0,0)));
            }

            {
                var c = new Canvas(10, 20);
                var red = new Color(1, 0, 0);
                c[2, 3] = red;
                Assert.IsTrue(c[2,3] == red);
            }

            {
                var c = new Canvas(10, 20);
                c[2, 3] = new Color(1, 0, 0);
                c.Save("img/chapter2/001.png");
            }
        }

        [Test]
        public void Chapter3()
        {
            {
                var m = new Matrix4x4
                {
                    {
                        1, 2, 3, 4,
                        5.5, 6.5, 7.5, 8.5,
                        9, 10, 11, 12,
                        13.5, 14.5, 15.5, 16.5
                    }
                };
                Assert.IsTrue(m[0,0].Is(1));
                Assert.IsTrue(m[0,3].Is(4));
                Assert.IsTrue(m[1,0].Is(5.5));
                Assert.IsTrue(m[1,2].Is(7.5));
                Assert.IsTrue(m[2,2].Is(11));
                Assert.IsTrue(m[3,0].Is(13.5));
                Assert.IsTrue(m[3,2].Is(15.5));
            }

            {
                var m = new Matrix2x2
                {
                    { -3, 5, 1, -2 }
                };
                Assert.IsTrue(m[0,0].Is(-3));
                Assert.IsTrue(m[0,1].Is(5));
                Assert.IsTrue(m[1,0].Is(1));
                Assert.IsTrue(m[1,1].Is(-2));
            }

            {
                var m = new Matrix3x3
                {
                    { -3, 5, 0, 1, -2, -7, 0, 1, 1 }
                };
                Assert.IsTrue(m[0,0].Is(-3));
                Assert.IsTrue(m[1,1].Is(-2));
                Assert.IsTrue(m[2,2].Is(1));
            }

            {
                var A = new Matrix4x4
                {
                    {
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                        9, 8, 7, 6,
                        5, 4, 3, 2
                    }
                };

                var B = new Matrix4x4
                {
                    {
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                        9, 8, 7, 6,
                        5, 4, 3, 2
                    }
                };
                Assert.IsTrue(A == B);
            }
            
            {
                var A = new Matrix4x4
                {
                    {
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                        9, 8, 7, 6,
                        5, 4, 3, 2
                    }
                };

                var B = new Matrix4x4
                {
                    {
                        2, 3, 4, 5, 
                        6, 7, 8, 9, 
                        8, 7, 6, 5, 
                        4, 3, 2, 1
                    }
                };
                Assert.IsTrue(A != B);
            }

            {
                var A = new Matrix4x4
                {
                    {
                        1,2,3,4,
                        5,6,7,8,
                        9,8,7,6,
                        5,4,3,2
                    }
                };

                var B = new Matrix4x4
                {
                    {
                        -2, 1, 2, 3,
                        3, 2, 1, -1,
                        4, 3, 6, 5,
                        1, 2, 7, 8
                    }
                };
                Assert.IsTrue(A * B == new Matrix4x4
                {
                    {
                        20, 22, 50, 48,
                        44, 54, 114, 108,
                        40, 58, 110, 102,
                        16, 26, 46, 42
                    }
                });
            }

            {
                var A = new Matrix4x4
                {
                    {
                        1, 2, 3, 4,
                        2, 4, 4, 2,
                        8, 6, 4, 1,
                        0, 0, 0, 1
                    }
                };
                var b = new Tuple(1, 2, 3, 1);
                Assert.IsTrue(A * b == new Tuple(18,24,33,1));
            }

            {
                var A = new Matrix4x4
                {
                    {
                        0, 1, 2, 4,
                        1, 2, 4, 8,
                        2, 4, 8, 16,
                        4, 8, 16, 32
                    }
                };
                Assert.IsTrue(A * Matrix4x4.Identity == A);
            }

            {
                var A = new Matrix4x4
                {
                    {
                        0, 9, 3, 0,
                        9, 8, 0, 8,
                        1, 8, 5, 3,
                        0, 0, 5, 8
                    }
                };
                Assert.IsTrue(A.Transpose == new Matrix4x4
                {
                    {
                        0, 9, 1, 0,
                        9, 8, 8, 0,
                        3, 0, 5, 5,
                        0, 8, 3, 8
                    }
                });
            }

            {
                var A = Matrix4x4.Identity.Transpose;
                Assert.IsTrue(A == Matrix4x4.Identity);
            }

            {
                var A = new Matrix2x2
                {
                    {
                        1, 5,
                        -3, 2
                    }
                };
                Assert.IsTrue(A.Determinant.Is(17));
            }

            {
                var A = new Matrix3x3
                {
                    {
                        1, 5, 0,
                        -3, 2, 7,
                        0, 6, -3
                    }
                };
                Assert.IsTrue(A.SubMatrix(0, 2) == new Matrix2x2
                {
                    {
                        -3, 2,
                        0, 6
                    }
                });
            }

            {
                var A = new Matrix4x4
                {
                    {
                        -6, 1, 1, 6,
                        -8, 5, 8, 6,
                        -1, 0, 8, 2,
                        -7, 1, -1, 1
                    }
                };
                Assert.IsTrue(A.SubMatrix(2, 1) == new Matrix3x3
                {
                    {
                        -6, 1, 6,
                        -8, 8, 6,
                        -7, -1, 1
                    }
                });
            }

            {
                var A = new Matrix3x3
                {
                    {
                        3, 5, 0,
                        2, -1, -7,
                        6, -1, 5
                    }
                };
                var B = A.SubMatrix(1, 0);
                Assert.IsTrue(B.Determinant.Is(25));
                Assert.IsTrue(A.Minor(1,0).Is(25));
            }

            {
                var A = new Matrix3x3
                {
                    {
                        3, 5, 0,
                        2, -1, -7,
                        6, -1, 5
                    }
                };
                Assert.IsTrue(A.Minor(0,0).Is(-12));
                Assert.IsTrue(A.Cofactor(0,0).Is(-12));
                Assert.IsTrue(A.Minor(1,0).Is(25));
                Assert.IsTrue(A.Cofactor(1,0).Is(-25));
            }

            {
                var A = new Matrix3x3
                {
                    {
                        1, 2, 6,
                        -5, 8, -4,
                        2, 6, 4
                    }
                };
                Assert.IsTrue(A.Cofactor(0,0).Is(56));
                Assert.IsTrue(A.Cofactor(0,1).Is(12));
                Assert.IsTrue(A.Cofactor(0,2).Is(-46));
                Assert.IsTrue(A.Determinant.Is(-196));
            }

            {
                var A = new Matrix4x4
                {
                    {
                        -2, -8, 3, 5,
                        -3, 1, 7, 3,
                        1, 2, -9, 6,
                        -6, 7, 7, -9
                    }
                };
                Assert.IsTrue(A.Cofactor(0,0).Is(690));
                Assert.IsTrue(A.Cofactor(0,1).Is(447));
                Assert.IsTrue(A.Cofactor(0,2).Is(210));
                Assert.IsTrue(A.Cofactor(0,3).Is(51));
                Assert.IsTrue(A.Determinant.Is(-4071));
            }

            {
                var A = new Matrix4x4
                {
                    {
                        6, 4, 4, 4, 
                        5, 5, 7, 6,
                        4, -9, 3, -7,
                        9, 1, 7, -6
                    }
                };
                Assert.IsTrue(A.Determinant.Is(-2120));
                Assert.IsTrue(A.Invertible);
            }

            {
                var A = new Matrix4x4
                {
                    {
                        -4, 2, -2, -3,
                        9, 6, 2, 6,
                        0, -5, 1, -5,
                        0, 0, 0, 0
                    }
                };
                Assert.IsTrue(A.Determinant.Is(0));
                Assert.IsFalse(A.Invertible);
            }

            {
                var A = new Matrix4x4
                {
                    {
                        -5, 2, 6, -8,
                        1, -5, 1, 8,
                        7, 7, -6, -7,
                        1, -3, 7, 4
                    }
                };
                var B = A.Inverse;
                Assert.IsTrue(A.Determinant.Is(532));
                Assert.IsTrue(A.Cofactor(2,3).Is(-160));
                Assert.IsTrue(B[3,2].Is(-160.0/532.0));
                Assert.IsTrue(A.Cofactor(3,2).Is(105));
                Assert.IsTrue(B[2,3].Is(105.0/532.0));
                Assert.IsTrue(B == new Matrix4x4
                {
                    {
                        0.21805, 0.45113, 0.24060, -0.04511,
                        -0.80827, -1.45677, -0.44361, 0.52068,
                        -0.07895, -0.22368, -0.05263, 0.19737,
                        -0.52256, -0.81391, -0.30075, 0.30639
                    }
                });
            }

            {
                var A = new Matrix4x4
                {
                    {
                        8, -5, 9, 2,
                        7, 5, 6, 1,
                        -6, 0, 9, 6,
                        -3, 0, -9, -4
                    }
                };
                Assert.IsTrue(A.Inverse == new Matrix4x4{
                {
                    -0.15385, -0.15385, -0.28205, -0.53846,
                    -0.07692, 0.12308, 0.02564, 0.03077, 
                    0.35897, 0.35897, 0.43590, 0.92308,
                    -0.69231, -0.69231, -0.76923, -1.92308
                }});
            }

            {
                var A = new Matrix4x4
                {
                    {
                        9, 3, 0, 9,
                        -5, -2, -6, -3,
                        -4, 9, 6, 4,
                        -7, 6, 6, 2
                    }
                };
                Assert.IsTrue(A.Inverse == new Matrix4x4{
                {
                    -0.04074, -0.07778, 0.14444, -0.22222,
                    -0.07778, 0.03333, 0.36667, -0.33333,
                    -0.02901, -0.14630, -0.10926, 0.12963,
                    0.17778, 0.06667, -0.26667, 0.33333
                }});
            }

            {
                var A = new Matrix4x4
                {
                    {
                        3, -9, 7, 3,
                        3, -8, 2, -9,
                        -4, 4, 4, 1,
                        -6, 5, -1, 1
                    }
                };
                var B = new Matrix4x4
                {
                    {
                        8, 2, 2, 2,
                        3, -1, 7, 0,
                        7, 0, 5, 4,
                        6, -2, 0, 5
                    }
                };
                var C = A * B;
                Assert.IsTrue(C * B.Inverse == A);
            }
        }

        [Test]
        public void Chapter4()
        {
            {
                var transform = Transformation.Translation(5, -3, 2);
                var p = Point(-3, 4, 5);
                Assert.IsTrue(transform * p == Point(2, 1, 7));
            }

            {
                var transform = Transformation.Translation(5, -3, 2);
                var inv = transform.Inverse;
                var p = Point(-3, 4, 5);
                Assert.IsTrue(inv * p == Point(-8,7,3));
            }

            {
                var transform = Transformation.Translation(5, -3, 2);
                var v = Vector(-3, 4, 5);
                Assert.IsTrue(transform * v == v);
            }

            {
                var transform = Transformation.Scaling(2, 3, 4);
                var p = Point(-4, 6, 8);
                Assert.IsTrue(transform * p == Point(-8,18,32));
            }

            {
                var transform = Transformation.Scaling(2, 3, 4);
                var v = Vector(-4, 6, 8);
                Assert.IsTrue(transform * v == Vector(-8,18,32));
            }

            {
                var transform = Transformation.Scaling(2, 3, 4);
                var inv = transform.Inverse;
                var v = Vector(-4, 6, 8);
                Assert.IsTrue(inv * v == Vector(-2,2,2));
            }

            {
                var transform = Transformation.Scaling(-1, 1, 1);
                var p = Point(2, 3, 4);
                Assert.IsTrue(transform * p == Point(-2,3,4));
            }

            {
                Assert.IsTrue(Math.RadToDeg(1).Is(57.295779));
                Assert.IsTrue(Math.DegToRad(180).Is(System.Math.PI));
                Assert.IsTrue(Math.Sin(Math.Pi*2).Is(0));
            }

            {
                var p = Point(0, 1, 0);
                var halfQuarter = Transformation.RotationX(Math.Pi / 4.0);
                var fullQuarter = Transformation.RotationX(Math.Pi / 2.0);
                Assert.IsTrue(halfQuarter * p == Point(0, 2.0.Sqrt()/2.0,2.0.Sqrt()/2.0));
                Assert.IsTrue(fullQuarter * p == Point(0, 0,1));
            }

            {
                var p = Point(0, 1, 0);
                var halfQuarter = Transformation.RotationX(Math.Pi / 4.0);
                var inv = halfQuarter.Inverse;
                Assert.IsTrue(inv * p == Point(0, 2.0.Sqrt()/2.0,-2.0.Sqrt()/2.0));
            }

            {
                var p = Point(0, 0, 1);
                var halfQuarter = Transformation.RotationY(Math.Pi / 4.0);
                var fullQuarter = Transformation.RotationY(Math.Pi / 2.0);
                Assert.IsTrue(halfQuarter * p == Point(2.0.Sqrt()/2.0,0,2.0.Sqrt()/2.0));
                Assert.IsTrue(fullQuarter * p == Point(1,0,0));
            }

            {
                var p = Point(0, 1, 0);
                var halfQuarter = Transformation.RotationZ(Math.Pi / 4.0);
                var fullQuarter = Transformation.RotationZ(Math.Pi / 2.0);
                Assert.IsTrue(halfQuarter * p == Point(-2.0.Sqrt()/2.0,2.0.Sqrt()/2.0,0));
                Assert.IsTrue(fullQuarter * p == Point(-1,0,0));
            }

            {
                var transform = Transformation.Shearing(1, 0, 0, 0, 0, 0);
                var p = Point(2, 3, 4);
                Assert.IsTrue(transform * p == Point(5,3,4));
            }
            
            {
                var transform = Transformation.Shearing(0, 1, 0, 0, 0, 0);
                var p = Point(2, 3, 4);
                Assert.IsTrue(transform * p == Point(6,3,4));
            }
            
            {
                var transform = Transformation.Shearing(0, 0, 1, 0, 0, 0);
                var p = Point(2, 3, 4);
                Assert.IsTrue(transform * p == Point(2,5,4));
            }
            
            {
                var transform = Transformation.Shearing(0, 0, 0, 1, 0, 0);
                var p = Point(2, 3, 4);
                Assert.IsTrue(transform * p == Point(2,7,4));
            }
            
            {
                var transform = Transformation.Shearing(0, 0, 0, 0, 1, 0);
                var p = Point(2, 3, 4);
                Assert.IsTrue(transform * p == Point(2,3,6));
            }
            
            {
                var transform = Transformation.Shearing(0, 0, 0, 0, 0, 1);
                var p = Point(2, 3, 4);
                Assert.IsTrue(transform * p == Point(2,3,7));
            }

            {
                var p = Point(1, 0, 1);
                var A = Transformation.RotationX(Math.Pi / 2.0);
                var B = Transformation.Scaling(5, 5, 5);
                var C = Transformation.Translation(10, 5, 7);

                var p2 = A * p;
                Assert.IsTrue(p2 == Point(1,-1,0));

                var p3 = B * p2;
                Assert.IsTrue(p3 == Point(5, -5, 0));

                var p4 = C * p3;
                Assert.IsTrue(p4 == Point(15,0,7));
            }

            {
                var p = Point(1, 0, 1);
                var A = Transformation.RotationX(Math.Pi / 2.0);
                var B = Transformation.Scaling(5, 5, 5);
                var C = Transformation.Translation(10, 5, 7);

                var T = C * B * A;
                Assert.IsTrue(T * p == Point(15, 0, 7));
            }

            {
                var p = Point(1, 0, 1);
                var T = Transformation.Translate(10, 5, 7).Scale(5, 5, 5).Rotate(Math.Pi / 2.0, 0, 0).Build;
                Assert.IsTrue(T * p == Point(15, 0, 7));
            }
        }
    }
}