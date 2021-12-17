// -----------------------------------------------------------------------
// <copyright file="Packet.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal abstract class Packet
    {
        internal Packet(int version, int type, BitArray bits, Packet? parent, int subPacketLengthType = -1)
        {
            this.Version = version;
            this.Type = type;
            this.OriginalPacket = bits;
            this.RootPacket = parent;
        }

        internal Packet? RootPacket { get; }

        internal BitArray OriginalPacket { get; }

        internal int Version { get; }

        internal int Type { get; }

        internal abstract int ConsumedBits { get; }

        internal static Packet BuildPacket(BitArray bits, Packet? parent)
        {
            int version = GetVersion(bits);
            int type = GetType(bits);
            if (type == 4)
            {
                Literal packet = new Literal(version, type, bits, parent);
                return packet;
            }
            else
            {
                int subPacketLengthType = GetSubPacketLengthType(bits);
                if (subPacketLengthType == 0)
                {
                    OperatorWithFixedSubPacketBitLength packet = new OperatorWithFixedSubPacketBitLength(version, type, bits, parent);
                    OperatorWithFixedSubPacketBitLength.ProcessChildren(bits, packet);
                    return packet;
                }
                else
                {
                    OperatorWithCountOfSubPackets packet = new OperatorWithCountOfSubPackets(version, type, bits, parent);
                    OperatorWithCountOfSubPackets.ProcessChildren(bits, packet);
                    return packet;
                }
            }
        }

        internal static int GetVersion(BitArray bits)
        {
            int version = 0;
            version |= bits[^1] ? 1 : 0;
            version <<= 1;
            version |= bits[^2] ? 1 : 0;
            version <<= 1;
            version |= bits[^3] ? 1 : 0;
            return version;
        }

        internal static int GetType(BitArray bits)
        {
            int type = 0;
            type |= bits[^4] ? 1 : 0;
            type <<= 1;
            type |= bits[^5] ? 1 : 0;
            type <<= 1;
            type |= bits[^6] ? 1 : 0;
            return type;
        }

        internal static int GetSubPacketLengthType(BitArray bits)
        {
            return bits[^7] ? 1 : 0;
        }
    }
}
