// -----------------------------------------------------------------------
// <copyright file="Basin.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models
{
    using System.Drawing;

    internal class Basin
    {
        internal Basin(int[,] map, Point lowPoint)
        {
            this.LowPoint = lowPoint;

            this.PointsToExplore.Add(lowPoint);

            this.Map = map;
        }

        internal HashSet<Point> BasinPoints { get; } = new();

        private HashSet<Point> PointsToExplore { get; } = new();

        private int[,] Map { get; init; }

        private Point LowPoint { get; init; }

        private Size[] Sizes { get; } = new Size[] { new Size(0, -1), new Size(1, 0),  new Size(0, 1), new Size(-1, 0) };

        internal void MapBasin()
        {
            while (this.PointsToExplore.Count > 0)
            {
                this.CatalogSurroundingPoints(this.PointsToExplore.First());
            }
        }

        private void CatalogSurroundingPoints(Point p)
        {
            if (!this.BasinPoints.Contains(p))
            {
                this.BasinPoints.Add(p);
            }

            this.PointsToExplore.Remove(p);

            foreach (Size s in this.Sizes)
            {
                Point otherPoint = Point.Add(p, s);
                if (this.Map![otherPoint.Y, otherPoint.X] != 9)
                {
                    if (!this.BasinPoints.Contains(otherPoint) && !this.PointsToExplore.Contains(otherPoint))
                    {
                        this.PointsToExplore.Add(otherPoint);
                    }
                }
            }
        }
    }
}
