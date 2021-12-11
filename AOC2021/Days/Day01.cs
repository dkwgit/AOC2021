// -----------------------------------------------------------------------
// <copyright file="Day01.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

    /// <summary>
    /// Day 1: summarizing trends in a series.
    /// </summary>
    public class Day01 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day01(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override long Result1()
        {
            int[] depths = this.PrepData();

            long result = depths.Where(
                (item, index) => (index > 0 && (depths[index - 1] < depths[index]))
            ).Count();

            return result;
        }

        public override long Result2()
        {
            int[] depths = this.PrepData();

            List<int> sums = new();

            for (int i = 2; i < depths.Length; i++)
            {
                sums.Add(depths[i - 2] + depths[i - 1] + depths[i]);
            }

            long result = sums.Where(
                (item, index) => (index > 0 && (sums[index - 1] < sums[index]))
            ).Count();

            return result;
        }

        /// <summary>
        /// This variant does the calculation via a windowed Linq function.
        /// </summary>
        /// <returns>Result 2 for Day 1.</returns>
        public long Result2Variant()
        {
            int[] depths = this.PrepData();

            List<int> windowedSums = depths.WindowedTraverse(3, (int accumulate, int source) => accumulate + source).ToList();

            long result = windowedSums.Where(
                (item, index) => (index > 0 && (windowedSums[index - 1] < windowedSums[index]))
            ).Count();

            return result;
        }

        public override IDayResult[] GetResults()
        {
            return new IDayResult[]
            {
                new DayResult(this.GetName(), 1, string.Empty, this.Result1()),
                new DayResult(this.GetName(), 2, string.Empty, this.Result2()),

                // new DayResult(this.GetName(), 2, string.Empty, this.Result2Variant(), "Variant"),
            };
        }

        private int[] PrepData()
        {
            return this.datastore.GetRawData(this.GetName()).Select(x =>
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
