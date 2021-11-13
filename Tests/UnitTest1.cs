using System;
using NUnit.Framework;
using RayTracing;
using Tuple = RayTracing.Tuple;
using static RayTracing.Tuple;

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
    }
}