// -----------------------------------------------------------------------
// <copyright file="Day02.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

    /// <summary>
    /// Day 2: Submarine depth calculation, etc.
    /// </summary>
    public class Day02 : IDay
    {
        private readonly DataStore datastore;

        public Day02(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day02";

        public long Result1()
        {
            (int, int)[] coords = this.PrepData1();

            (int Forward, int Vertical) aggregate = coords.Aggregate<(int, int)>(
                ((int, int) element, (int, int) accumulate) =>
                {
                    (int forward, int vertical) = element;
                    (int f, int v) = accumulate;
                    return (forward + f, vertical + v);
                });

            long result = aggregate.Forward * aggregate.Vertical;
            return result;
        }

        public long Result2()
        {
            (int, int)[] coords = this.PrepData2();

            (int Forward, int Vertical) aggregate = coords.Aggregate<(int, int)>(
                ((int, int) element, (int, int) accumulate) =>
                {
                    (int forward, int vertical) = element;
                    (int f, int v) = accumulate;
                    return (forward + f, vertical + v);
                });

            long result = aggregate.Forward * aggregate.Vertical;
            return result;
        }

        private (int Forward, int Vertical)[] PrepData1()
        {
            string[] data = this.datastore.GetRawData("02");
            return data.Select(s =>
            {
                int forward = 0;
                int vertical = 0;

                if (s.Contains("forward"))
                {
                    s = s.Replace("forward ", string.Empty);
                    bool tryResult = int.TryParse(s, out forward);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }
                }

                if (s.Contains("up"))
                {
                    s = s.Replace("up ", string.Empty);
                    bool tryResult = int.TryParse(s, out vertical);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }

                    vertical *= -1;
                }

                if (s.Contains("down"))
                {
                    s = s.Replace("down ", string.Empty);
                    bool tryResult = int.TryParse(s, out vertical);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }
                }

                return (forward, vertical);
            }).ToArray();
        }

        private (int Forward, int Vertical)[] PrepData2()
        {
            string[] data = this.datastore.GetRawData("02");
            int aim = 0;

            return data.Select(s =>
            {
                int forward = 0;
                int vertical = 0;
                int aimChange = 0;

                if (s.Contains("forward"))
                {
                    s = s.Replace("forward ", string.Empty);
                    bool tryResult = int.TryParse(s, out forward);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String s {s} was expected to be parsable into an int, but was not.");
                    }

                    vertical = aim * forward;
                }

                if (s.Contains("up"))
                {
                    s = s.Replace("up ", string.Empty);
                    bool tryResult = int.TryParse(s, out aimChange);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }

                    aim -= aimChange;
                }

                if (s.Contains("down"))
                {
                    s = s.Replace("down ", string.Empty);
                    bool tryResult = int.TryParse(s, out aimChange);
                    if (!tryResult)
                    {
                        throw new InvalidDataException($"String bingoNumber {s} was expected to be parsable into an int, but was not.");
                    }

                    aim += aimChange;
                }

                return (forward, vertical);
            }).ToArray();
        }
    }
}
