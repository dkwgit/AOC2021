﻿// -----------------------------------------------------------------------
// <copyright file="Literal.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal class Literal : Packet
    {
        internal Literal(BitArray bits, int distanceFromTop, int version, int type,  Action<IPacket, int> packetRegistrationFunction)
            : base(version, type, packetRegistrationFunction)
        {
            int i = 7 + distanceFromTop;
            bool more = true;
            int literal = 0;
            int bitCount = 6; // already handled version and type
            while (more)
            {
                more = bits[^i++];
                literal |= bits[^i++] ? 1 : 0;
                literal <<= 1;
                literal |= bits[^i++] ? 1 : 0;
                literal <<= 1;
                literal |= bits[^i++] ? 1 : 0;
                literal <<= 1;
                literal |= bits[^i++] ? 1 : 0;
                bitCount += 5;
            }

            this.Number = literal;
            this.consumedBits = bitCount;
        }

        internal int Number { get; }
    }
}
