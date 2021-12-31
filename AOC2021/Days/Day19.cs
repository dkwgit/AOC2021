﻿// -----------------------------------------------------------------------
// <copyright file="Day19.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

    public class Day19 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day19(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "";
        }

        public override string Result1()
        {
            this.PrepData();

            long result = 0;
            return result.ToString();
        }

        public override string Result2()
        {
            long result = 0;
            return result.ToString();
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
        }
    }
}