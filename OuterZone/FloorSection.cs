using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterZone
{
    class FloorSection : Entity
    {
        public override Vector Position { get; set; } = (0, 10);
        public override Vector Size => (8, 2);
    }
}
