// -----------------------------------------------------------------------
// <copyright file="Operator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal abstract class Operator : Packet
    {
        internal Operator(int version, int type, BitArray bits, Packet? parent)
           : base(version, type, bits, parent)
        {
        }

        internal List<Packet> SubPackets { get; } = new();
    }
}
