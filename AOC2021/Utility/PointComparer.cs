// -----------------------------------------------------------------------
// <copyright file="PointComparer.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Utility
{
    using System.Drawing;

    public class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public int GetHashCode(Point p)
        {
            int hCode = p.X ^ p.Y;
            return hCode.GetHashCode();
        }
    }
}
