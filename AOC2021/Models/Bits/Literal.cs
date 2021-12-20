// -----------------------------------------------------------------------
// <copyright file="Literal.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal class Literal : Packet
    {
        internal Literal(int version, int type, BitArray bits, Packet? parent, Action<IPacket> packetRegistrationFunction)
            : base(version, type, bits, parent, packetRegistrationFunction)
        {
            int i = 7;
            bool more = true;
            int literal = 0;
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
            }

            this.Number = literal;
            this.consumedBits = 6 + (i - 7);
        }

        internal int Number { get; }
    }
}
