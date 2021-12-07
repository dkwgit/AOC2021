// -----------------------------------------------------------------------
// <copyright file="Day06.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using AOC2021.Models;

    /// <summary>
    /// Day 6: Fish population projections.
    /// </summary>
    public class Day06 : IDay
    {
        private readonly DataStore datastore;

        public Day06(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day06";

        public long Result1()
        {
            int[] starterFish = this.PrepData();

            Sea sea = new(9, starterFish);

            for (int day = 0; day < 80; day++)
            {
                sea.ADayAtSea();
            }

            long result = sea.TotalFish;
            return result;
        }

        public long Result2()
        {
            int[] starterFish = this.PrepData();

            Sea sea = new(9, starterFish);

            for (int day = 0; day < 256; day++)
            {
                sea.ADayAtSea();
            }

            long result = sea.TotalFish;
            return result;
        }

        private int[] PrepData()
        {
            string[] data = this.datastore.GetRawData("06")[0].Split(",");

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
    }
}
