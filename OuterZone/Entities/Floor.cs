﻿using OuterZone.Entities.Base;
using System;

namespace OuterZone.Entities
{
    class Floor : CollectionEntity
    {
        private const int MaxListCount = 10;
        private const int DiscardCount = 5;
        private const double InitialProbability = 0.1;
        private const double FirstGap = 2;

        private double NextPosition = -20;
        private double TileProbability = InitialProbability;
        private Random Random = new Random();

        public void Generate(double rightEdge)
        {
            while (NextPosition < rightEdge)
            {
                GenerateTile();
            }

            if (Children.Count > MaxListCount)
            {
                Children.RemoveRange(0, DiscardCount);
            }
        }

        public void GenerateTile()
        {
            if (NextPosition > FirstGap && Random.NextDouble() < TileProbability)
            {
                TileProbability = InitialProbability;
            }
            else
            {
                TileProbability *= 2;

                Children.Add(new FloorSection
                {
                    Position = (NextPosition, Position.Y),
                });
            }
            NextPosition += FloorSection.Width;
        }
    }
}