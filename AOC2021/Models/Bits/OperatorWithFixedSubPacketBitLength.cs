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
        internal OperatorWithFixedSubPacketBitLength(int version, int type, BitArray bits, Packet? parent)
           : base(version, type, bits, parent)
        {
            int subPacketBitLength = 0;

            for (int i = 0; i < 15; i++)
            {
                subPacketBitLength |= bits[^(8 + i)] ? 1 : 0;
                if (i + 1 < 15)
                {
                    subPacketBitLength <<= 1;
                }
            }

            this.SubPacketBitLength = subPacketBitLength;
            this.ConsumedBits = 22;
            BitArray unprocessed = new BitArray(bits.Cast<bool>().Skip(22).ToArray());
            ProcessChildren(unprocessed, this);
        }

        internal int SubPacketBitLength { get; }

        internal override int ConsumedBits { get; }

        internal static void ProcessChildren(BitArray bits, OperatorWithFixedSubPacketBitLength parent)
        {
            bool processChildren = true;
            while (processChildren)
            {
                Packet subPacket = Packet.BuildPacket(bits, parent);
                parent.SubPackets.Add(subPacket);
                BitArray unprocessed = new BitArray(bits.Cast<bool>().Skip(subPacket.ConsumedBits).ToArray());
                if (unprocessed.Length < 11)
                {
                    processChildren = false;
                }
            }
        }
    }
}
