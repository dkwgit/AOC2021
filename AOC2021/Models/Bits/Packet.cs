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

        internal Packet(int version, int type, Action<IPacket, int> packetRegistrationFunction)
        {
            this.Version = version;
            this.Type = type;
            this.PacketRegistrationFunction = packetRegistrationFunction;
        }

        public int ConsumedBits => this.consumedBits;

        public int Version { get; }

        public int Type { get; }

        public Action<IPacket, int> PacketRegistrationFunction { get; }

        public abstract long Value { get; }

        internal static IPacket BuildPacket(BitArray bits, int distanceFromTop, Action<IPacket, int> packetRegistrationFunction)
        {
            int version = GetVersion(bits, distanceFromTop);
            int type = GetType(bits, distanceFromTop);
            if (type == 4)
            {
                Literal packet = new(bits, distanceFromTop, version, type, packetRegistrationFunction);
                packetRegistrationFunction(packet, distanceFromTop);
                return packet;
            }
            else
            {
                SubPacketLengthDescriptor descriptor = GetSubPacketLengthType(bits, distanceFromTop);
                if (descriptor == SubPacketLengthDescriptor.BitLength)
                {
                    OperatorWithFixedSubPacketBitLength packet = new(bits, distanceFromTop, version, type, descriptor, packetRegistrationFunction);
                    packetRegistrationFunction(packet, distanceFromTop);
                    return packet;
                }
                else
                {
                    OperatorWithCountOfSubPackets packet = new(bits, distanceFromTop, version, type,  descriptor, packetRegistrationFunction);
                    packetRegistrationFunction(packet, distanceFromTop);
                    return packet;
                }
            }
        }

        internal static SubPacketLengthDescriptor GetSubPacketLengthType(BitArray bits, int distanceFromTop)
        {
            int bitPosition = 7 + distanceFromTop;
            SubPacketLengthDescriptor descriptor = bits[^bitPosition] switch
            {
                false => SubPacketLengthDescriptor.BitLength,
                true => SubPacketLengthDescriptor.SubPacketCount,
            };

            return descriptor;
        }

        internal static int GetVersion(BitArray bits, int distanceFromTop)
        {
            int version = 0;
            version |= bits[^(1 + distanceFromTop)] ? 1 : 0;
            version <<= 1;
            version |= bits[^(2 + distanceFromTop)] ? 1 : 0;
            version <<= 1;
            version |= bits[^(3 + distanceFromTop)] ? 1 : 0;
            return version;
        }

        internal static int GetType(BitArray bits, int distanceFromTop)
        {
            int type = 0;
            type |= bits[^(4 + distanceFromTop)] ? 1 : 0;
            type <<= 1;
            type |= bits[^(5 + distanceFromTop)] ? 1 : 0;
            type <<= 1;
            type |= bits[^(6 + distanceFromTop)] ? 1 : 0;
            return type;
        }
    }
}
