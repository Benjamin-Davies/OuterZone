using System.CodeDom;
using System.Drawing;

namespace OuterZone
{
    struct Vector
    {
        public double X;
        public double Y;

        public static Vector Zero => (0, 0);

        public static Vector operator *(double x, Vector v) => (x * v.X, x * v.Y); 
        public static Vector operator +(Vector a, Vector b) => (a.X + b.X, a.Y + b.Y); 

        public static implicit operator Vector((double X, double Y) v) => new Vector { X = v.X, Y = v.Y };

        public static implicit operator PointF(Vector v) => new PointF((float)v.X, (float)v.Y);
        public static explicit operator Vector(PointF p) => (p.X, p.Y);

        public static implicit operator SizeF(Vector v) => new SizeF((float)v.X, (float)v.Y);
        public static explicit operator Vector(SizeF p) => (p.Width, p.Height);
    }
}
