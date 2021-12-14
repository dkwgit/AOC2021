// -----------------------------------------------------------------------
// <copyright file="Day13.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Drawing;
    using System.Text;
    using AOC2021.Data;
    using AOC2021.Utility;

    public class Day13 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day13(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public static void Print(string message, HashSet<Point> points)
        {
            Console.WriteLine(message);

            int maxX = points.Max(p => p.X) + 1;
            int maxY = points.Max(p => p.Y) + 1;

            for (int y = 0; y < maxY; y++)
            {
                StringBuilder sb = new();
                for (int x = 0; x < maxX; x++)
                {
                    if (points.Contains(new Point(x, y)))
                    {
                        sb.Append('*');
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }

                Console.WriteLine(sb.ToString());
            }

            Console.WriteLine(string.Empty);
        }

        public override string GetDescription()
        {
            return "Transparent Origami.";
        }

        public override string Result1()
        {
            (HashSet<Point> points, List<int> folds) = this.PrepData();

            int fold = folds[0];
            points = DoFold(points, fold);

            long result = points.Count;
            return result.ToString();
        }

        public override string Result2()
        {
            (HashSet<Point> points, List<int> folds) = this.PrepData();

            // int i = 1;
            foreach (int fold in folds)
            {
                points = DoFold(points, fold);
                /*
                 * this.Print($"After fold {i++}", points);
                 */
            }

            Print("Solution for Day13", points);

            // Carrying over solution into string below after visual inspection of printout above
            string result = "HLBUBGFR";
            return result;
        }

        public (HashSet<Point> Points, List<int> Folds) PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            HashSet<Point> points = new(new PointComparer());

            List<int> folds = new();

            string state = "points";

            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    state = "folds";
                    continue;
                }

                if (state == "points")
                {
                    string[] coords = line.Split(',');
                    int x = int.Parse(coords[0]);
                    int y = int.Parse(coords[1]);

                    points.Add(new Point(x, y));
                }
                else
                {
                    string fold = line;
                    int f;

                    if (line.Contains("along x"))
                    {
                        f = 1;
                        fold = fold.Replace('x', ' ');
                    }
                    else
                    {
                        f = -1;
                        fold = fold.Replace('y', ' ');
                    }

                    fold = fold.Replace("fold along  =", string.Empty);
                    f *= int.Parse(fold);
                    folds.Add(f);
                }
            }

            return (points, folds);
        }

        private static HashSet<Point> DoFold(HashSet<Point> points, int fold)
        {
            HashSet<Point> newPoints = new(new PointComparer());

            foreach (Point p in points)
            {
                Size s = fold > 0 ? new Size(-2 * (p.X - fold), 0) : new Size(0, -2 * (p.Y - Math.Abs(fold)));
                if (p.X > fold && fold > 0)
                {
                    Point newPoint = Point.Add(p, s);
                    if (!newPoints.Contains(newPoint))
                    {
                        newPoints.Add(newPoint);
                    }
                }
                else if (p.Y > Math.Abs(fold) && fold < 0)
                {
                    Point newPoint = Point.Add(p, s);
                    if (!newPoints.Contains(newPoint))
                    {
                        newPoints.Add(newPoint);
                    }
                }
                else
                {
                    newPoints.Add(p);
                }
            }

            return newPoints;
        }
    }
}
