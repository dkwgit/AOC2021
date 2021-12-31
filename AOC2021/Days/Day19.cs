// -----------------------------------------------------------------------
// <copyright file="Day19.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Text.RegularExpressions;
    using AOC2021.Data;
    using AOC2021.Models;
    using MathNet.Spatial.Euclidean;

    public class Day19 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private List<Scanner>? scanners;

        public Day19(DataStore datastore)
        {
            this.datastore = datastore;
        }

        internal List<Scanner> Scanners
        {
            get
            {
                if (this.scanners == null)
                {
                    throw new InvalidOperationException("Access to Scanners property before initialized.");
                }

                return this.scanners;
            }
        }

        public override string GetDescription()
        {
            return "Scanner/beacon mapping";
        }

        public override string Result1()
        {
            this.PrepData();

            var intersections = this.Scanners.Skip(1).Select(s => s.DistancesToPoints.Keys.Where(k => s.DistancesToPoints[k].Count == 1).Intersect(this.Scanners[0].DistancesToPoints.Keys.Where(k => this.Scanners[0].DistancesToPoints[k].Count == 1)).ToList()).ToList();

            long result = 0;
            return result.ToString();
        }

        public override string Result2()
        {
            long result = 0;
            return result.ToString();
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            string state = "initial";
            int scannerId = -1;
            List<Point3D> points = new();
            List<Scanner> scanners = new();
            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    state = "initial";
                    scanners.Add(new Scanner(scannerId, points.ToArray()));
                    scannerId = -1;
                    points.Clear();
                }

                if (state == "points")
                {
                    string[] strings = line.Split(",");
                    double[] coordinates = strings.Select(s => double.Parse(s)).ToArray();
                    Point3D point = new(coordinates[0], coordinates[1], coordinates[2]);
                    points.Add(point);
                }

                if (state == "initial" && line.Contains("scanner"))
                {
                    string pattern = @"(\d+)";
                    Match m = Regex.Match(line, pattern);
                    if (m.Success)
                    {
                        scannerId = int.Parse(m.Groups[1].Value);
                    }

                    state = "points";
                }
            }

            scanners.Add(new Scanner(scannerId, points.ToArray()));

            this.scanners = scanners;
        }
    }
}
