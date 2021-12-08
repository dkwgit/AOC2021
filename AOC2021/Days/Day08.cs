// -----------------------------------------------------------------------
// <copyright file="Day08.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

    /// <summary>
    /// Day 8:Crossed Wires.
    /// </summary>
    public class Day08 : IDay
    {
        private readonly DataStore datastore;

        public Day08(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public string Name { get; init; } = "Day08";

        public long Result1()
        {
            long result = 0;
            return result;
        }

        public long Result2()
        {
            long result = 0;
            return result;
        }

        private void PrepData()
        {
            string[] data = this.datastore.GetRawData("08");
        }
    }
}
