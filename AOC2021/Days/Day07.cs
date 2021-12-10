// -----------------------------------------------------------------------
// <copyright file="Day07.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

    /// <summary>
    /// Day 7: Whales and crabs and fuel, oh my.
    /// </summary>
    public class Day07 : IDay
    {
        private readonly DataStore datastore;

        public Day07(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day07";

        public long Result1()
        {
            int[] crabs = this.PrepData();

            // Group the crabs by position, with count at that position. Use a sort along the way to get min max.
            CrabGroup[] groupedCrabs = crabs.GroupBy(c => c).Select(g => new CrabGroup(g.Key, g.Count())).OrderBy(x => x.Position).ToArray();
            int minPosition = groupedCrabs[0].Position;
            int maxPosition = groupedCrabs[^1].Position;

            // compute fuel consumption for all positions against all groups
            long minConsumption = long.MaxValue;
            for (long position = minPosition; position < maxPosition; position++)
            {
                long sum = groupedCrabs.Select(group => Math.Abs(group.Position - position) * group.Count).Sum();

                minConsumption = sum < minConsumption ? sum : minConsumption;
            }

            long result = minConsumption;
            return result;
        }

        public long Result2()
        {
            int[] crabs = this.PrepData();

            // Group the crabs by position, with count at that position. Use a sort to get min max.
            CrabGroup[] groupedCrabs = crabs.GroupBy(c => c).Select(g => new CrabGroup(g.Key, g.Count())).OrderBy(x => x.Position).ToArray();
            int minPosition = groupedCrabs[0].Position;
            int maxPosition = groupedCrabs[^1].Position;

            long minConsumption = long.MaxValue;
            for (long position = minPosition; position < maxPosition; position++)
            {
                long sum = groupedCrabs.Select(group => this.SumSequence(Math.Abs(group.Position - position)) * group.Count).Sum();

                minConsumption = sum < minConsumption ? sum : minConsumption;
            }

            long result = minConsumption;
            return result;
        }

        // Compute the sum of 1 + 2 + . . . + n
        private long SumSequence(long n)
        {
            return (n * (1 + n)) / 2;
        }

        private int[] PrepData()
        {
            string[] data = this.datastore.GetRawData(this.Name)[0].Split(",");

            return data.Select(s =>
            {
                bool tryParse = int.TryParse(s, out int number);
                if (!tryParse)
                {
                    throw new InvalidDataException($"String s {s} was expected to be parsable into ints, but at least one was not");
                }

                return number;
            }).ToArray();
        }

        internal record CrabGroup(int Position, int Count);
    }
}
