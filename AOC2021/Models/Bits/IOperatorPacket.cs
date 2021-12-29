// -----------------------------------------------------------------------
// <copyright file="IOperatorPacket.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal enum SubPacketLengthDescriptor
    {
        BitLength,
        SubPacketCount,
    }

    internal interface IOperatorPacket : IPacket
    {
        SubPacketLengthDescriptor LengthType { get; }
    }
}
