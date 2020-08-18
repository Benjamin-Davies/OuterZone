using OuterZone.Entities.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone.Entities
{
    class Background : CollectionEntity
    {
        private Vector screenSize;
        public override Vector Size => screenSize;
        public Vector ScreenSize { set => screenSize = value; }

        public override Brush Fill => Brushes.Transparent;

        public double PlayerPosition { get; set; }
        private double lastPlacePosition;
        private double lastPlayerPosition;
        public Random Random { get; set; }

        public override void Update(double dt)
        {
            while (PlayerPosition > lastPlacePosition && Size.SizeSq > 0)
            {
                Children.Add(new BackgroundObject
                {
                    Alpha = Random.Next(20, 100),
                    Rate = 1 / (2 + 3*Random.NextDouble()),
                    Position = Position + Size * (1, Random.NextDouble()),
                });

                lastPlacePosition += 3;
            }

            var deltaPosition = PlayerPosition - lastPlayerPosition;
            lastPlayerPosition = PlayerPosition;

            foreach (var child in Children)
            {
                child.Position -= ((child as BackgroundObject).Rate * deltaPosition, 0);
            }
        }

        class BackgroundObject : Entity
        {
            public int Alpha;
            public double Rate;
            public override Brush Fill => new SolidBrush(Color.FromArgb(Alpha, Color.DarkGray));
        }
    }
}
