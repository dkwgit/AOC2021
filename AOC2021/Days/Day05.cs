// -----------------------------------------------------------------------
// <copyright file="Day05.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Drawing;
    using System.Text.RegularExpressions;
    using AOC2021.Data;
    using AOC2021.Models.Map;
    using FluentAssertions;
    using Map = AOC2021.Models.Map.Map;

    /// <summary>
    /// Day 5: Mapping thermal vents on the ocean floor.
    /// </summary>
    public class Day05 : IDay
    {
        private readonly DataStore datastore;

        public Day05(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day05";

        private Map TheMap { get; set; } = default!;

        public long Result1()
        {
            (Point Point1, Point Point2)[] allLines = this.PrepData();

            // Filter down to only lines that where either x or y are the same (vertical or horizontal)
            (Point Point1, Point Point2)[] lines = this.PrepData().Where(line => line.Item1.X == line.Item2.X || line.Item1.Y == line.Item2.Y).ToArray();

            int xSide = allLines.Select(x => x.Point1.X >= x.Point2.X ? x.Point1.X : x.Point2.X).Max() + 1;
            int ySide = allLines.Select(y => y.Point1.Y >= y.Point2.Y ? y.Point1.Y : y.Point2.Y).Max() + 1;

            int side = xSide >= ySide ? xSide : ySide;

            this.TheMap = new Map(side, side);

            foreach (var line in lines)
            {
                this.TheMap.TraceLine(line);
            }

            long result = this.TheMap.CountHighOverlap();
            return result;
        }

        public long Result2()
        {
            (Point Point1, Point Point2)[] allLines =
               this.PrepData();

            int xSide = allLines.Select(x => x.Point1.X >= x.Point2.X ? x.Point1.X : x.Point2.X).Max() + 1;
            int ySide = allLines.Select(y => y.Point1.Y >= y.Point2.Y ? y.Point1.Y : y.Point2.Y).Max() + 1;

            int side = xSide >= ySide ? xSide : ySide;

            this.TheMap = new Map(side, side);

            foreach (var line in allLines)
            {
                this.TheMap.TraceLine(line);
            }

            long result = this.TheMap.CountHighOverlap();
            return result;
        }

        private (Point, Point)[] PrepData()
        {
            string[] data = this.datastore.GetRawData("05");

            string pattern = @"^(\d+),(\d+)\s?->\s?(\d+),(\d+)$";
            return data.Select(s =>
            {
                var matches = Regex.Matches(s, pattern);
                var groups = matches[0].Groups;
                groups.Count.Should().Be(5);
                bool tryX1 = int.TryParse(groups[1].Value, out int x1);
                bool tryY1 = int.TryParse(groups[2].Value, out int y1);
                bool tryX2 = int.TryParse(groups[3].Value, out int x2);
                bool tryY2 = int.TryParse(groups[4].Value, out int y2);
                if (!tryX1 || !tryY1 || !tryX2 || !tryY2)
                {
                    throw new InvalidDataException($"String x1 {x1} or y1 {y1}  or x2 {x2} or y2 {y2} were expected to be parsable into ints, but at least one was not");
                }

                return (new Point(x1, y1), new Point(x2, y2));
            }).ToArray();
        }
    }
}
