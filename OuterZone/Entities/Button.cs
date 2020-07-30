using OuterZone.Entities.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone.Entities
{
    class Button : Entity
    {
        public override Brush Fill => Brushes.LightGray;
        public override Font Font => new Font(base.Font.FontFamily, 0.5f);
        public override Vector Size => (8, 1);

        public event EventHandler<Vector> OnClick;

        public string Text { get; set; } = "";

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            var center = Position + Size / 2;
            var textSize = (Vector)g.MeasureString(Text, Font);
            var textPosition = center - textSize / 2;
            g.DrawString(Text, Font, Brushes.Black, textPosition);
        }

        public virtual void MouseDown(Vector mousePosition)
        {
            if (Rectangle.Contains(mousePosition))
            {
                OnClick?.Invoke(this, mousePosition);
            }
        } 
    }
}
