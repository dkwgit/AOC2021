// -----------------------------------------------------------------------
// <copyright file="Day06.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using AOC2021.Models;

    public class Day06 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day06(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Lanternfish populations.";
        }

        public override string Result1()
        {
            int[] starterFish = this.PrepData();

            Sea sea = new(9, starterFish);

            for (int day = 0; day < 80; day++)
            {
                sea.ADayAtSea();
            }

            long result = sea.TotalFish;
            return result.ToString();
        }

        public override string Result2()
        {
            int[] starterFish = this.PrepData();

            Sea sea = new(9, starterFish);

            for (int day = 0; day < 256; day++)
            {
                sea.ADayAtSea();
            }

            long result = sea.TotalFish;
            return result.ToString();
        }

        private int[] PrepData()
        {
            string[] data = this.datastore.GetRawData(this.GetName())[0].Split(",");

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
