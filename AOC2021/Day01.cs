// -----------------------------------------------------------------------
// <copyright file="Day01.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021
{
    using AOC2021.Data;

    public class Day01 : IDay
    {
        private readonly DataStore datastore;

        public Day01(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public int Result1()
        {
            int[] depths = this.PrepData();

            int result = depths.Where(
                (item, index) => (index > 0 && (depths[index - 1] < depths[index]))
            ).Count();

            Console.WriteLine($"Day_01_01 result: {result}");
            return result;
        }

        public int Result2()
        {
            int[] depths = this.PrepData();

            List<int> sums = new();

            for (int i = 2; i < depths.Length; i++)
            {
                sums.Add(depths[i - 2] + depths[i - 1] + depths[i]);
            }

            int result = sums.Where(
                (item, index) => (index > 0 && (sums[index - 1] < sums[index]))
            ).Count();

            Console.WriteLine($"Day_01_02 result: {result}");
            return result;
        }

        public int Result2Variant()
        {
            int[] depths = this.PrepData();

            List<int> windowedSums = depths.WindowedTraverse(3, (int accumulate, int source) => accumulate + source).ToList();

            int result = windowedSums.Where(
                (item, index) => (index > 0 && (windowedSums[index - 1] < windowedSums[index]))
            ).Count();

            Console.WriteLine($"Day_01_02 result: {result}");
            return result;
        }

        private int[] PrepData()
        {
            return this.datastore.GetRawData("01").Select(x =>
            {
                bool tryResult = int.TryParse(x, out int result);
                if (!tryResult)
                {
                    throw new InvalidDataException($"String x {x} was expected to be parsable into an int, but was not.");
                }

                return result;
            }).ToArray();
        }
    }
}
