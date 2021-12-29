// -----------------------------------------------------------------------
// <copyright file="OperatorWithFixedSubPacketBitLength.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;
    using System.Linq;

    internal class OperatorWithFixedSubPacketBitLength : Operator
    {
        internal OperatorWithFixedSubPacketBitLength(BitArray bits, int distanceFromTop, int version, int type, SubPacketLengthDescriptor descriptor, Action<IPacket, int> packetRegistrationFunction)
           : base(version, type, descriptor, packetRegistrationFunction)
        {
            int subPacketBitLength = 0;

            for (int i = 0 + distanceFromTop; i < 15 + distanceFromTop; i++)
            {
                subPacketBitLength |= bits[^(8 + i)] ? 1 : 0;
                if (i + 1 < 15 + distanceFromTop)
                {
                    subPacketBitLength <<= 1;
                }
            }

            this.SubPacketBitLength = subPacketBitLength;
            this.consumedBits = 22; // 3 version bits + 3 type bits + 1 length descriptor bit + 15 length count bits
        }

        internal int SubPacketBitLength { get; }
    }
}
