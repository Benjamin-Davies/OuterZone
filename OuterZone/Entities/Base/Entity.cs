using System.Drawing;

using static OuterZone.Vector;

namespace OuterZone.Entities.Base
{
    abstract class Entity
    {
        public virtual Vector Position { get; set; } = Zero;
        public virtual Vector Size => (1, 1);
        public virtual Brush Fill => Brushes.Black;
        public RectangleF Rectangle => new RectangleF(Position, Size);

        public virtual void Update(double dt) { }

        public virtual void Draw(Graphics g)
        {
            g.FillRectangle(Fill, Rectangle);
        }
    }
}
