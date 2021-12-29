// -----------------------------------------------------------------------
// <copyright file="IPacket.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal interface IPacket
    {
        int Version { get; }

        int Type { get; }

        int ConsumedBits { get; }
    }
}
