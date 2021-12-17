// -----------------------------------------------------------------------
// <copyright file="Day16.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Collections;
    using AOC2021.Data;
    using AOC2021.Models.Bits;
    using FluentAssertions;

    public class Day16 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        public Day16(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "BITS";
        }

        public override string Result1()
        {
            BitArray bits = this.PrepData();

            Packet p = Packet.BuildPacket(bits, null);

            long result = 0;
            return result.ToString();
        }

        public override string Result2()
        {
            long result = 0;
            return result.ToString();
        }

        internal BitArray PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
            lines.Length.Should().Be(1);
            string line = lines[0];

            BitArray bits = new(Convert.FromHexString(line).Reverse().ToArray());
            return bits;
        }
    }
}
