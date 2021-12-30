// -----------------------------------------------------------------------
// <copyright file="IOperatorPacket.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;
    using AOC2021.Models.Bits.ValueCalculator;

    internal enum SubPacketLengthDescriptor
    {
        BitLength,
        SubPacketCount,
    }

    internal interface IOperatorPacket : IPacket
    {
        SubPacketLengthDescriptor LengthType { get; }

        List<IPacket> Children { get; }

        IValueCalculator Calculator { get; }

        void CalculateValue();
    }
}
