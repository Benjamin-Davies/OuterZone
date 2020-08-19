using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone.Entities
{
    class FloorSection : Base.Entity
    {
        public static readonly double Width = 4;
        public static readonly double Height = 3;

        public override Vector Position { get; set; } = (0, 10);
        public override Vector Size => (Width, Height);

        public int ColorVariant { get; set; }
        public override Brush Fill => (new Brush[]
        { 
            Brushes.DarkOliveGreen,
            Brushes.DarkGreen,
            Brushes.Green,
        })[ColorVariant];
    }
}
