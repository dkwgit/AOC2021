// -----------------------------------------------------------------------
// <copyright file="Scanner.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models
{
    using MathNet.Spatial.Euclidean;

    internal class Scanner
    {
        internal Scanner(int id, Point3D[] points)
        {
            this.Id = id;
            this.Points = points;

            List<Point3D> pointList = new(points);
            Point3D current = pointList[0];
            pointList.RemoveAt(0);
            while (pointList.Count > 0)
            {
                foreach (var p in pointList)
                {
                    Line3D line = new(current, p);
                    double distance = line.Length;

                    if (!this.DistancesToPoints.ContainsKey(distance))
                    {
                        List<(Point3D Start, Point3D End, Line3D Line)> list = new();
                        this.DistancesToPoints[distance] = list;
                    }

                    this.DistancesToPoints[distance].Add((current, p, line));
                }

                current = pointList[0];
                pointList.RemoveAt(0);
            }
        }

        internal int Id { get; }

        internal Point3D[] Points { get; }

        internal Dictionary<double, List<(Point3D Start, Point3D End, Line3D Line)>> DistancesToPoints { get; } = new();
    }
}
