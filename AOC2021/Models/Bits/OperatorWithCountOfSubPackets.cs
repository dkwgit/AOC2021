// -----------------------------------------------------------------------
// <copyright file="OperatorWithCountOfSubPackets.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal class OperatorWithCountOfSubPackets : Operator
    {
        internal OperatorWithCountOfSubPackets(int version, int type, BitArray bits, Packet? parent)
           : base(version, type, bits, parent)
        {
            int subPacketCount = 0;

            for (int i = 0; i < 11; i++)
            {
                subPacketCount |= bits[^(8 + i)] ? 1 : 0;
                if (i + 1 < 11)
                {
                    subPacketCount <<= 1;
                }
            }

            this.SubPacketCount = subPacketCount;
            this.ConsumedBits = 18;
            BitArray unprocessed = new BitArray(bits.Cast<bool>().Skip(18).ToArray());
            ProcessChildren(unprocessed, this);
        }

        internal int SubPacketCount { get; }

        internal override int ConsumedBits { get; }

        internal static void ProcessChildren(BitArray bits, OperatorWithCountOfSubPackets parent)
        {
            Packet.BuildPacket(bits, parent);
        }
    }
}
