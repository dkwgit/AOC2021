// -----------------------------------------------------------------------
// <copyright file="Day06.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

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

        public int Result1()
        {
            int result = 0;
            return result;
        }

        public int Result2()
        {
            int result = 0;
            return result;
        }

        private int[] PrepData()
        {
            string[] data = this.datastore.GetRawData("06");

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
