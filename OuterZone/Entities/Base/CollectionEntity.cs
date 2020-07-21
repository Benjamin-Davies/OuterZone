using System.Collections.Generic;
using System.Drawing;

namespace OuterZone.Entities.Base
{
    class CollectionEntity : Entity
    {
        public readonly List<Entity> Children = new List<Entity>();
        public override Brush Fill => Brushes.Transparent;

        public override void Update(double dt)
        {
            foreach (var child in Children)
            {
                child.Update(dt);
            }
        }

        public override void Draw(Graphics g)
        {
            foreach (var child in Children)
            {
                child.Draw(g);
            }
        }
    }
}