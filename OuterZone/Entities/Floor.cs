using OuterZone.Entities.Base;

namespace OuterZone.Entities
{
    class Floor : CollectionEntity
    {
        private const int MaxListCount = 10;
        private const int DiscardCount = 5;

        private double NextPosition = -20;

        public void Generate(double rightEdge)
        {
            while (NextPosition < rightEdge)
            {
                Children.Add(new FloorSection
                {
                    Position = (NextPosition, Position.Y),
                });
                NextPosition += FloorSection.Width;
            }

            if (Children.Count > MaxListCount)
            {
                Children.RemoveRange(0, DiscardCount);
            }
        }
    }
}
