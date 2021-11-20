using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using NUnit.Framework;
using RayTracing;
using RayTracing.Light;
using RayTracing.Patterns;
using RayTracing.Shapes;
using Tuple = RayTracing.Tuple;
using static RayTracing.Tuple;
using Color = RayTracing.Color;
using Math = RayTracing.Math;
using Matrix4x4 = RayTracing.Matrix4x4;
using Plane = RayTracing.Shapes.Plane;

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

        [Test]
        public void Chapter5()
        {
            {
                var origin = Point(1, 2, 3);
                var direction = Vector(4, 5, 6);

                var r = new Ray(origin, direction);
                Assert.IsTrue(r.Origin == origin);
                Assert.IsTrue(r.Direction == direction);
            }

            {
                var r = new Ray(Point(2, 3, 4), Vector(1, 0, 0));
                Assert.IsTrue(r.Position(0) == Point(2,3,4));
                Assert.IsTrue(r.Position(1) == Point(3,3,4));
                Assert.IsTrue(r.Position(-1) == Point(1,3,4));
                Assert.IsTrue(r.Position(2.5) == Point(4.5,3,4));
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var s = new Sphere();
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 2);
                Assert.IsTrue(xs[0].t.Is(4.0));
                Assert.IsTrue(xs[1].t.Is(6.0));
            }

            {
                var r = new Ray(Point(0, 1, -5), Vector(0, 0, 1));
                var s = new Sphere();
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 2);
                Assert.IsTrue(xs[0].t.Is(5.0));
                Assert.IsTrue(xs[1].t.Is(5.0));
            }

            {
                var r = new Ray(Point(0, 2, -5), Vector(0, 0, 1));
                var s = new Sphere();
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 0);
            }

            {
                var r = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
                var s = new Sphere();
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 2);
                Assert.IsTrue(xs[0].t.Is(-1.0));
                Assert.IsTrue(xs[1].t.Is(1.0));
            }

            {
                var r = new Ray(Point(0, 0, 5), Vector(0, 0, 1));
                var s = new Sphere();
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 2);
                Assert.IsTrue(xs[0].t.Is(-6.0));
                Assert.IsTrue(xs[1].t.Is(-4.0));
            }

            {
                var s = new Sphere();
                var i = new Intersection(3.5, s);
                Assert.IsTrue(i.t.Is(3.5));
                Assert.IsTrue(i.Object == s);
            }

            {
                var s = new Sphere();
                var i1 = new Intersection(1, s);
                var i2 = new Intersection(2, s);
                var xs = Intersection.Combine(i1, i2);
                Assert.IsTrue(xs.Length == 2);
                Assert.IsTrue(xs[0].t.Is(1));
                Assert.IsTrue(xs[1].t.Is(2));
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var s = new Sphere();
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 2);
                Assert.IsTrue(xs[0].Object == s);
                Assert.IsTrue(xs[1].Object == s);
            }

            {
                var s = new Sphere();
                var i1 = new Intersection(1, s);
                var i2 = new Intersection(2, s);
                var xs = Intersection.Combine(i2, i1);
                var i = xs.Hit();
                Assert.IsTrue(i == i1);
            }

            {
                var s = new Sphere();
                var i1 = new Intersection(-1, s);
                var i2 = new Intersection(1, s);
                var xs = Intersection.Combine(i2, i1);
                var i = xs.Hit();
                Assert.IsTrue(i == i2);
            }

            {
                var s = new Sphere();
                var i1 = new Intersection(-2, s);
                var i2 = new Intersection(-1, s);
                var xs = Intersection.Combine(i2, i1);
                var i = xs.Hit();
                Assert.IsTrue(i is null);
            }

            {
                var s = new Sphere();
                var i1 = new Intersection(5, s);
                var i2 = new Intersection(7, s);
                var i3 = new Intersection(-3, s);
                var i4 = new Intersection(2, s);
                var xs = Intersection.Combine(i1, i2, i3, i4);
                var i = xs.Hit();
                Assert.IsTrue(i == i4);
            }

            {
                var r = new Ray(Point(1, 2, 3), Vector(0, 1, 0));
                var m = Transformation.Translation(3, 4, 5);
                var r2 = r.Transform(m);
                Assert.IsTrue(r2.Origin == Point(4,6,8));
                Assert.IsTrue(r2.Direction == Vector(0,1,0));
            }

            {
                var r = new Ray(Point(1, 2, 3), Vector(0, 1, 0));
                var m = Transformation.Scaling(2, 3, 4);
                var r2 = r.Transform(m);
                Assert.IsTrue(r2.Origin == Point(2,6,12));
                Assert.IsTrue(r2.Direction == Vector(0,3,0));
            }

            {
                var s = new Sphere();
                Assert.IsTrue(s.Transform == Matrix4x4.Identity);
            }

            {
                var s = new Sphere();
                var t = Transformation.Translation(2, 3, 4);
                s.Transform = t;
                Assert.IsTrue(s.Transform == t);
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var s = new Sphere
                {
                    Transform = Transformation.Scaling(2, 2, 2)
                };
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 2);
                Assert.IsTrue(xs[0].t.Is(3));
                Assert.IsTrue(xs[1].t.Is(7));
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var s = new Sphere
                {
                    Transform = Transformation.Translation(5, 0, 0)
                };
                var xs = s.Intersect(r);
                Assert.IsTrue(xs.Length == 0);
            }

            {
                var rayOrigin = Point(0, 0, -5);
                var wallZ = 10.0;
                var wallSize = 7.0;
                var canvasPixels = 100;

                var pixelSize = wallSize / canvasPixels;
                var half = wallSize / 2.0;

                var canvas = new Canvas(canvasPixels, canvasPixels);
                var color = new Color(1, 0, 0);

                var shape = new Sphere();

                for (int y = 0; y < canvasPixels; y++)
                {
                    var worldY = half - pixelSize * y;
                    for (int x = 0; x < canvasPixels; x++)
                    {
                        var worldX = -half + pixelSize * x;
                        var position = Point(worldX, worldY, wallZ);
                        var r = new Ray(rayOrigin, (position - rayOrigin).Normalised);
                        var xs = shape.Intersect(r);
                        if (xs.Hit() is not null)
                            canvas[x, y] = color;
                    }
                }
                
                canvas.Save("img/Chapter5/img.png");
            }
        }

        [Test]
        public void Chapter6()
        {
            {
                var s = new Sphere();
                var n = s.NormalAt(Point(1, 0, 0));
                Assert.IsTrue(n == Vector(1,0,0));
            }

            {
                var s = new Sphere();
                var n = s.NormalAt(Point(0, 1, 0));
                Assert.IsTrue(n == Vector(0, 1, 0));
            }

            {
                var s = new Sphere();
                var n = s.NormalAt(Point(0, 0, 1));
                Assert.IsTrue(n == Vector(0, 0, 1));
            }

            {
                var s = new Sphere();
                var n = s.NormalAt(Point(3.0.Sqrt() / 3.0, 3.0.Sqrt() / 3.0, 3.0.Sqrt() / 3.0));
                Assert.IsTrue(n == Vector(3.0.Sqrt() / 3.0, 3.0.Sqrt() / 3.0, 3.0.Sqrt() / 3.0));
            }

            {
                var s = new Sphere();
                var n = s.NormalAt(Point(3.0.Sqrt() / 3.0, 3.0.Sqrt() / 3.0, 3.0.Sqrt() / 3.0));
                Assert.IsTrue(n == n.Normalised);
            }

            {
                var s = new Sphere
                {
                    Transform = Transformation.Translation(0, 1, 0)
                };
                var n = s.NormalAt(Point(0, 1.70711, -0.70711));
                Assert.IsTrue(n == Vector(0,0.70711, -0.70711));
            }

            {
                var s = new Sphere
                {
                    Transform = Transformation.Scaling(1, 0.5, 1) * Transformation.RotationZ(Math.Pi / 5.0)
                };
                var n = s.NormalAt(Point(0, 2.0.Sqrt() / 2.0, -2.0.Sqrt() / 2.0));
                Assert.IsTrue(n == Vector(0, 0.97014, -0.24254));
            }

            {
                var v = Vector(1, -1, 0);
                var n = Vector(0, 1, 0);
                var r = v.Reflect(n);
                Assert.IsTrue(r == Vector(1,1,0));
            }

            {
                var v = Vector(0, -1, 0);
                var n = Vector(2.0.Sqrt() / 2.0, 2.0.Sqrt() / 2.0, 0);
                var r = v.Reflect(n);
                Assert.IsTrue(r == Vector(1,0,0));
            }

            {
                var intensity = new Color(1, 1, 1);
                var position = Point(0, 0, 0);
                var light = new PointLight(position, intensity);
                Assert.IsTrue(light.Position == position);
                Assert.IsTrue(light.Intensity == intensity);
            }

            {
                var m = new Material();
                Assert.IsTrue(m.Color == new Color(1, 1, 1));
                Assert.IsTrue(m.Ambient.Is(0.1));
                Assert.IsTrue(m.Diffuse.Is(0.9));
                Assert.IsTrue(m.Specular.Is(0.9));
                Assert.IsTrue(m.Shininess.Is(200.0));
            }

            {
                var s = new Sphere();
                var m = s.Material;
                Assert.IsTrue(m == new Material());
            }

            {
                var s = new Sphere();
                var m = new Material
                {
                    Ambient = 1
                };
                s.Material = m;
                Assert.IsTrue(s.Material == m);
            }

            {
                var m = new Material();
                var position = Point(0, 0, 0);
                var obj = new Sphere();

                {
                    var eyev = Vector(0, 0, -1);
                    var normalv = Vector(0, 0, -1);
                    var light = new PointLight(Point(0, 0, -10), new Color(1, 1, 1));
                    var result = m.Lighting(obj, light, position, eyev, normalv);
                    Assert.IsTrue(result == new Color(1.9, 1.9, 1.9));
                }
                
                {
                    var eyev = Vector(0, 2.0.Sqrt()/2.0, -2.0.Sqrt()/2.0);
                    var normalv = Vector(0, 0, -1);
                    var light = new PointLight(Point(0, 0, -10), new Color(1, 1, 1));
                    var result = m.Lighting(obj, light, position, eyev, normalv);
                    Assert.IsTrue(result == new Color(1.0, 1.0, 1.0));
                }
                
                {
                    var eyev = Vector(0, 0, -1);
                    var normalv = Vector(0, 0, -1);
                    var light = new PointLight(Point(0, 10, -10), new Color(1, 1, 1));
                    var result = m.Lighting(obj, light, position, eyev, normalv);
                    Assert.IsTrue(result == new Color(0.7364, 0.7364, 0.7364));
                }
                
                {
                    var eyev = Vector(0, -2.0.Sqrt()/2.0, -2.0.Sqrt()/2.0);
                    var normalv = Vector(0, 0, -1);
                    var light = new PointLight(Point(0, 10, -10), new Color(1, 1, 1));
                    var result = m.Lighting(obj, light, position, eyev, normalv);
                    Assert.IsTrue(result == new Color(1.6364, 1.6364, 1.6364));
                }
                
                {
                    var eyev = Vector(0, 0, -1);
                    var normalv = Vector(0, 0, -1);
                    var light = new PointLight(Point(0, 0, 10), new Color(1, 1, 1));
                    var result = m.Lighting(obj, light, position, eyev, normalv);
                    Assert.IsTrue(result == new Color(0.1, 0.1, 0.1));
                }
                
            }
            
            {
                var rayOrigin = Point(0, 0, -5);
                var wallZ = 10.0;
                var wallSize = 7.0;
                var canvasPixels = 100;

                var pixelSize = wallSize / canvasPixels;
                var half = wallSize / 2.0;

                var canvas = new Canvas(canvasPixels, canvasPixels);
                var color = new Color(1, 0, 0);

                var shape = new Sphere
                {
                    Material = new Material
                    {
                        Color = new Color(1, 0.2, 1)
                    }
                };
                var light = new PointLight
                {
                    Position = Point(-10, 10, -10),
                    Intensity = new Color(1, 1, 1)
                };

                for (int y = 0; y < canvasPixels; y++)
                {
                    var worldY = half - pixelSize * y;
                    for (int x = 0; x < canvasPixels; x++)
                    {
                        var worldX = -half + pixelSize * x;
                        var position = Point(worldX, worldY, wallZ);
                        var r = new Ray(rayOrigin, (position - rayOrigin).Normalised);
                        var xs = shape.Intersect(r);
                        var hit = xs.Hit();
                        if (hit is not null)
                        {
                            var point = r.Position(hit.t);
                            var normal = hit.Object.NormalAt(point);
                            var eye = -r.Direction;
                            canvas[x, y] = hit.Object.Material.Lighting(shape, light, point, eye, normal);
                        }
                    }
                }
                
                canvas.Save("img/Chapter6/imgt.png");
            }
        }

        [Test]
        public void Chapter7()
        {
            {
                var w = new World();
                Assert.IsTrue(w.Objects.Count == 0);
                Assert.IsTrue(w.Light is null);
            }

            {
                var light = new PointLight(Point(-10, 10, -10), new Color(1, 1, 1));
                var s1 = new Sphere
                {
                    Material = new Material
                    {
                        Color = new Color(0.8, 1.0, 0.6),
                        Diffuse = 0.7,
                        Specular = 0.2
                    }
                };
                var s2 = new Sphere
                {
                    Transform = Transformation.Scaling(0.5, 0.5, 0.5)
                };
                var w = World.Default;
                Assert.IsTrue(w.Light == light);
                Assert.IsTrue(w.Contains(s1));
                Assert.IsTrue(w.Contains(s2));
            }

            {
                var w = World.Default;
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var xs = w.IntersectWorld(r);
                Assert.IsTrue(xs.Length == 4);
                Assert.IsTrue(xs[0].t.Is(4));
                Assert.IsTrue(xs[1].t.Is(4.5));
                Assert.IsTrue(xs[2].t.Is(5.5));
                Assert.IsTrue(xs[3].t.Is(6));
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var shape = new Sphere();
                var i = new Intersection(4, shape);
                var comps = i.PrepareComputations(r);
                Assert.IsTrue(comps.t.Is(i.t));
                Assert.IsTrue(comps.Object == i.Object);
                Assert.IsTrue(comps.Point == Point(0,0,-1));
                Assert.IsTrue(comps.EyeV == Vector(0,0,-1));
                Assert.IsTrue(comps.NormalV == Vector(0,0,-1));
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                Sphere shape = new Sphere();
                var i = new Intersection(4, shape);
                var comps = i.PrepareComputations(r);
                Assert.IsFalse(comps.Inside);
            }

            {
                var r = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
                var shape = new Sphere();
                var i = new Intersection(1, shape);
                var comps = i.PrepareComputations(r);
                Assert.IsTrue(comps.Point == Point(0,0,1));
                Assert.IsTrue(comps.EyeV == Vector(0,0,-1));
                Assert.IsTrue(comps.Inside);
                Assert.IsTrue(comps.NormalV == Vector(0,0,-1));
            }

            {
                var w = World.Default;
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var shape = w.Objects[0];
                var i = new Intersection(4, shape);
                var comps = i.PrepareComputations(r);
                var c = w.ShadeHit(comps);
                Assert.IsTrue(c == new Color(0.38066, 0.47583, 0.2855));
            }

            {
                var w = World.Default;
                w.Light = new PointLight(Point(0, 0.25, 0), new Color(1, 1, 1));
                var r = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
                var shape = w.Objects[1];
                var i = new Intersection(0.5, shape);
                var comps = i.PrepareComputations(r);
                var c = w.ShadeHit(comps);
                Assert.IsTrue(c == new Color(0.90498, 0.90498, 0.90498));
            }

            {
                var w = World.Default;
                var r = new Ray(Point(0, 0, -5), Vector(0, 1, 0));
                var c = w.ColorAt(r);
                Assert.IsTrue(c == new Color(0,0,0));
            }
            
            {
                var w = World.Default;
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var c = w.ColorAt(r);
                Assert.IsTrue(c == new Color(0.38066,0.47583,0.2855));
            }

            {
                var w = World.Default;
                var outer = w.Objects[0];
                outer.Material.Ambient = 1;
                var inner = w.Objects[1];
                inner.Material.Ambient = 1;
                var r = new Ray(Point(0, 0, 0.75), Vector(0, 0, -1));
                var c = w.ColorAt(r);
                Assert.IsTrue(c == inner.Material.Color);
            }

            {
                var from = Point(0, 0, 0);
                var to = Point(0, 0, -1);
                var up = Vector(0, 1, 0);
                var t = Transformation.View(from, to, up);
                Assert.IsTrue(t == Matrix4x4.Identity);
            }

            {
                var from = Point(0, 0, 0);
                var to = Point(0, 0, 1);
                var up = Vector(0, 1, 0);
                var t = Transformation.View(from, to, up);
                Assert.IsTrue(t == Transformation.Scaling(-1,1,-1));
            }

            {
                var from = Point(0, 0, 8);
                var to = Point(0, 0, 0);
                var up = Vector(0, 1, 0);
                var t = Transformation.View(from, to, up);
                Assert.IsTrue(t == Transformation.Translation(0, 0, -8));
            }

            {
                var from = Point(1, 3, 2);
                var to = Point(4, -2, 8);
                var up = Vector(1, 1, 0);
                var t = Transformation.View(from, to, up);
                Assert.IsTrue(t == new Matrix4x4
                {
                    {
                        -0.50709, 0.50709, 0.67612, -2.36643,
                        0.76772, 0.60609, 0.12122, -2.82843,
                        -0.35857, 0.59761, -0.71714, 0.00000,
                        0.0, 0.0, 0.0, 1.0
                    }
                });
            }

            {
                var hSize = 160;
                var vSize = 120;
                var fieldOfView = System.Math.PI/2.0;
                var c = new Camera(hSize, vSize, fieldOfView);
                Assert.IsTrue(c.HSize == 160);
                Assert.IsTrue(c.VSize == 120);
                Assert.IsTrue(c.FieldOfView.Is(Math.Pi/2.0));
                Assert.IsTrue(c.Transform == Matrix4x4.Identity);
            }

            {
                var c = new Camera(200, 125, Math.Pi/2.0);
                Assert.IsTrue(c.PixelSize.Is(0.01));
            }

            {
                var c = new Camera(125, 200, Math.Pi / 2.0);
                Assert.IsTrue(c.PixelSize.Is(0.01));
            }

            {
                var c = new Camera(201, 101, Math.Pi / 2.0);
                var r = c.RayForPixel(100, 50);
                Assert.IsTrue(r.Origin == Point(0,0,0));
                Assert.IsTrue(r.Direction == Vector(0,0,-1));
            }

            {
                var c = new Camera(201, 101, Math.Pi / 2.0);
                var r = c.RayForPixel(0, 0);
                Assert.IsTrue(r.Origin == Point(0,0,0));
                Assert.IsTrue(r.Direction == Vector(0.66519,0.33259,-0.66851));
            }

            {
                var c = new Camera(201, 101, Math.Pi / 2.0);
                c.Transform = Transformation.RotationY(Math.Pi / 4.0) * Transformation.Translation(0, -2, 5);
                var r = c.RayForPixel(100, 50);
                Assert.IsTrue(r.Origin == Point(0,2,-5));
                Assert.IsTrue(r.Direction == Vector(2.0.Sqrt()/2.0,0,-2.0.Sqrt()/2.0));
            }

            {
                var w = World.Default;
                var c = new Camera(11, 11, System.Math.PI / 2.0);
                var from = Point(0, 0, -5);
                var to = Point(0, 0, 0);
                var up = Vector(0, 1, 0);
                c.Transform = Transformation.View(from, to, up);
                var image = c.Render(w);
                Assert.IsTrue(image[5,5] == new Color(0.38066, 0.47583, 0.2855));
                image.Save("img/Chapter7/1.png");
            }

            {
                var floor = new Sphere
                {
                    Transform = Transformation.Scaling(10, 0.01, 10),
                    Material = new Material
                    {
                        Color = new Color(1, 0.9, 0.9),
                        Specular = 0
                    }
                };

                var leftWall = new Sphere
                {
                    Transform = Transformation.Translation(0, 0, 5) * Transformation.RotationY(-Math.Pi / 4.0) *
                                Transformation.RotationX(Math.Pi / 2.0) * Transformation.Scaling(10, 0.01, 10), //NOTE: If not working Check Order on page 195
                    Material = floor.Material
                };

                var rightWall = new Sphere
                {
                    Transform = Transformation.Translation(0, 0, 5) * Transformation.RotationY(Math.Pi / 4.0) *
                                Transformation.RotationX(Math.Pi / 2.0) * Transformation.Scaling(10, 0.01, 10), //NOTE: If not working Check Order on page 195
                    Material = floor.Material
                };

                var middle = new Sphere
                {
                    Transform = Transformation.Translation(-0.5, 1, 0.5),
                    Material = new Material
                    {
                        Color = new Color(0.1, 1, 0.5),
                        Diffuse = 0.7,
                        Specular = 0.3
                    }
                };

                var right = new Sphere
                {
                    Transform = Transformation.Translate(1.5, 0.5, -0.5).Scale(0.5, 0.5, 0.5).Build,
                    Material = new Material
                    {
                        Color = new Color(0.5, 1, 0.1),
                        Diffuse = 0.7,
                        Specular = 0.3
                    }
                };

                var left = new Sphere
                {
                    Transform = Transformation.Translate(-1.5, 0.33, -0.75).Scale(0.33, 0.33, 0.33).Build,
                    Material = new Material
                    {
                        Color = new Color(1, 0.8, 0.1),
                        Diffuse = 0.7,
                        Specular = 0.3
                    }
                };

                var world = new World();
                world.Objects.Add(floor);
                world.Objects.Add(leftWall);
                world.Objects.Add(rightWall);
                world.Objects.Add(middle);
                world.Objects.Add(left);
                world.Objects.Add(right);

                world.Light = new PointLight(Point(-10, 10, -10), new Color(1, 1, 1));
                var camera = new Camera(100, 50, Math.Pi / 3.0);
                camera.Transform = Transformation.View(Point(0, 1.5, -5), Point(0, 1, 0), Vector(0, 1, 0));

                var canvas = camera.Render(world);
                canvas.Save("img/Chapter7/2.png");
            }
        }

        [Test]
        public void Chapter8()
        {
            var m = new Material();
            var position = Point(0, 0, 0);

            {
                var obj = new Sphere();
                var eyeV = Vector(0, 0, -1);
                var normalV = Vector(0, 0, -1);
                var light = new PointLight(Point(0, 0, -10), new Color(1, 1, 1));
                var inShadow = true;
                var result = m.Lighting(obj, light, position, eyeV, normalV, inShadow);
                Assert.IsTrue(result == new Color(0.1, 0.1, 0.1));
            }

            {
                var w = World.Default;
                var p = Point(0, 10, 0);
                Assert.IsFalse(w.IsShadowed(p));
            }

            {
                var w = World.Default;
                var p = Point(10, -10, 10);
                Assert.IsTrue(w.IsShadowed(p));
            }

            {
                var w = World.Default;
                var p = Point(-20, 20, -20);
                Assert.IsFalse(w.IsShadowed(p));
            }

            {
                var w = World.Default;
                var p = Point(-2, 2, -2);
                Assert.IsFalse(w.IsShadowed(p));
            }

            {
                var w = new World();
                w.Light = new PointLight(Point(0, 0, -10), new Color(1, 1, 1));
                var s1 = new Sphere();
                w.Objects.Add(s1);
                var s2 = new Sphere
                {
                    Transform = Transformation.Translation(0, 0, 10)
                };
                w.Objects.Add(s2);

                var r = new Ray(Point(0, 0, 5), Vector(0, 0, 1));
                var i = new Intersection(4, s2);

                var comps = i.PrepareComputations(r);
                var c = w.ShadeHit(comps);
                Assert.IsTrue(c == new Color(0.1,0.1,0.1));
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var shape = new Sphere
                {
                    Transform = Transformation.Translation(0, 0, 1)
                };
                var i = new Intersection(5, shape);
                var comps = i.PrepareComputations(r);
                Assert.IsTrue(comps.OverPoint.Z < -Constant.Epsilon/2.0);
                Assert.IsTrue(comps.Point.Z > comps.OverPoint.Z);
            }
        }

        [Test]
        public void Chapter9()
        {
            {
                var s = new TestShape();
                Assert.IsTrue(s.Transform == Matrix4x4.Identity);
            }

            {
                var s = new TestShape
                {
                    Transform = Transformation.Translation(2, 3, 4)
                };
                Assert.IsTrue(s.Transform == Transformation.Translation(2,3,4));
            }

            {
                var s = new TestShape();
                var m = s.Material;
                Assert.IsTrue(m == new Material());
            }

            {
                var s = new TestShape();
                var m = new Material
                {
                    Ambient = 1
                };
                s.Material = m;
                Assert.IsTrue(s.Material == m);
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var s = new TestShape
                {
                    Transform = Transformation.Scaling(2, 2, 2)
                };
                var xs = s.Intersect(r);
                Assert.IsTrue(s.SavedRay.Origin == Point(0,0,-2.5));
                Assert.IsTrue(s.SavedRay.Direction == Vector(0,0,0.5));
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var s = new TestShape
                {
                    Transform = Transformation.Translation(5, 0, 0)
                };
                var xs = s.Intersect(r);
                Assert.IsTrue(s.SavedRay.Origin == Point(-5,0,-5));
                Assert.IsTrue(s.SavedRay.Direction == Vector(0,0,1));
            }

            {
                var s = new TestShape();
                s.Transform = Transformation.Translation(0, 1, 0);
                var n = s.NormalAt(Point(0, 1.70711, -0.70711));
                Assert.IsTrue(n == Vector(0,0.70711, -0.70711));
            }

            {
                var s = new TestShape();
                var m = Transformation.Scaling(1, 0.5, 1) * Transformation.RotationZ(Math.Pi / 5.0);
                s.Transform = m;
                var n = s.NormalAt(Point(0, 2.0.Sqrt() / 2.0, -2.0.Sqrt() / 2.0));
                Assert.IsTrue(n == Vector(0, 0.97014, -0.24254));
            }

            {
                var p = new Plane();
                var n1 = p.NormalAt(Point(0, 0, 0));
                var n2 = p.NormalAt(Point(10, 0, -10));
                var n3 = p.NormalAt(Point(-5, 0, 150));
                
                Assert.IsTrue(n1 == Vector(0,1,0));
                Assert.IsTrue(n2 == Vector(0,1,0));
                Assert.IsTrue(n3 == Vector(0,1,0));
            }

            {
                var p = new Plane();
                var r = new Ray(Point(0, 10, 0), Vector(0, 0, 1));
                var xs = p.Intersect(r);
                Assert.IsTrue(xs.Length == 0);
            }

            {
                var p = new Plane();
                var r = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
                var xs = p.Intersect(r);
                Assert.IsTrue(xs.Length == 0);
            }

            {
                var p = new Plane();
                var r = new Ray(Point(0, 1, 0), Vector(0, -1, 0));
                var xs = p.Intersect(r);
                Assert.IsTrue(xs.Length == 1);
                Assert.IsTrue(xs[0].t.Is(1));
                Assert.IsTrue(xs[0].Object == p);
            }

            {
                var p = new Plane();
                var r = new Ray(Point(0, -1, 0), Vector(0, 1, 0));
                var xs = p.Intersect(r);
                Assert.IsTrue(xs.Length == 1);
                Assert.IsTrue(xs[0].t.Is(1));
                Assert.IsTrue(xs[0].Object == p);
            }

            {
                var c = new Color("#ffffff");
                Assert.IsTrue(c.Red.Is(1));
                Assert.IsTrue(c.Green.Is(1));
                Assert.IsTrue(c.Blue.Is(1));
            }

            {
                var floor = new Plane();
                floor.Material.Color = new Color(0.4, 0.4, 0.3);

                var s1 = new Sphere
                {
                    Material = new Material
                    {
                        Color = new Color("#0C9E4E")
                    },
                    Transform = Transformation.Translate(0, 1, 0).Scale(2, 2, 2).Build
                };

                var s2 = new Sphere
                {
                    Material = new Material
                    {
                        Color = new Color("#51A40B")
                    },
                    Transform = Transformation.Translate(5, 0.5, -1).Build
                };

                var s3 = new Sphere
                {
                    Material = new Material
                    {
                        Color = new Color("#9B7D0D")
                    },
                    Transform = Transformation.Translate(-4, 0.25, -1).Scale(0.5, 0.5, 0.5).Build
                };

                var w = new World();
                w.Objects.Add(floor);
                w.Objects.Add(s1);
                w.Objects.Add(s2);
                w.Objects.Add(s3);
                w.Light = new PointLight(Point(-10, 10, -10), new Color(1,1,1));

                var c = new Camera(200, 150, Math.Pi / 3.0);
                c.Transform = Transformation.View(Point(0, 5, -12), Point(2, 2, 0), Vector(0, 1, 0));
                var image = c.Render(w);
                image.Save("img/Chapter9/1.png");
            }
        }

        [Test]
        public void Chapter10()
        {
            {
                var pattern = new StripePattern(Color.White, Color.Black);

                Assert.IsTrue(pattern.A == Color.White);
                Assert.IsTrue(pattern.B == Color.Black);
            }

            {
                var pattern = new StripePattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0,1,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0,2,0)) == Color.White);
            }

            {
                var pattern = new StripePattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,1)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,2)) == Color.White);
            }

            {
                var pattern = new StripePattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0.9,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(1,0,0)) == Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(-0.1,0,0)) == Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(-1.1,0,0)) == Color.White);
            }

            {
                var obj = new Sphere();
                var m = new Material();
                m.Pattern = new StripePattern(Color.White, Color.Black);
                m.Ambient = 1;
                m.Diffuse = 0;
                m.Specular = 0;
                var eyeV = Vector(0, 0, -1);
                var normalV = Vector(0, 0, -1);
                var light = new PointLight(Point(0, 0, -10), new Color(1, 1, 1));
                var c1 = m.Lighting(obj, light, Point(0.9, 0, 0), eyeV, normalV, false);
                var c2 = m.Lighting(obj, light, Point(1.1, 0, 0), eyeV, normalV, false);
                Assert.IsTrue(c1 == Color.White);
                Assert.IsTrue(c2 == Color.Black);
            }

            {
                var obj = new Sphere();
                obj.Transform = Transformation.Scaling(2, 2, 2);
                var pattern = new StripePattern(Color.White, Color.Black);
                var c = pattern.PatternAtObject(obj, Point(1.5, 0, 0));
                Assert.IsTrue(c == Color.White);
            }

            {
                var obj = new Sphere();
                var pattern = new StripePattern(Color.White, Color.Black);
                pattern.Transform = Transformation.Scaling(2, 2, 2);
                var c = pattern.PatternAtObject(obj, Point(1.5, 0, 0));
                Assert.IsTrue(c == Color.White);
            }

            {
                var obj = new Sphere();
                obj.Transform = Transformation.Scaling(2, 2, 2);
                var pattern = new StripePattern(Color.White, Color.Black);
                pattern.Transform = Transformation.Translation(0.5, 0, 0);
                var c = pattern.PatternAtObject(obj, Point(2.5, 0, 0));
                Assert.IsTrue(c == Color.White);
            }

            {
                var pattern = new TestPattern();
                Assert.IsTrue(pattern.Transform == Matrix4x4.Identity);
            }

            {
                var pattern = new TestPattern();
                pattern.Transform = Transformation.Translation(1, 2, 3);
                Assert.IsTrue(pattern.Transform == Transformation.Translation(1,2,3));
            }

            {
                var shape = new Sphere();
                shape.Transform = Transformation.Scaling(2, 2, 2);
                var pattern = new TestPattern();
                var c = pattern.PatternAtObject(shape, Point(2, 3, 4));
                Assert.IsTrue(c == new Color(1,1.5,2));
            }

            {
                var shape = new Sphere();
                var pattern = new TestPattern();
                pattern.Transform = Transformation.Scaling(2, 2, 2);
                var c = pattern.PatternAtObject(shape, Point(2, 3, 4));
                Assert.IsTrue(c == new Color(1,1.5,2));
            }

            {
                var shape = new Sphere();
                shape.Transform = Transformation.Scaling(2, 2, 2);
                var pattern = new TestPattern();
                pattern.Transform = Transformation.Translation(0.5, 1, 1.5);
                var c = pattern.PatternAtObject(shape, Point(2.5, 3, 3.5));
                Assert.IsTrue(c == new Color(0.75, 0.5, 0.25));
            }

            {
                var pattern = new GradientPattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0.25,0,0)) == new Color(0.75,0.75,0.75));
                Assert.IsTrue(pattern.PatternAt(Point(0.5,0,0)) == new Color(0.5,0.5,0.5));
                Assert.IsTrue(pattern.PatternAt(Point(0.75,0,0)) == new Color(0.25,0.25,0.25));
            }

            {
                var pattern = new RingPattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(1,0,0)) == Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,1)) == Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0.708,0, 0.708)) == Color.Black);
            }

            {
                var pattern = new CheckerPattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0.99,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(1.01,0,0)) == Color.Black);
            }
            
            {
                var pattern = new CheckerPattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0, 0.99,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0, 1.01,0)) == Color.Black);
            }
            
            {
                var pattern = new CheckerPattern(Color.White, Color.Black);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,0.99)) == Color.White);
                Assert.IsTrue(pattern.PatternAt(Point(0,0,1.01)) == Color.Black);
            }
        }

        [Test]
        public void Chapter11()
        {
            {
                var m = new Material();
                Assert.IsTrue(m.Reflective.Is(0));
            }
            
            {
                var shape = new Plane();
                var r = new Ray(Point(0, 1, -1), Vector(0, -2.0.Sqrt() / 2.0, 2.0.Sqrt() / 2.0));
                var i = new Intersection(2.0.Sqrt(), shape);
                var comps = i.PrepareComputations(r);
                Assert.IsTrue(comps.ReflectV == Vector(0, 2.0.Sqrt()/2.0, 2.0.Sqrt()/2.0));
            }

            {
                var w = World.Default;
                var r = new Ray(Point(0, 0, 0), Vector(0, 0, 1));
                var shape = w.Objects[1];
                shape.Material.Ambient = 1;
                var i = new Intersection(1, shape);
                var comps = i.PrepareComputations(r);
                var color = w.ReflectedColor(comps, 4);
                Assert.IsTrue(color == new Color(0,0,0));
            }

            {
                var w = World.Default;
                var shape = new Plane
                {
                    Material = new Material
                    {
                        Reflective = 0.5
                    },
                    Transform = Transformation.Translation(0, -1, 0)
                };
                w.Objects.Add(shape);
                var r = new Ray(Point(0, 0, -3), Vector(0, -2.0.Sqrt()/2.0, 2.0.Sqrt()/2.0));
                var i = new Intersection(2.0.Sqrt(), shape);
                var comps = i.PrepareComputations(r);
                var color = w.ReflectedColor(comps, 4);
                Assert.IsTrue(color == new Color(0.190332, 0.237915, 0.1427492)); // Modified values
            }

            {
                var w = World.Default;
                var shape = new Plane
                {
                    Material = new Material
                    {
                        Reflective = 0.5
                    },
                    Transform = Transformation.Translation(0, -1, 0)
                };
                w.Objects.Add(shape);
                var r = new Ray(Point(0, 0, -3), Vector(0, -2.0.Sqrt() / 2.0, 2.0.Sqrt() / 2.0));
                var i = new Intersection(2.0.Sqrt(), shape);
                var comps = i.PrepareComputations(r);
                var color = w.ShadeHit(comps);
                Assert.IsTrue(color == new Color(0.8767577, 0.9243407, 0.8291746));
            }

            {
                var w = new World();
                w.Light = new PointLight(Point(0, 0, 0), new Color(1,1,1));
                var lower = new Plane
                {
                    Material = new Material
                    {
                        Reflective = 1
                    },
                    Transform = Transformation.Translation(0, -1, 0)
                };
                w.Objects.Add(lower);

                var upper = new Plane
                {
                    Material = new Material
                    {
                        Reflective = 1
                    },
                    Transform = Transformation.Translation(0, 1, 0)
                };
                w.Objects.Add(upper);

                var r = new Ray(Point(0, 0, 0), Vector(0, 1, 0));
                Assert.DoesNotThrow(() => w.ColorAt(r));
            }

            {
                var w = World.Default;
                var shape = new Plane
                {
                    Material = new Material
                    {
                        Reflective = 0.5
                    },
                    Transform = Transformation.Translation(0, -1, 0)
                };
                w.Objects.Add(shape);

                var r = new Ray(Point(0, 0, -3), Vector(0, -2.0.Sqrt() / 2.0, 2.0.Sqrt() / 2.0));
                var i = new Intersection(2.0.Sqrt(), shape);
                var comps = i.PrepareComputations(r);
                var color = w.ReflectedColor(comps, 0);
                Assert.IsTrue(color == Color.Black);
            }

            {
                var m = new Material();
                Assert.IsTrue(m.Transparency.Is(0));
                Assert.IsTrue(m.RefractiveIndex.Is(1));
            }

            {
                var s = Sphere.GlassSphere;
                Assert.IsTrue(s.Transform == Matrix4x4.Identity);
                Assert.IsTrue(s.Material.Transparency.Is(1));
                Assert.IsTrue(s.Material.RefractiveIndex.Is(1.5));
            }

            {
                var A = Sphere.GlassSphere;
                A.Transform = Transformation.Scaling(2, 2, 2);
                A.Material.RefractiveIndex = 1.5;
                
                var B = Sphere.GlassSphere;
                B.Transform = Transformation.Translation(0, 0, -0.25);
                B.Material.RefractiveIndex = 2;
                
                var C = Sphere.GlassSphere;
                B.Transform = Transformation.Translation(0, 0, 0.25);
                C.Material.RefractiveIndex = 2.5;

                var r = new Ray(Point(0, 0, -4), Vector(0, 0, 1));
                var xs = Intersection.Combine(new Intersection(2, A), new Intersection(2.75, B),
                    new Intersection(3.25, C), new Intersection(4.75, B), new Intersection(5.25, C),
                    new Intersection(6, A));

                {
                    var comps = xs[0].PrepareComputations(r, xs);
                    Assert.IsTrue(comps.N1.Is(1.0));
                    Assert.IsTrue(comps.N2.Is(1.5));
                }
                
                {
                    var comps = xs[1].PrepareComputations(r, xs);
                    Assert.IsTrue(comps.N1.Is(1.5));
                    Assert.IsTrue(comps.N2.Is(2.0));
                }
                
                {
                    var comps = xs[2].PrepareComputations(r, xs);
                    Assert.IsTrue(comps.N1.Is(2.0));
                    Assert.IsTrue(comps.N2.Is(2.5));
                }
                
                {
                    var comps = xs[3].PrepareComputations(r, xs);
                    Assert.IsTrue(comps.N1.Is(2.5));
                    Assert.IsTrue(comps.N2.Is(2.5));
                }
                
                {
                    var comps = xs[4].PrepareComputations(r, xs);
                    Assert.IsTrue(comps.N1.Is(2.5));
                    Assert.IsTrue(comps.N2.Is(1.5));
                }
                
                {
                    var comps = xs[5].PrepareComputations(r, xs);
                    Assert.IsTrue(comps.N1.Is(1.5));
                    Assert.IsTrue(comps.N2.Is(1.0));
                }
            }

            {
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var shape = Sphere.GlassSphere;
                shape.Transform = Transformation.Translation(0, 0, 1);
                var i = new Intersection(5, shape);
                var xs = Intersection.Combine(i);
                var comps = i.PrepareComputations(r, xs);
                Assert.IsTrue(comps.UnderPoint.Z > Constant.Epsilon/2.0);
                Assert.IsTrue(comps.Point.Z < comps.UnderPoint.Z);
            }

            {
                var w = World.Default;
                var shape = w.Objects[0];
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var xs = Intersection.Combine(new Intersection(4, shape), new Intersection(6, shape));
                var comps = xs[0].PrepareComputations(r, xs);
                var c = w.RefractedColor(comps, Constant.MaxRecursionDepth);
                Assert.IsTrue(c == new Color(0,0,0));
            }
            
            {
                var w = World.Default;
                var shape = w.Objects[0];
                shape.Material.Transparency = 1.0;
                shape.Material.RefractiveIndex = 1.5;
                var r = new Ray(Point(0, 0, -5), Vector(0, 0, 1));
                var xs = Intersection.Combine(new Intersection(4, shape), new Intersection(6, shape));
                var comps = xs[0].PrepareComputations(r, xs);
                var c = w.RefractedColor(comps, 0);
                Assert.IsTrue(c == new Color(0,0,0));
            }
            
            {
                var w = World.Default;
                var shape = w.Objects[0];
                shape.Material.Transparency = 1.0;
                shape.Material.RefractiveIndex = 1.5;
                var r = new Ray(Point(0, 0, 2.0.Sqrt()/2.0), Vector(0, 1, 0));
                var xs = Intersection.Combine(new Intersection(-2.0.Sqrt()/2.0, shape), new Intersection(2.0.Sqrt()/2.0, shape));
                var comps = xs[1].PrepareComputations(r, xs);
                var c = w.RefractedColor(comps, Constant.MaxRecursionDepth);
                Assert.IsTrue(c == new Color(0,0,0));
            }

            {
                var w = World.Default;
                var A = w.Objects[0];
                A.Material.Ambient = 1.0;
                A.Material.Pattern = new TestPattern();

                var B = w.Objects[1];
                B.Material.Transparency = 1.0;
                B.Material.RefractiveIndex = 1.5;

                var r = new Ray(Point(0, 0, 0.1), Vector(0, 1, 0));
                var xs = Intersection.Combine(new Intersection(-0.9899, A), new Intersection(-0.4899, B),
                    new Intersection(0.4899, B), new Intersection(0.9899, A));
                var comps = xs[2].PrepareComputations(r, xs);
                var c = w.RefractedColor(comps, Constant.MaxRecursionDepth);
                Assert.IsTrue(c == new Color(0, 0.99887, 0.04721)); // Values modified original page 277
            }

            {
                var w = World.Default;
                var floor = new Plane
                {
                    Transform = Transformation.Translation(0, -1, 0),
                    Material = new Material
                    {
                        Transparency = 0.5,
                        RefractiveIndex = 1.5
                    }
                };
                w.Objects.Add(floor);

                var ball = new Sphere
                {
                    Material = new Material
                    {
                        Color = new Color(1, 0, 0),
                        Ambient = 0.5
                    },
                    Transform = Transformation.Translation(0, -3.5, -0.5)
                };
                w.Objects.Add(ball);

                var r = new Ray(Point(0, 0, -3), Vector(0, -2.0.Sqrt() / 2.0, 2.0.Sqrt() / 2.0));
                var xs = Intersection.Combine(new Intersection(2.0.Sqrt(), floor));
                var comps = xs[0].PrepareComputations(r, xs);
                var color = w.ShadeHit(comps);
                Assert.IsTrue(color == new Color(0.93642, 0.68642, 0.68642));
            }
        }
    }
}