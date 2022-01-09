// -----------------------------------------------------------------------
// <copyright file="Day22.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;

    internal class Day22 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day22(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Reactor Reboot";
        }

        public override string Result1()
        {
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
            string[] data = this.datastore.GetRawData(this.GetName());
        }
    }
}
