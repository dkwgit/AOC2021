// -----------------------------------------------------------------------
// <copyright file="Packet.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;
    using System.Collections.Generic;

    internal abstract class Packet : IPacket
    {
        protected int consumedBits = -1;

        internal Packet(int version, int type, BitArray bits, Packet? parent, Action<IPacket> packetRegistrationFunction)
        {
            this.Bits = bits;
            this.Version = version;
            this.Type = type;
            this.Bits = bits;
            this.Parent = parent;
            this.PacketRegistrationFunction = packetRegistrationFunction;
        }

        public int ConsumedBits => this.consumedBits;

        public int Version { get; }

        public int Type { get; }

        public IPacket? Parent { get; }

        public BitArray Bits { get; }

        public Action<IPacket> PacketRegistrationFunction { get; }

        internal static IPacket BuildPacket(BitArray bits, Packet? parent, Action<IPacket> packetRegistrationFunction)
        {
            int version = GetVersion(bits);
            int type = GetType(bits);
            if (type == 4)
            {
                Literal packet = new Literal(version, type, bits, parent, packetRegistrationFunction);
                packetRegistrationFunction(packet);
                return packet;
            }
            else
            {
                SubPacketLengthDescriptor descriptor = GetSubPacketLengthType(bits);
                if (descriptor == SubPacketLengthDescriptor.BitLength)
                {
                    OperatorWithFixedSubPacketBitLength packet = new OperatorWithFixedSubPacketBitLength(version, type, bits, parent, descriptor, packetRegistrationFunction);
                    packetRegistrationFunction(packet);
                    return packet;
                }
                else
                {
                    OperatorWithCountOfSubPackets packet = new OperatorWithCountOfSubPackets(version, type, bits, parent, descriptor, packetRegistrationFunction);
                    packetRegistrationFunction(packet);
                    return packet;
                }
            }
        }

        internal static SubPacketLengthDescriptor GetSubPacketLengthType(BitArray bits)
        {
            SubPacketLengthDescriptor descriptor = bits[^7] switch
            {
                false => SubPacketLengthDescriptor.BitLength,
                true => SubPacketLengthDescriptor.SubPacketCount,
            };

            return descriptor;
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
    }
}
