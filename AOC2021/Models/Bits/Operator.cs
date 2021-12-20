// -----------------------------------------------------------------------
// <copyright file="Operator.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Bits
{
    using System.Collections;

    internal abstract class Operator : Packet, IOperatorPacket
    {
        protected BitArray subPacketBits;

        internal Operator(int version, int type, BitArray bits, Packet? parent, SubPacketLengthDescriptor descriptor, Action<IPacket> packetRegistrationFunction)
           : base(version, type, bits, parent, packetRegistrationFunction)
        {
            this.LengthType = descriptor;
        }

        public SubPacketLengthDescriptor LengthType { get; }

        public List<IPacket> Children { get; } = new();

        public BitArray SubPacketBits => this.subPacketBits;

        public abstract int ProcessChildPackets();
    }
}
