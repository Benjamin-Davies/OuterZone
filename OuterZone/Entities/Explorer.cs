using System.Drawing;

namespace OuterZone.Entities
{
    class Explorer : Base.PhysicsEntity
    {
        public override Vector Size => (1, 2);
        public override Brush Fill => Brushes.Red;
    }
}
