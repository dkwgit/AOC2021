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
        internal Operator(int version, int type, SubPacketLengthDescriptor descriptor, Action<IPacket, int> packetRegistrationFunction)
           : base(version, type, packetRegistrationFunction)
        {
            this.LengthType = descriptor;
        }

        public SubPacketLengthDescriptor LengthType { get; }
    }
}
