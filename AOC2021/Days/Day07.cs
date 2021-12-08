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
            int[] crabs = this.PrepData().OrderBy(c => c).ToArray();
            int minPosition = crabs[0];
            int maxPosition = crabs[^1];

            // Group the crabs by position, with count at that position.
            CrabGroup[] groupedCrabs = crabs.GroupBy(c => c).Select(g => new CrabGroup(g.Key, g.Count())).ToArray();
            List<long> fuelConsumptions = new(groupedCrabs.Length);

            // compute fuel consumption for all positions against all groups
            for (long position = minPosition; position < maxPosition; position++)
            {
                long sum = groupedCrabs.Select(group => Math.Abs(group.Position - position) * group.Count).Sum();
                fuelConsumptions.Add(sum);
            }

            long result = fuelConsumptions.Min();
            return result;
        }

        public long Result2()
        {
            // Get sorted crabs
            int[] crabs = this.PrepData().OrderBy(c => c).ToArray();
            int minPosition = crabs[0];
            int maxPosition = crabs[^1];

            // Group the crabs by position, with count at that position.
            CrabGroup[] groupedCrabs = crabs.GroupBy(c => c).Select(g => new CrabGroup(g.Key, g.Count())).ToArray();
            List<long> fuelConsumptions = new(groupedCrabs.Length);

            for (long position = crabs.Min(); position < crabs.Max(); position++)
            {
                long sum = groupedCrabs.Select(group => this.SumSequence(Math.Abs(group.Position - position)) * group.Count).Sum();

                fuelConsumptions.Add(sum);
            }

            long result = fuelConsumptions.Min();
            return result;
        }

        // Compute the sum of 1 + 2 + . . . + n
        private long SumSequence(double n)
        {
            return (long)((n / 2) * (2 + (n - 1)));
        }

        private int[] PrepData()
        {
            string[] data = this.datastore.GetRawData("07")[0].Split(",");

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
