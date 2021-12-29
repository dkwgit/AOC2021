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
        internal OperatorWithCountOfSubPackets(BitArray bits, int distanceFromTop, int version, int type, SubPacketLengthDescriptor descriptor, Action<IPacket, int> packetRegistrationFunction)
           : base(version, type, descriptor, packetRegistrationFunction)
        {
            int subPacketCount = 0;

            for (int i = 0 + distanceFromTop; i < 11 + distanceFromTop; i++)
            {
                subPacketCount |= bits[^(8 + i)] ? 1 : 0;
                if (i + 1 < distanceFromTop + 11)
                {
                    subPacketCount <<= 1;
                }
            }

            this.SubPacketCount = subPacketCount;
            this.consumedBits = 18; // 3 version bits + 3 type bits + 1 length type bit + 11 count bits.
        }

        internal int SubPacketCount { get; }
    }
}
