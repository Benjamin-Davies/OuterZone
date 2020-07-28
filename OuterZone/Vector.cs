using System;
using System.Drawing;

namespace OuterZone
{
    public struct Vector
    {
        public double X;
        public double Y;

        public double SizeSq => Dot(this, this);
        public double Size => Math.Sqrt(SizeSq);

        public static Vector Zero => (0, 0);
        public static Vector Up => (0, -1);
        public static Vector Down => (0, 1);
        public static Vector Left => (-1, 0);
        public static Vector Right => (1, 0);
        public static double Small => 0.1; // 10 cm or 10 cm/s

        public static double Dot(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;

        public static Vector operator *(double x, Vector v) => (x * v.X, x * v.Y); 
        public static Vector operator *(Vector a, Vector b) => (a.X * b.X, a.Y * b.Y); 
        public static Vector operator /(Vector v, double x) => (v.X / x, v.Y / x); 
        public static Vector operator +(Vector a, Vector b) => (a.X + b.X, a.Y + b.Y); 
        public static Vector operator -(Vector v) => -1 * v;
        public static Vector operator -(Vector a, Vector b) => a + -b;

        public static implicit operator Vector((double X, double Y) v) => new Vector { X = v.X, Y = v.Y };

        public static implicit operator PointF(Vector v) => new PointF((float)v.X, (float)v.Y);
        public static explicit operator Vector(PointF p) => (p.X, p.Y);

        public static implicit operator SizeF(Vector v) => new SizeF((float)v.X, (float)v.Y);
        public static explicit operator Vector(SizeF p) => (p.Width, p.Height);
        public static explicit operator Vector(Size p) => (p.Width, p.Height);
    }
}
