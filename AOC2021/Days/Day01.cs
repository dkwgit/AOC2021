﻿// -----------------------------------------------------------------------
// <copyright file="Day01.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Diagnostics;
    using AOC2021.Data;

    public class Day01 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day01(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Series trends.";
        }

        public override string Result1()
        {
            int[] depths = this.PrepData();

            long result = depths.Where(
                (item, index) => (index > 0 && (depths[index - 1] < depths[index]))
            ).Count();

            return result.ToString();
        }

        public override string Result2()
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

            return result.ToString();
        }

        /// <summary>
        /// This variant does the calculation via a windowed Linq function.
        /// </summary>
        /// <returns>Result 2 for Day 1.</returns>
        public long Result2Variant()
        {
            int[] depths = this.PrepData();

            List<int> windowedSums = depths.WindowedAggregation(3, (int accumulate, int source) => accumulate + source).ToList();

            long result = windowedSums.Where(
                (item, index) => (index > 0 && (windowedSums[index - 1] < windowedSums[index]))
            ).Count();

            return result;
        }

        public override IDayResult[] GetResults()
        {
            List<IDayResult> dayResults = new(base.GetResults());

            /*Stopwatch sw = new();
            sw.Start();
            long result = this.Result2Variant();
            sw.Stop();
            long timing = sw.ElapsedMilliseconds;

            dayResults.Add(new DayResult(this.GetName(), 2, string.Empty, result, timing, "Variant"));*/

            return dayResults.ToArray();
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
