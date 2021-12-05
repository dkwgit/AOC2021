// -----------------------------------------------------------------------
// <copyright file="Map.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Map
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a two dimensional map.
    /// </summary>
    internal class Map
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="xSide">X side length of the map.</param>
        /// <param name="ySide">Y side length of the map.</param>
        internal Map(int xSide, int ySide)
        {
            (this.XSide, this.YSide) = (xSide, ySide);
            this.MapPoints = new MapPoint[this.YSide, this.XSide];
            for (int x = 0; x < this.YSide; x++)
            {
                for (int y = 0; y < this.XSide; y++)
                {
                    this.MapPoints[x, y] = new MapPoint(new Point(x, y), 0);
                }
            }
        }

        private int XSide { get; init; }

        private int YSide { get; init; }

        private MapPoint[,] MapPoints { get; init; }

        /* internal void TraceLineOld((Point Point1, Point Point2) line)
        {
            LineHelperOld helper = new(line.Point1, line.Point2);
            (Point lower, Point higher) = helper.PointOrderSelector(line.Point1, line.Point2);
            Point[] points = new Point[] { lower, higher };

            int min = points.Select(p => helper.ChangingValueSelector(p)).Min();
            int max = points.Select(p => helper.ChangingValueSelector(p)).Max();

            for (int value = min; value <= max; value++)
            {
                Point p = helper.PointCreator(value);
                this.MapPoints[p.Y, p.X].HitCount++;
            }
        }*/

        /// <summary>
        /// Trace a line on the map, marking each point on the line as having hit (increments hit count).
        /// </summary>
        /// <param name="line">A line defined by 2 points.</param>
        internal void TraceLine((Point Point1, Point Point2) line)
        {
            LineHelper helper = new(line.Point1, line.Point2);

            Point current = line.Point1;
            while (true)
            {
                this.MapPoints[current.Y, current.X].HitCount++;
                if (current.Equals(line.Point2))
                {
                    break;
                }

                current = helper.GetNext(current);
            }
        }

        /// <summary>
        /// Finding all points on the map with 2 hits or greater.
        /// </summary>
        /// <returns>Number of points that have 2 hits or greater.</returns>
        internal int CountHighOverlap()
        {
            int count = 0;

            for (int y = 0; y < this.YSide; y++)
            {
                for (int x = 0; x < this.XSide; x++)
                {
                    if (this.MapPoints[y, x].HitCount >= 2)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// A single point on the map.
        /// </summary>
        internal struct MapPoint
        {
            internal MapPoint(Point p, int hitCount) => (this.P, this.HitCount) = (p, hitCount);

            internal Point P { get; init; }

            internal int HitCount { get; set; }
        }

        /* internal class LineHelperOld
        {
            internal LineHelperOld(Point p1, Point p2)
            {
                if (p1.X == p2.X)
                {
                    int constant = p1.X;
                    this.ChangingValueSelector = (Point p) => p.Y;
                    this.PointCreator = (int value) => new Point(constant, value);
                }
                else
                {
                    int constant = p1.Y;
                    this.ChangingValueSelector = (Point p) => p.X;
                    this.PointCreator = (int value) => new Point(value, constant);
                }

                this.PointOrderSelector = (Point p1, Point p2) => this.ChangingValueSelector(p1) < this.ChangingValueSelector(p2) ? (p1, p2) : (p2, p1);
            }

            internal Func<Point, int> ChangingValueSelector { get; init; }

            internal Func<int, Point> PointCreator { get; init; }

            internal Func<Point, Point, (Point Lower, Point Higher)> PointOrderSelector { get; init; }
        }*/

        /// <summary>
        /// Helper class to generate points between end points on a line segment, inclusive.
        /// </summary>
        internal class LineHelper
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LineHelper"/> class.
            /// Construct with the 2 end points.
            /// </summary>
            /// <param name="p1">Endpoint 1.</param>
            /// <param name="p2">Endpoint 2.</param>
            internal LineHelper(Point p1, Point p2)
            {
                int x = Math.Sign(p2.X - p1.X);
                int y = Math.Sign(p2.Y - p1.Y);

                this.OffSetPoint = new Point(x, y);
            }

            /// <summary>
            /// Gets or sets a point representing the offset between each succesive point. So, given a point, this offset point will generate the next one.
            /// </summary>
            internal Point OffSetPoint { get; set; }

            /// <summary>
            /// Given a point currently on the line, generate the next one in the offset direction.
            /// </summary>
            /// <param name="p">A point currently on the line.</param>
            /// <returns>The new point.</returns>
            internal Point GetNext(Point p)
            {
                p.Offset(this.OffSetPoint);
                return p;
            }
        }
    }
}
