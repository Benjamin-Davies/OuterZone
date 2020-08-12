using OuterZone.Entities.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OuterZone.Entities
{
    class TextInput : Entity
    {
        public bool Hover { get; private set; } = false;
        public override Brush Fill => Brushes.White;
        public override Font Font => new Font(base.Font.FontFamily, 0.5f);
        public override Vector Size => (8, 1);

        public event EventHandler<Vector> OnClick;
        public event EventHandler<string> OnChange;

        private string text = "";
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnChange?.Invoke(this, value);
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            var center = Position + Size / 2;
            var textSize = (Vector)g.MeasureString(Text, Font);
            var textPosition = center - (Size.X / 2, textSize.Y / 2);
            g.DrawString(Text, Font, Brushes.Black, textPosition);

            g.FillRectangle(Brushes.Black, new RectangleF(center - (Size / 2) + (0, 0.15) + (1, 0) * textSize, new SizeF(0.05f, 0.7f)));
        }

        public virtual void MouseDown(Vector mousePosition)
        {
            if (Rectangle.Contains(mousePosition))
            {
                OnClick?.Invoke(this, mousePosition);
            }
        }

        public virtual void KeyDown(Keys key, bool shift)
        {
            switch (key)
            {
                case Keys.Back:
                    if (Text.Length > 0)
                        Text = Text.Substring(0, Text.Length - 1);
                    break;
                default:
                    var str = key.ToString();
                    if (str.Length == 1)
                    {
                        if (shift)
                            Text += str;
                        else
                            Text += str.ToLower();
                    }
                    break;
            }
        }

        public virtual void CheckHover(Vector mousePosition)
        {
            Hover = Rectangle.Contains(mousePosition);
        }
    }
}
